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

public static class ActionTableParser
{
  internal static ParseAction ActionsHeader(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_THEAD, annotation: ActionAnnotations.START_ACTION_THEAD_ANNOTATION)
        .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_HEADER_TR_ANNOTATION)
          .ExpectProductionUntil(Heading,
        endRule: HtmlRules.END_TR, endAnnodation: ActionAnnotations.END_HEADER_TR_ANNOTATION)
      .Expect(HtmlRules.END_THEAD, annotation: ActionAnnotations.END_ACTION_THEAD_ANNOTATION)
      ;
    return parser;
  }

  private static ParseAction Heading(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TH_VALUE, annotation: ActionAnnotations.START_HEADING_ANNOTATION)
      .Expect(HtmlRules.END_TH, annotation: ActionAnnotations.END_HEADING_ANNOTATION);

  private static ParseAction TableData(ParseAction parser)
    => parser
      .ExpectProductionUntil(RowData, HtmlRules.END_TABLE, ActionAnnotations.END_ACTION_TABLE_ANNOTATION);

  internal static ParseAction RowData(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_ROW_ANNOTATION)
        .Expect( ActionDeclaration )  
        .Expect( ActionDescription )
        .Expect( ActionAccessLevelProduction )
        .Expect( ResourceType )
        .Expect( ConditionKeys )
        .Expect( DependendActions )
      .Expect(HtmlRules.END_TR, annotation: ActionAnnotations.END_ROW_ANNOTATION)
      ;
    return parser;
  }

  private static ParseAction ActionDeclaration(ParseAction parser)
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

  private static ParseAction ActionDescription(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTIONDESC_ANNOTATION)
      ;

  private static ParseAction ActionAccessLevelProduction(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_ACCESSLEVEL_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_ACCESSLEVEL_ANNOTATION)
      ;
  
  private static ParseAction ResourceType(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: ActionAnnotations.START_TD_RESOURCETYPE)
      .ExpectProductionUntil(ResourcePara,
        HtmlRules.END_TD, endAnnodation: ActionAnnotations.END_TD_RESOURCETYPE
      );
  
  private static ParseAction ResourcePara(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_PARA, ActionAnnotations.START_PARA)
        .Expect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_RESOURCE)
        .Expect(HtmlRules.END_A, ActionAnnotations.END_A)
      .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA);

  private static ParseAction ConditionKeys(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, ActionAnnotations.START_TD_CONDKEY)
        .ExpectProductionUntil(
          ConditionKeyEntry,
        HtmlRules.END_TD, ActionAnnotations.END_TD_CONDKEY
      );
  
  private static ParseAction ConditionKeyEntry(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_PARA, ActionAnnotations.START_PARA)
          .Expect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_CONDKEY)
          .Expect(HtmlRules.END_A, ActionAnnotations.END_A)
      .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      ;

  private static ParseAction DependendActions(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, ActionAnnotations.START_TD_DEPACT)
        .ExpectProductionUntil(
          RepeatableDependentActionParagraphs,
      HtmlRules.END_TD, endAnnodation: ActionAnnotations.END_TD_DEPACT)
    ;
  
  private static ParseAction RepeatableDependentActionParagraphs(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_PARA_DEENDENT)
        .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA);


  internal static ParseAction RepeatRowData(ParseAction parser)
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