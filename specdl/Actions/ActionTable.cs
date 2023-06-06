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

  public ActionTable()
  {
    _HeadingNames = new List<string>();
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
      })
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
      if (countResources > 0)
      {
        for(int index = 0; index < countResources; index++)
        {
          parser.Expect(RepeatRowData);
        }
      }
    return parser;
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

    parser
      .MismatchesThen( (list, writer) =>{
        var query = from node in list where node.MatchResult == MatchKind.Mismatch
          select node;
        query.ForEach( (m, idx) => Console.WriteLine(value:$"ActionDecl {idx}: token {m.MismatchToken}"));
      })
      // .SkipUntil(HtmlRules.END_TD)
      // .Expect(HtmlRules.END_TD, annotation:"end:tr:skipped-ActionDeclaration")
    ;
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
