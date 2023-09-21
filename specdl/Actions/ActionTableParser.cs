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

public static class ActionTableParser
{
  internal static ParseAction ActionTableStart( ParseAction parser )
    => parser
        .Expect(production: ActionsHeader)
        .ExpectProductionUntil(
          ActionDeclarationParser.ActionDeclarationProduction,
          HtmlRules.END_TABLE,
        ActionAnnotations.END_ACTION_TABLE_ANNOTATION)
        ;

  private static ParseAction ActionsHeader(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_THEAD, annotation: ActionAnnotations.START_ACTION_THEAD_ANNOTATION)
        .Expect(HtmlRules.START_TR, annotation: ActionAnnotations.START_HEADER_TR_ANNOTATION)
          .Expect(MainHeadingProd)
          .ExpectProductionUntil(OtherHeadings,
        endRule: HtmlRules.END_TR, endAnnodation: ActionAnnotations.END_HEADER_TR_ANNOTATION)
      .Expect(HtmlRules.END_THEAD, annotation: ActionAnnotations.END_ACTION_THEAD_ANNOTATION)
      ;
    return parser;
  }

  /// <summary>Ensures table is the Actions table.</summary>
  /// <param name="parser">parser to use</param>
  /// <returns>parser for chaining.</returns>
  private static ParseAction MainHeadingProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TH_ACTION, ActionAnnotations.START_TH_ACTIONS)
      .Expect(HtmlRules.END_TH, ActionAnnotations.END_HEADING_ANNOTATION);

  private static ParseAction OtherHeadings(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TH_VALUE, annotation: ActionAnnotations.START_HEADING_ANNOTATION)
      .Expect(HtmlRules.END_TH, annotation: ActionAnnotations.END_HEADING_ANNOTATION)
      ;

}