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
      .Expect(HtmlFragRules.START_TR, ActionAnnotations.START_ROW_ACTION)
        .Expect(ActionDeclarationWithId) // frag
        .Expect(ActionDescriptionProd) // frag
        .Expect(ActionAccessLevelProd) // frag
        .Expect(ActionInitialResourceCondKeyDepProd) // next frag
      .Expect(HtmlRules.END_TR, annotation: ActionAnnotations.END_ROW_ACTION)
      .ProductionWhileMatch(ActionRowsResourceCondKeyDependentsProd)
      .ProductionWhileMatch(ActionNewDescriptionResourceCondKeyDependentProd)
      ;

  private static ParseAction ActionInitialResourceCondKeyDepProd(ParseAction parser)
    => PropertiesResourceCondKeyDepProd(parser);

  private static ParseAction ActionRowsResourceCondKeyDependentsProd(ParseAction parser)
    => parser
      .Expect(HtmlFragRules.START_TR, ActionAnnotations.START_ROW_ADD_PROPS_ACTION)
      .ScanForTagClose()
        .Expect(PropertiesResourceCondKeyDepProd)
      .Expect(HtmlFragRules.END_TR, ActionAnnotations.END_ROW_ADD_PROPS_ACTION)
      .TagClose()
      ;

  private static ParseAction PropertiesResourceCondKeyDepProd(ParseAction parser)
    => parser
        .Expect(ResourceType) //  frag
        .Expect(ConditionKeys) // frag
        .Expect(DependendActions) // next frag
      ;


  // --- SPECIAL CASE
  private static ParseAction ActionNewDescriptionResourceCondKeyDependentProd(ParseAction parser)
    => parser
      .Expect(HtmlFragRules.START_TR, ActionAnnotations.START_ROW_EXTENDED_ACTION)
      .ScanForTagClose()
        .Expect(NewDescriptionSameAction)
        .Expect(EmptyAccessLevelProd)
        .Expect(ActionInitialResourceCondKeyDepProd)
      .Expect(HtmlFragRules.END_TR, ActionAnnotations.END_ROW_EXTENDED_ACTION)
      .TagClose()
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
        .Expect(HtmlFragRules.START_TD).ScanForTagClose()
          .Expect(HtmlFragRules.START_P).TagClose()
            .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.SAME_ACTION_DESCRIPTION)
          .Expect(HtmlFragRules.END_P).TagClose()
        .Expect(HtmlFragRules.END_TD).TagClose()
      ;

  private static ParseAction EmptyAccessLevelProd(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TD).ScanForTagClose()
        .Expect(HtmlFragRules.END_TD).TagClose()
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

  private static ParseAction EmptyResourceType(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TD, ActionAnnotations.RESOURCE_START).ScanForTagClose()
        .Expect(HtmlFragRules.END_TD, ActionAnnotations.RESOURCE_END).TagClose()
        ;

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

  // TODO adjust for <p>name | <p><a href>name
  private static ParseAction ConditionKeys(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TD, ActionAnnotations.CONDKEY_START)
        .ExpectProductionUntil(
          ConditionKeyEntry,
          HtmlFragRules.END_TD, ActionAnnotations.CONDKEY_END
        );


  // See this link - shows that <a href> is optional and that it's
  // possible there could be a name only in the paragraph
  // https://docs.aws.amazon.com/service-authorization/latest/reference/list_apachekafkaapisforamazonmskclusters.html
  private static ParseAction ConditionKeyEntry(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_P)
          .Expect(HtmlFragRules.START_A)
            .MayExpect(HtmlFragRules.HREF_VALUE, ActionAnnotations.CONDKEY_HREF)
            .TagClose()
            .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.CONDKEY_NAME)
          .Expect(HtmlFragRules.END_A)
        .Expect(HtmlFragRules.END_P).TagClose()
        ;

  private static ParseAction DependendActions(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TD, ActionAnnotations.DEPACT_START)
          .ExpectProductionUntil(RepeatableDependentActionParagraphs,
        HtmlFragRules.END_TD, ActionAnnotations.DEPACT_END).TagClose()
        ;

  private static ParseAction RepeatableDependentActionParagraphs(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_P).ScanForTagClose()
          .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.DEPACT_VALUE)
        .Expect(HtmlFragRules.END_P).TagClose()
        ;

}
