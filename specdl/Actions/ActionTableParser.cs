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
        .Expect(ActionDeclarationProduction)
      .Expect(HtmlRules.END_TR, annotation: ActionAnnotations.END_ROW_ANNOTATION)
      ;
    return parser;
  }


  private static ParseAction ActionDeclarationProduction(ParseAction parser)
    => parser
        .EitherProduction( ActionDeclarationRowspan, ActionDeclarationNoRowspan )
        .IfThenProduction(
          x => x.Annotation == ActionAnnotations.START_CELL_ACTION_ANNOTATION,
          ActionPropertiesOneRowProd
        )
        .IfThenProduction(
          x => x.Annotation == ActionAnnotations.START_CELL_ACTION_ROWSPAN_ANNOTATION,
          ActionPropsRowspan
        )
      ;

  private static ParseAction ActionPropertiesOneRowProd(ParseAction parser)
    => parser
        .Expect( ActionDescriptionOneRowProd )
        .Expect( ActionAccessLevelOneRowProd )
        .Expect( ResourceType )
        .Expect( ConditionKeys )
        .Expect( DependendActions )
      ;

  private static ParseAction ActionPropsRowspan(ParseAction parser)
    => parser
        .Expect( ActionDescriptionRowspanProd )
        .Expect( ActionAccessLevelRowspanProd )
        .Expect( ResourceType )
        .Expect( ConditionKeys )
        .Expect( DependendActions )
      ;

  private static ParseAction ActionDeclarationNoRowspan(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TD, annotation: ActionAnnotations.START_CELL_ACTION_ANNOTATION)
        .Expect(ActionIdAndRefProd)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTION_ANNOTATION)
      ;
    return parser;
  }

  private static ParseAction ActionDeclarationRowspan(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TD_ROWSPAN, annotation: ActionAnnotations.START_CELL_ACTION_ROWSPAN_ANNOTATION)
        .Expect(ActionIdAndRefProd)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTION_ANNOTATION)
      ;
    return parser;
  }

  private static ParseAction ActionIdAndRefProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_A_ID, annotation: ActionAnnotations.START_ID_ACTION_ANNOTATION)
        .Expect(HtmlRules.END_A, annotation: ActionAnnotations.END_ID_ACTION_ANNOTATION)
        .Expect(HtmlRules.START_A_HREF, annotation: ActionAnnotations.START_HREF_ACTION_ANNOTATION)
        .Expect(HtmlRules.END_A, annotation: ActionAnnotations.END_HREF_ACTION_ANNOTATION)
        ;

  // --- SPECIAL CASE
  private static ParseAction NewDescriptionForSameActionProduction(ParseAction parser)
    => parser
        .Expect(NewDescriptionSameAction)
        .Expect(ActionAccessLevelOneRowProd )
        .Expect(ResourceType)
        .Expect(ConditionKeys)
        .Expect(DependendActions)
        ;


  private static ParseAction ActionDescriptionOneRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTIONDESC_ANNOTATION)
      ;

  private static ParseAction ActionDescriptionRowspanProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ROWSPAN, ActionAnnotations.START_CELL_ACTIONDESC_ROWSPAN_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_CELL_ACTIONDESC_ANNOTATION)
      ;

  private static ParseAction NewDescriptionSameAction(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, ActionAnnotations.START_CELL_ACTION_NEWDESC_ANNOTATION)
        .Expect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_NEWDECL_PARA)
        .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_CELL_ACTIONDESC_ANNOTATION)
      ;


  private static ParseAction ActionAccessLevelOneRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, annotation: ActionAnnotations.START_ACCESSLEVEL_ANNOTATION)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_ACCESSLEVEL_ANNOTATION)
      ;

  private static ParseAction ActionAccessLevelRowspanProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ROWSPAN, annotation: ActionAnnotations.START_ACCESSLEVEL_ROWSPAN_ANNOTATION)
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
        .MayExpect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_RESOURCE)
        .MayExpect(HtmlRules.END_A, ActionAnnotations.END_A)
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
        .MayExpect(HtmlRules.START_A_HREF, ActionAnnotations.A_HREF_CONDKEY)
        .MayExpect(HtmlRules.END_A, ActionAnnotations.END_A)
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
        .Expect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_PARA_DEPENDENT)
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