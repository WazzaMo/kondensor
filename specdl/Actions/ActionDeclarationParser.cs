/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


using System.Collections.Generic;
using Optional;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

namespace Actions;

public static class ActionDeclarationParser
{
  /// <summary>
  /// Parse either rowspanned or singular action declarations.
  /// </summary>
  /// <param name="parser">Parser instance to use.</param>
  /// <returns>Same parser returned for fluid API.</returns>
  internal static ParseAction ActionDeclarationProduction(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_ACTION_ROW_ANNOTATION)
        .Expect(ActionDeclarationWithId)
        .Expect(ActionDescriptionProd)
        .Expect(ActionAccessLevelProd)
        .Expect(ActionInitialResourceCondKeyDepProd)
      .Expect(HtmlRules.END_TR, annotation: ActionAnnotations.END_TR_ACTION_PROP_ROW)
      .ProductionWhileMatch(ActionRowsResourceCondKeyDependentsProd)
      .ProductionWhileMatch(ActionNewDescriptionResourceCondKeyDependentProd)
      ;

  private static ParseAction ActionInitialResourceCondKeyDepProd(ParseAction parser)
    => PropertiesResourceCondKeyDepProd(parser);

  private static ParseAction ActionRowsResourceCondKeyDependentsProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TR, ActionAnnotations.START_TR_ACTION_PROP_ROW)
        .Expect(PropertiesResourceCondKeyDepProd)
      .Expect(HtmlRules.END_TR, ActionAnnotations.END_TR_ACTION_PROP_ROW)
      ;

  private static ParseAction PropertiesResourceCondKeyDepProd(ParseAction parser)
    => parser
        .Expect(ResourceType)
        .Expect(ConditionKeys)
        .Expect(DependendActions)
      ;


  // --- SPECIAL CASE
  private static ParseAction ActionNewDescriptionResourceCondKeyDependentProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TR, ActionAnnotations.START_TR_ACTION_PROP_ROW)
        .Expect(NewDescriptionSameAction)
        .Expect(EmptyAccessLevelProd)
        .Expect(ActionInitialResourceCondKeyDepProd)
      .Expect(HtmlRules.END_TR, ActionAnnotations.END_TR_ACTION_PROP_ROW)
      ;

  private static ParseAction ActionDeclarationWithId(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_ID_ACTION)
        .Expect(ActionIdAndRefProd)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_CELL_ACTION_ANNOTATION)
      ;
    return parser;
  }

  private static ParseAction ActionIdAndRefProd(ParseAction parser)
    => parser
        .MayExpect(HtmlRules.START_A_ID, ActionAnnotations.START_ID_ACTION_ANNOTATION)
        .MayExpect(HtmlRules.END_A, ActionAnnotations.END_ID_ACTION_ANNOTATION)
        .Expect(HtmlRules.START_A_HREF, ActionAnnotations.START_HREF_ACTION_ANNOTATION)
        .MayExpect(HtmlRules.START_AWSUIICON, ActionAnnotations.START_AWSICON)
        .MayExpect(HtmlRules.END_AWSUIICON, ActionAnnotations.END_AWSICON)
        .Expect(HtmlRules.END_A, ActionAnnotations.END_HREF_ACTION_ANNOTATION)
        ;

  private static ParseAction ActionDescriptionProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_ACTIONDESC)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_ACTIONDESC)
      ;

  private static ParseAction NewDescriptionSameAction(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_ACTION_NEWDESC)
        .Expect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_NEWDECL_PARA)
        .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_ACTIONDESC)
      ;

  private static ParseAction EmptyAccessLevelProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_ACCESSLEVEL_EMPTY_ANNOTATION)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_ACCESSLEVEL)
      ;

  private static ParseAction ActionAccessLevelProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, annotation: ActionAnnotations.START_TD_ACCESSLEVEL)
      .Expect(HtmlRules.END_TD, annotation: ActionAnnotations.END_TD_ACCESSLEVEL)
      ;

  private static ParseAction ResourceType(ParseAction parser)
    => parser
      .EitherProduction(EmptyResourceType, MultiResourceType);

  private static ParseAction EmptyResourceType(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_RESOURCETYPE)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_RESOURCETYPE);

  private static ParseAction MultiResourceType(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_RESOURCETYPE)
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
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_CONDKEY)
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
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_DEPACT)
        .ExpectProductionUntil(
          RepeatableDependentActionParagraphs,
      HtmlRules.END_TD, endAnnodation: ActionAnnotations.END_TD_DEPACT)
    ;

  private static ParseAction RepeatableDependentActionParagraphs(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_PARA_VALUE, ActionAnnotations.START_PARA_DEPENDENT)
        .Expect(HtmlRules.END_PARA, ActionAnnotations.END_PARA);

}
