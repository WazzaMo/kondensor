/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;
using kondensor.Parser.HtmlParse;

namespace ConditionKeys;

public static class ConditionKeysTableParser
{
  internal static ParseAction Parser(ParseAction parser)
  {
    parser
      .Expect(HeadingsProd)
      .ExpectProductionUntil(ConditionKeyDefProd,
        HtmlRules.END_TABLE, ConditionAnnotations.E_TABLE_CK)
      ;
    return parser;
  }

  internal static ParseAction HeadingsProd(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_THEAD, ConditionAnnotations.S_THEAD)
        .Expect(HtmlRules.START_TR, ConditionAnnotations.S_TR_HDG)
        .ExpectProductionUntil( OneHeading,
          HtmlRules.END_TR, ConditionAnnotations.E_TR_HDG
        )
      .Expect(HtmlRules.END_THEAD, ConditionAnnotations.E_THEAD)
      ;
    return parser;
  }

  internal static ParseAction OneHeading(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TH_VALUE, ConditionAnnotations.S_TH_VALUE)
      .Expect(HtmlRules.END_TH, ConditionAnnotations.E_TH)
      ;
    return parser;
  }

  internal static ParseAction ConditionKeyDefProd(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, ConditionAnnotations.S_TR_DECL)
        .Expect(HtmlRules.START_TD, ConditionAnnotations.S_TD)
          .Expect(HtmlRules.START_A_ID, ConditionAnnotations.S_AID)
          .Expect(HtmlRules.END_A, ConditionAnnotations.E_AID)
          .Expect(HtmlRules.START_A_HREF, ConditionAnnotations.S_AHREF)
            .MayExpect(HtmlRules.START_AWSUIICON, ConditionAnnotations.S_AWSICON)
            .MayExpect(HtmlRules.END_AWSUIICON, ConditionAnnotations.E_AWSICON)
          .Expect(HtmlRules.END_A, ConditionAnnotations.E_AHREF)
        .Expect(HtmlRules.END_TD, ConditionAnnotations.E_TD)

        .Expect(HtmlRules.START_TD_VALUE, ConditionAnnotations.S_TD_DESC)
        .Expect(HtmlRules.END_TD, ConditionAnnotations.E_TD_DESC)

        .Expect(HtmlRules.START_TD_VALUE, ConditionAnnotations.S_TD_TYPE)
        .Expect(HtmlRules.END_TD, ConditionAnnotations.E_TD_TYPE)

      .Expect(HtmlRules.END_TR, ConditionAnnotations.E_TR_DECL)
      ;
    return parser;
  }
}