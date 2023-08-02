/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;
using Optional;

using Parser;
using HtmlParse;

using YamlWriters;

namespace Actions;

/// <summary>
/// Placeholder for action table processing logic.
/// </summary>
public struct ActionTable
{
  private class InternalData{
    private bool _CanBeWritten;

    public string _SourceUrl = "";
    public List<string> _HeadingNames = new List<string>();
    public List<ActionType> _Actions = new List<ActionType>();
    public Option<ActionAccessLevel> _CurrentAccessLevel;

    public bool IsReadyToWrite => _CanBeWritten;
    public void SignalReadyToWrite() => _CanBeWritten = true;

    public ActionType CurrentAction {
      get {
        if (_Actions.Count == 0) _Actions.Add( item: new ActionType() );
        ActionType _action = _Actions.Last();
        return _action;
      }
    }

    public ActionResourceType CurrentResourceType {
      get {
        ActionAccessLevel level = _CurrentAccessLevel.ValueOr(ActionAccessLevel.Unknown);
        if (level != ActionAccessLevel.Unknown)
          return CurrentAction.GetResourceFor(level);
        else
          throw new InvalidOperationException(message: "Cannot get resource type before processing access level.");
      }
    }

    public InternalData()
    {
      _CanBeWritten = false;
    }
  } // -- Internal Data

  private InternalData _Data;

  public ActionTable()
  {
    _Data = new InternalData() {
      _HeadingNames = new List<string>(),
      _Actions = new List<ActionType>(),
      _CurrentAccessLevel = Option.None<ActionAccessLevel>()
    };
  }

  public void SetSourceUrl(string url) => _Data._SourceUrl = url;

  public ParseAction ActionsTable(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: ActionAnnotations.START_ACTION_TABLE_ANNOTATION)
        .Expect(production: ActionsHeader)
        .Expect(TableData);
      
