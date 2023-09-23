/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


using System.Collections.Generic;
using Optional;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse.Frag;

namespace Actions;

public static class ActionTableParser
{
  internal static ParseAction ActionTableStart( ParseAction parser )
    => parser
        .ScanForAndExpect(HtmlFragRules.START_TR).TagClose()
          .Expect(ActionsHeader)
          .ExpectProductionUntil(
            OtherHeaders,
            HtmlFragRules.END_TR
          ).TagClose()

        .Expect(production: OtherHeaders)
        .ExpectProductionUntil(
          ActionDeclarationParser.ActionDeclarationProduction,
          HtmlFragRules.END_TABLE,
        ActionAnnotations.END_ACTION_TABLE)
        ;

  private static ParseAction ActionsHeader(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TH).TagClose()
        .Expect(HtmlFragRules.ACTIONS_TAG_VALUE, ActionAnnotations.ACTION_HEADING)
        .Expect(HtmlFragRules.END_TH).TagClose()
        ;
  
  private static ParseAction OtherHeaders(ParseAction parser)
    => parser
        .Expect(HtmlFragRules.START_TH).TagClose()
        .Expect(HtmlFragRules.TAG_VALUE, ActionAnnotations.OTHER_HEADING)
        .Expect(HtmlFragRules.END_TH).TagClose()
        ;

}