/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


using System.Collections.Generic;
using Optional;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;
using kondensor.Parser.AwsHtmlParse.Frag;

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
      .Expect(HtmlFragRules.START_TR, ActionAnnotations.START_ROW_ACTIONS)
        .Expect(ActionDeclarationWithId) // frag
        .Expect(ActionDescriptionProd) // frag
        .Expect(ActionAccessLevelProd) // frag
        .Expect(ActionInitialResourceCondKeyDepProd) // next frag
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
        .Expect(ResourceType) // next frag
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
      .ScanForAndExpect(HtmlFragRules.ID_VALUE, ActionAnnotations.ID_ACTION)
      .ScanForTagClose()
      .Expect(HtmlFragRules.START_A)
        .Expect(HtmlFragRules.HREF_VALUE, ActionAnnotations.HREF_ACTION)
      .ScanForTagClose()
      .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.NAME_ACTION)
      .Expect(HtmlFragRules.END_A).TagClose()
      .Expect(HtmlFragRules.END_TD).TagClose()
      ;
    return parser;
  }

  private static ParseAction ActionDescriptionProd(ParseAction parser)
    => parser
      .Expect(HtmlFragRules.START_TD)
      .ScanForTagClose()
      .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.ACTION_DESCRIPTION)
      .Expect(HtmlFragRules.END_TD).TagClose()
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
        .Expect(HtmlFragRules.START_TD).ScanForTagClose()
          .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.ACTION_ACCESS_LEVEL)
        .Expect(HtmlFragRules.END_TD).TagClose()
        ;

  private static ParseAction ResourceType(ParseAction parser)
    => parser
      .EitherProduction(EmptyResourceType, MultiResourceType);

  // TODO FRAG
  private static ParseAction EmptyResourceType(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ID_VALUE, ActionAnnotations.START_TD_RESOURCETYPE)
      .Expect(HtmlRules.END_TD, ActionAnnotations.END_TD_RESOURCETYPE);

  private static ParseAction MultiResourceType(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TD, ActionAnnotations.RESOURCE_START)
        .ScanForTagClose()
          .ExpectProductionUntil(ResourcePara,
            HtmlFragRules.END_TD, endAnnodation: ActionAnnotations.RESOURCE_END
          ).TagClose()
      ;

  private static ParseAction ResourcePara(ParseAction parser)
    => parser
        .ScanForAndExpect(HtmlFragRules.START_P).ScanForTagClose()
          .ScanForAndExpect(HtmlFragRules.HREF_VALUE, ActionAnnotations.RESOURCE_HREF)
          .ScanForTagClose()
          .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.RESOURCE_NAME)
          .Expect(HtmlFragRules.END_A)
        .Expect(HtmlFragRules.END_P).TagClose()
      ;

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
