/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


using System.Collections.Generic;
using Optional;

using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.HtmlParse;
using kondensor.YamlFormat;

using YamlWriters;

namespace Actions;

/// <summary>
/// Placeholder for action table processing logic.
/// </summary>
public struct ActionTable
{
  private class InternalData{
    private bool _CanBeWritten;
    private string _SavedDescription;

    public string _SourceUrl = "";
    public List<string> _HeadingNames;
    public List<ActionType> _Actions;
    public Option<ActionAccessLevel> _CurrentAccessLevel;

    public void SignalReadyToWrite() => _CanBeWritten = true;
    public bool HaveSavedDescription()
      => ! String.IsNullOrEmpty(_SavedDescription);
    
    public string SavedDescription {
      get => _SavedDescription;
      set => _SavedDescription = value;
    }

    public ActionType CurrentAction {
      get {
        if (_Actions.Count == 0) _Actions.Add( item: new ActionType() );
        ActionType _action = _Actions.Last();
        return _action;
      }
    }

    public bool IsReadyToWrite => _CanBeWritten;

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
      _SavedDescription = "";
      _CanBeWritten = false;
      _HeadingNames = new List<string>();
      _Actions = new List<ActionType>();
      _CurrentAccessLevel = Option.None<ActionAccessLevel>();
    }
  } // -- Internal Data

  public bool IsReadyToWrite => _Data.IsReadyToWrite;

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
    parser.Expect(ActionTableParser.ActionTableStart);
      
    if (parser.IsAllMatched)
    {
      _Data.SignalReadyToWrite();
      parser.AllMatchThen( CollectParsedData );
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

  private void CollectParsedData(LinkedList<Matching> list, IPipeWriter _)
  {
    CollectHeadings(list);
    CollectActionDeclarations(list);
  }

  private void CollectHeadings(LinkedList<Matching> list)
  {
    var headingList = _Data._HeadingNames;

    var query = from node in list
      where node.HasAnnotation && node.Annotation == ActionAnnotations.START_HEADING_ANNOTATION
      && node.Parts.HasValue
      select node;

    query.ForEach( (node, idx) => {
      headingList.Add( HtmlPartsUtils.GetThTagValue(node.Parts));
    });
  }

  private void CollectActionDeclarations(LinkedList<Matching> list)
  {
    var declarationNodes
      = ActionCollection.FilterActionDeclaration(list).GetEnumerator();
    
    ActionType actionDecl = new ActionType();
    ActionAccessLevel level;
    ActionResourceType resourceType = new ActionResourceType();

    while( declarationNodes.MoveNext())
    {
      if (
        ActionCollection.IsStartActionIdAnnotation(declarationNodes.Current.Annotation)
      )
      {
        Matching aId = declarationNodes.Current;
        declarationNodes.MoveNext();
        if (
          ActionCollection.IsStartActionNameAndRef(declarationNodes.Current.Annotation)
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

          if (
            ActionCollection.IsFirstActionDescription(declarationNodes.Current.Annotation)
          )
          {
            Matching desc = declarationNodes.Current;
            declarationNodes.MoveNext();
            
            string description = HtmlPartsUtils.GetTdTagValue(desc.Parts);
            _Data.SavedDescription = description;
            bool hasResourceType = ActionResourceCollection.HasCollectedActionProperties(
                declarationNodes,
                description,
                out level,
                out resourceType
              );
            if (hasResourceType)
            {
              if (resourceType.Description.IsEmptyPartsValue())
              {
                resourceType.SetDescription(description);
              }
              _Data._CurrentAccessLevel = Option.Some( level );
              _Data.CurrentAction.MapAccessToResourceType(level, resourceType );
            }
          }
        }
      }
      else if (
        ActionCollection.IsActionPropertyRowStart(declarationNodes.Current.Annotation)
      )
      {
        declarationNodes.MoveNext();
        if (
          ActionCollection.IsResourceConditionKeyOrDependency( declarationNodes.Current.Annotation)
          && resourceType.IsDescriptionSet
        )
        {
          ActionResourceType nextResourceType
            = ActionResourceCollection.CopyResourceType(resourceType);
          bool isNew = ActionResourceCollection.CollectActionPropertyRow(declarationNodes, ref nextResourceType);
          var _level = _Data._CurrentAccessLevel.ValueOr(ActionAccessLevel.Unknown);

          if (_level != ActionAccessLevel.Unknown)
          {
            if (isNew)
            {
              if (nextResourceType.Description.IsEmptyPartsValue()
                && _Data.HaveSavedDescription()
              )
              {
                nextResourceType.SetDescription(_Data.SavedDescription);
              }
              _Data.CurrentAction.MapAccessToResourceType(
                _level, nextResourceType
              );
            }
            else
            {
              _Data.CurrentAction.TryMapUpdateAccessAndResourceType(
                _level,
                nextResourceType
              );
            }
          }
        }
        
        if (
          ActionCollection.IsSameActionNewDescriptionAnnotation(declarationNodes.Current.Annotation)
          && actionDecl.IsActionIdSet && resourceType.IsIdAndNameSet
        )
        {
          // reuse actionDecl to create ActionResourceType(s) with new description.
          Matching descNode = declarationNodes.Current;
          declarationNodes.MoveNext();

          string description = HtmlPartsUtils.GetPTagValue(descNode.Parts);
          ActionResourceType nextDescResourceType = ActionResourceCollection.CopyResourceType(resourceType);
          bool isNew = ActionResourceCollection.CollectActionPropertyRow(declarationNodes, ref nextDescResourceType);
          nextDescResourceType.SetDescription(description);

          ActionAccessLevel _level = _Data._CurrentAccessLevel.ValueOr(ActionAccessLevel.Unknown);
          if (_level != ActionAccessLevel.Unknown)
            _Data.CurrentAction.MapAccessToResourceType(_level, nextDescResourceType);
        }
      }
    }
  }

}