      if (parser.IsAllMatched)
      {
        _Data.SignalReadyToWrite();
      }
      else
      {
        parser
          .MismatchesThen( (list,wr) => {
            var query = from node in list where node.MatchResult == MatchKind.Mismatch
              select node;
            query.ForEach( (node, idx) => {
              Console.Error.WriteLine(
                value: $"Error # {idx}: mismatch on token {node.MismatchToken} for annotation: {node.Annotation}"
              );
            });
          });
      }
    return parser;
  }

  public void WriteTable(YamlFormatter formatter)
  {
    ActionsYamlWriter.WriteYaml(_Data._SourceUrl, _Data._HeadingNames, _Data._Actions, formatter);
  }

  private ParseAction ActionsHeader(ParseAction parser)
  {
    var headingList = _Data._HeadingNames;

    parser
      .Expect(HtmlRules.START_THEAD, annotation: ActionAnnotations.START_ACTION_THEAD_ANNOTATION)
        .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_HEADER_TR_ANNOTATION)
          .ExpectProductionUntil(Heading,
        endRule: HtmlRules.END_TR, endAnnodation: ActionAnnotations.END_HEADER_TR_ANNOTATION)
      .Expect(HtmlRules.END_THEAD, annotation: ActionAnnotations.END_ACTION_THEAD_ANNOTATION)
      .AllMatchThen( (list, writer) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == ActionAnnotations.START_HEADING_ANNOTATION
          && node.Parts.HasValue
          select node;
        query.ForEach( (node, idx) => {
          headingList.Add( HtmlPartsUtils.GetThTagValue(node.Parts));
        });
      });
    return parser;
  }

  private ParseAction Heading(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TH_VALUE, annotation: ActionAnnotations.START_HEADING_ANNOTATION)
      .Expect(HtmlRules.END_TH, annotation: ActionAnnotations.END_HEADING_ANNOTATION);

  private ParseAction TableData(ParseAction parser)
    => parser
      .ExpectProductionUntil(RowData, HtmlRules.END_TABLE, ActionAnnotations.END_ACTION_TABLE_ANNOTATION);

  private ParseAction RowData(ParseAction parser)
  {
    int countResources = -1;
    ActionType foundAction = new ActionType();
    InternalData data = _Data;
    Action<LinkedList<Matching>>
      processActionInfo = ProcessActionDeclaration,
      processResource = CollectResourceValues;

    parser
      .Expect(ActionTableParser.RowData)
      .AllMatchThen( (list,writer) => {
        processActionInfo(list);
        //
        var query = from node in list
          where (node.HasAnnotation
            && node.Annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION)
          select node;
        Matching match = query.Last();
        string attribName = HtmlPartsUtils.GetTdAttribName(match.Parts);
        if (attribName == "rowspan")
        {
          int rowSpan = HtmlPartsUtils.GetTdAttribIntValue(match.Parts);
          countResources = rowSpan - 1;
        }
        else
          countResources = 0;
        
        processResource(list);
      });

      if (parser.IsAllMatched)
      {
        if (countResources > 0)
        {
          for(int index = 0; index < countResources; index++)
          {
            parser
              .Expect(ActionTableParser.RepeatRowData)
              .AllMatchThen( (list, writer) => processResource(list));
          }
        }
      }
      else
      {
        parser
          .MismatchesThen( (list,wr) => {
          var query = from node in list where node.MatchResult == MatchKind.Mismatch
            select node;
          query.ForEach( (node, idx) => {
            Console.Error.WriteLine(
              value: $"Error # {idx}: mismatch on token {node.MismatchToken} for annotation: {node.Annotation}"
            );
          });
        })
          .SkipUntil(HtmlRules.END_TR)
          .Expect(HtmlRules.END_TR, annotation: "end:tr:SKIPPED-ROW")
        ;
      }
    return parser;
  }

  private void ProcessActionDeclaration(LinkedList<Matching> list)
  {
    ActionType foundAction = new ActionType();
    bool hasData = false;

    var idNode = 
      from node in list where node.Annotation == ActionAnnotations.START_ID_ACTION_ANNOTATION
      select node;
    var hrefNode =
      from node in list where node.Annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION
      select node;
    var descNode =
      from node in list where node.Annotation == ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION
      select node;
    var accessLevel =
      from node in list where node.Annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION
      select node;

    hasData = idNode.Count() > 0
      && hrefNode.Count() > 0
      && descNode.Count() > 0
      && accessLevel.Count() > 0;

    if (hasData)
    {
      Matching id = idNode.Last();
      Matching href = hrefNode.Last();
      Matching desc = descNode.Last();

      foundAction.SetActionId(HtmlPartsUtils.GetAIdAttribValue(id.Parts));
      foundAction.SetActionName(HtmlPartsUtils.GetAHrefTagValueLongShort( href ));
      foundAction.SetApiDocLink(HtmlPartsUtils.GetAHrefAttribValue(href.Parts));
      foundAction.SetDescription(HtmlPartsUtils.GetTdTagValue(desc.Parts));
      _Data._Actions.Add(foundAction);

      Matching levelNode = accessLevel.Last();
      string level = HtmlPartsUtils.GetTdTagValue(levelNode.Parts);
      ActionAccessLevel _level = level.GetLevelFrom();
      if ( _level != ActionAccessLevel.Unknown )
      {
        _Data._CurrentAccessLevel = Option.Some( _level );
      }
      else {
        Console.Error.WriteLine($"Cannot describe '{level}' using {nameof(ActionAccessLevel)}");
      }

      if (_level == ActionAccessLevel.Unknown )
        Console.Error.WriteLine(value: $"Unknown resource type for {foundAction.Name}");
    }
    else{
      Console.Error.WriteLine(value: "Missing some details");
    }
  }

  private void CollectActionDeclarations(LinkedList<Matching> list)
  {
    var declarationNodes = FilterActionDeclaration(list).GetEnumerator();
    ActionType actionDecl = new ActionType();
    ActionAccessLevel level;
    ActionResourceType resourceType;

    while( declarationNodes.MoveNext())
    {
      if (IsStartActionAnnotation(declarationNodes.Current.Annotation) )
      {
        Matching aId = declarationNodes.Current;
        declarationNodes.MoveNext();
        if (
          declarationNodes.Current.Annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION
        )
        {
          Matching aHref = declarationNodes.Current;
          declarationNodes.MoveNext();

          string id = HtmlPartsUtils.GetAIdAttribValue(aId.Parts);
          string actionName = HtmlPartsUtils.GetAHrefTagValue(aHref.Parts);
          string apiLink = HtmlPartsUtils.GetAHrefAttribValue(aHref.Parts);
          actionDecl = new ActionType();
          actionDecl.SetActionId(id);
          actionDecl.SetActionName(actionName);
          actionDecl.SetApiDocLink(apiLink);
          _Data._Actions.Add( actionDecl );

          if ( IsFirstActionDescription(declarationNodes.Current.Annotation) )
          {
            Matching desc = declarationNodes.Current;
            declarationNodes.MoveNext();
            
            string description = HtmlPartsUtils.GetTdTagValue(desc.Parts);
            if (
              HasCollectedActionProperties(
                declarationNodes,
                description,
                out level,
                out resourceType
              )
            )
            {
              _Data._CurrentAccessLevel = Option.Some( level );
              _Data.CurrentAction.MapAccessToResourceType(level, resourceType );
            }
          }

        }
      }
      else if (
        IsSameActionNewDescriptionAnnotation(declarationNodes.Current.Annotation)
        && actionDecl.IsActionIdSet)
      {
        // reuse actionDecl to create ActionResourceType(s) with new description.
      }
    }
  }

  private bool HasCollectedActionProperties(
    IEnumerator<Matching> nodes,
    string description,
    out ActionAccessLevel level,
    out ActionResourceType resourceType
  )
  {
    bool result = false;

    level = ActionAccessLevel.Unknown;
    resourceType = new ActionResourceType();

    bool isOk = nodes.Current.Annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION;
    if (isOk)
    {
      Matching levelNode = nodes.Current;
      nodes.MoveNext();

      string levelTxt = HtmlPartsUtils.GetTdTagValue(levelNode.Parts);
      level = levelTxt.GetLevelFrom();
      if (level != ActionAccessLevel.Unknown)
      {
        if (nodes.Current.Annotation == ActionAnnotations.A_HREF_RESOURCE)
        {
          Matching aHrefResource = nodes.Current;

          resourceType = new ActionResourceType();
          string resourceId, resourceName;
          resourceId = HtmlPartsUtils.GetAHrefAttribValue(aHrefResource.Parts);
          resourceName = HtmlPartsUtils.GetAHrefTagValue(aHrefResource.Parts);
          resourceType.SetTypeIdAndName(resourceId, resourceName);
          resourceType.SetDescription(description);

          while(nodes.MoveNext())
          {
            if (nodes.Current.Annotation == ActionAnnotations.A_HREF_CONDKEY)
            {
              Matching aHrefConditionKey = nodes.Current;
              string ckId, ckName;

              ckId = HtmlPartsUtils.GetAHrefAttribValue(aHrefConditionKey.Parts);
              ckName = HtmlPartsUtils.GetAHrefTagValue(aHrefConditionKey.Parts);
              resourceType.AddConditionKeyId(ckId);
            }

            if (nodes.Current.Annotation == ActionAnnotations.START_PARA_DEPENDENT )
            {
              Matching tdDependency = nodes.Current;
              string depId = HtmlPartsUtils.GetPTagValue(tdDependency.Parts);

              if (! depId.IsEmptyPartsValue())
                resourceType.AddDependentActionId(depId);
            }
          }
          result = true;
        }
      }
    }
    return result;
  }

  private IEnumerable<Matching> FilterActionDeclaration(LinkedList<Matching> list)
  {
    Func<string, bool> filter = IsActionDeclarationAnnotation;
    var query = from node in list where filter(node.Annotation) select node;
    return query;
  }

  private bool IsActionDeclarationAnnotation(string annotation)
    => IsRowStart(annotation)
    || IsRowEnd(annotation)
    || IsStartActionAnnotation(annotation) 
    || annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION
    || IsFirstActionDescription(annotation)
    || IsSameActionNewDescriptionAnnotation(annotation)
    || annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION
    || annotation == ActionAnnotations.A_HREF_RESOURCE
    || annotation == ActionAnnotations.A_HREF_CONDKEY
    || annotation == ActionAnnotations.START_PARA_DEPENDENT
    ;

  private bool IsRowStart(string annotation)
    => annotation == ActionAnnotations.START_ROW_ANNOTATION;
  
  private bool IsRowEnd(string annotation)
    => annotation == ActionAnnotations.END_ROW_ANNOTATION;

  private bool IsStartActionAnnotation(string annotation)
    => annotation == ActionAnnotations.START_ID_ACTION_ANNOTATION;

  private bool IsFirstActionDescription(string annotation)
    => annotation == ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION;
  
  private bool IsSameActionNewDescriptionAnnotation(string annotation)
    => annotation == ActionAnnotations.START_CELL_ACTION_NEWDESC_ANNOTATION;

  /// <summary>
  /// Condemned code
  /// </summary>
  /// <param name="list"></param>
  private void CollectResourceValues(LinkedList<Matching> list)
  {
    ActionResourceType resType;

    if (_Data._CurrentAccessLevel.HasValue)
    {
      ActionAccessLevel level = _Data._CurrentAccessLevel.ValueOr(ActionAccessLevel.Unknown);
      IEnumerator<Matching> nodesToCollect = FilterRepeatedRow(list).GetEnumerator();

      Matching aHrefResource;
      resType = new ActionResourceType();
      
      if (nodesToCollect.MoveNext() &&
        nodesToCollect.Current.Annotation == ActionAnnotations.A_HREF_RESOURCE)
      {
        aHrefResource = nodesToCollect.Current;
        string resourceId, resourceName;
        resourceId = HtmlPartsUtils.GetAHrefAttribValue(aHrefResource.Parts);
        resourceName = HtmlPartsUtils.GetAHrefTagValue(aHrefResource.Parts);
        resType.SetTypeIdAndName(resourceId, resourceName);
        _Data.CurrentAction.MapAccessToResourceType(level, resType);
      }

      while(nodesToCollect.MoveNext())
      {
        if (nodesToCollect.Current.Annotation == ActionAnnotations.A_HREF_CONDKEY)
        {
          Matching aHrefConditionKey = nodesToCollect.Current;
          string ckId, ckName;

          ckId = HtmlPartsUtils.GetAHrefAttribValue(aHrefConditionKey.Parts);
          ckName = HtmlPartsUtils.GetAHrefTagValue(aHrefConditionKey.Parts);
          resType.AddConditionKeyId(ckId);
        }

        if (nodesToCollect.Current.Annotation == ActionAnnotations.START_PARA_DEPENDENT )
        {
          Matching tdDependency = nodesToCollect.Current;
          string depId = HtmlPartsUtils.GetPTagValue(tdDependency.Parts);

          if (! depId.IsEmptyPartsValue())
            resType.AddDependentActionId(depId);
        }
      }
    }
  }

  private IEnumerable<Matching> FilterRepeatedRow(LinkedList<Matching> list)
  {
    Func<string, bool> isNeededNode = IsResourceConditionKeyOrDependency;

    var startNodes = from node in list
      where isNeededNode(node.Annotation) select node;
    return startNodes;
  }

  private static bool IsResourceConditionKeyOrDependency(string annotation)
    => annotation == ActionAnnotations.A_HREF_RESOURCE
      || annotation == ActionAnnotations.A_HREF_CONDKEY
      || annotation == ActionAnnotations.START_PARA_DEPENDENT
      ;

}
