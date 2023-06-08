/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;
using Optional;

using Parser;
using HtmlParse;

namespace Actions;

/// <summary>
/// Placeholder for action table processing logic.
/// </summary>
public struct ActionTable
{
  private List<string> _HeadingNames;
  private List<ActionType> _Actions;
  private Option<ActionAccessLevel> _CurrentAccessLevel;
  private Option<ActionResourceType> _CurrentResourceType;

  public ActionTable()
  {
    _HeadingNames = new List<string>();
    _Actions = new List<ActionType>();
    _CurrentAccessLevel = Option.None<ActionAccessLevel>();
    _CurrentResourceType = Option.None<ActionResourceType>();
  }

  public ParseAction ActionsTable(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: ActionAnnotations.START_ACTION_TABLE_ANNOTATION)
        .Expect(production: ActionsHeader)
        .Expect(TableData)
      // .Expect(HtmlRules.END_TABLE, annotation: ActionAnnotations.END_ACTION_TABLE_ANNOTATION)
      .MismatchesThen( (list,wr) => {
        var query = from node in list where node.MatchResult == MatchKind.Mismatch
          select node;
        query.ForEach( (node, idx) => {
          Console.Error.WriteLine(
            value: $"Error # {idx}: mismatch on token {node.MismatchToken} for annotation: {node.Annotation}"
          );
        });
      })
      ;
    return parser;
  }

  private ParseAction ActionsHeader(ParseAction parser)
  {
    var headingList = _HeadingNames;

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
    Action<LinkedList<Matching>>
      processActionInfo = ProcessActionDeclaration,
      processResource = ProcessResoureVlues;

    parser
      .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_ROW_ANNOTATION)
        .Expect( ActionDeclaration )  
        .Expect( ActionDescription )
        .Expect( ActionAccessLevel )
        .Expect( ResourceType )
        .Expect( ConditionKeys )
        .Expect( DependendActions )
      .Expect(HtmlRules.END_TR, annotation: ActionAnnotations.END_ROW_ANNOTATION)
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
      });

      if (parser.IsAllMatched)
      {
        if (countResources > 0)
        {
          for(int index = 0; index < countResources; index++)
          {
            parser.Expect(RepeatRowData);
          }
        }
        parser.AllMatchThen( (list, writer) => processResource(list));
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
    var resourceType = 
      from node in list where node.Annotation == ActionAnnotations.A_HREF_RESOURCE
      select node;

    hasData = idNode.Count() > 0
      && hrefNode.Count() > 0
      && descNode.Count() > 0
      && accessLevel.Count() > 0
      && resourceType.Count() > 0;

    if (hasData)
    {
      Matching id = idNode.Last();
      Matching href = hrefNode.Last();
      Matching desc = descNode.Last();

      foundAction.SetActionId(HtmlPartsUtils.GetAIdAttribValue(id.Parts));
      foundAction.SetActionName(HtmlPartsUtils.GetAHrefTagValue(href.Parts));
      foundAction.SetApiDocLink(HtmlPartsUtils.GetAHrefAttribValue(href.Parts));
      foundAction.SetDescription(HtmlPartsUtils.GetTdTagValue(desc.Parts));
      _Actions.Add(foundAction);

      Matching levelNode = accessLevel.Last();
      string level = HtmlPartsUtils.GetTdTagValue(levelNode.Parts);
      ActionAccessLevel levelValue = Enum.Parse<ActionAccessLevel>(level);
      _CurrentAccessLevel = Option.Some( levelValue );
      _CurrentResourceType = Option.Some( new ActionResourceType() );
      if (levelValue == Actions.ActionAccessLevel.Unknown)
        Console.Error.WriteLine(value: $"Unknown resource type for {foundAction.Name}");
    }
    else{
      Console.Error.WriteLine(value: "Missing some details");
    }
  }

  private void ProcessResoureVlues(LinkedList<Matching> list)
  {
    //
  }

  private ParseAction ActionDeclaration(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: ActionAnnotations.START_CELL_ACTION_ANNOTATION)
        .Expect(HtmlRules.START_A_ID, annotation: ActionAnnotations.START_ID_ACTION_ANNOTATION)
        .Expect(HtmlRules.END_A, annotation: ActionAnnotations.END_ID_ACTION_ANNOTATION)
        .Expect(HtmlRules.START_A_HREF, annotation: ActionAnnotations.START_HREF_ACTION_ANNOTATION)
        .Expect(HtmlRules.END_A, annotation: ActionAnnotations.END_HREF_ACTION_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTION_ANNOTATION)
      ;

    if (! parser.IsAllMatched)
    {
      parser
        .MismatchesThen( (list, writer) =>{
          var query = from node in list where node.MatchResult == MatchKind.Mismatch
            select node;
          query.ForEach( (m, idx) => Console.WriteLine(value:$"ActionDecl {idx}: token {m.MismatchToken}"));
        })
        .SkipUntil(HtmlRules.END_TD)
        .Expect(HtmlRules.END_TD, annotation:"end:tr:skipped-ActionDeclaration")
      ;
    }
    return parser;
  }

  private ParseAction ActionDescription(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTIONDESC_ANNOTATION)
      ;

  private ParseAction ActionAccessLevel(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_ACCESSLEVEL_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_ACCESSLEVEL_ANNOTATION)
      ;
  
  private ParseAction ResourceType(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: ActionAnnotations.START_TD_RESOURCETYPE)
        .MayExpect(HtmlRules.START_PARA, ActionAnnotations.START_PARA)
          .MayExpect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_RESOURCE)
          .MayExpect(HtmlRules.END_A, ActionAnnotations.END_A)
        .MayExpect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_TD_RESOURCETYPE)
    ;

  private ParseAction ConditionKeys(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, ActionAnnotations.START_TD_CONDKEY)
        .MayExpect(HtmlRules.START_PARA, ActionAnnotations.START_PARA)
          .MayExpect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_CONDKEY)
          .MayExpect(HtmlRules.END_A, ActionAnnotations.END_A)
        .MayExpect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_CONDKEY)
    ;

  private ParseAction DependendActions(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, ActionAnnotations.START_TD_DEPACT)
        .MayExpect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_PARA_DEENDENT)
        .MayExpect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_DEPACT)
    ;


  private ParseAction RepeatRowData(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, ActionAnnotations.START_ROW_ANNOTATION)
        .Expect( ResourceType )
        .Expect( ConditionKeys )
        .Expect( DependendActions )
      .Expect(HtmlRules.END_TR, ActionAnnotations.END_ROW_ANNOTATION);
    return parser;
  }
}
