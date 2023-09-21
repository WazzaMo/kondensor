/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

namespace RootDoc;


/// <summary>
/// Parser for the topics list page that identifies all
/// the separate policy documents.
/// <see
///   href="https://docs.aws.amazon.com/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html"
/// />
/// </summary>
public static class RootDocParser
{
  public static ParseAction RootDocProduction(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_H6_VALUE)
      .Expect(HtmlRules.START_H6_VALUE, RootAnnotations.START_LIST_HEADING)
      .Expect(HtmlRules.END_H6, RootAnnotations.END_LIST_HEADING)
      .Expect(PageListProd)
      ;
    return parser;
  }

  private static ParseAction PageListProd(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_UL, RootAnnotations.START_UL)
      .ExpectProductionUntil( PageEntryProd,
        HtmlRules.END_UL, RootAnnotations.END_UL
      );
    return parser;
  }

  private static ParseAction PageEntryProd(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_LI, RootAnnotations.START_LI)
        .Expect(HtmlRules.START_A_HREF, RootAnnotations.START_AHREF)
        .Expect(HtmlRules.END_A, RootAnnotations.END_AHREF)
      .Expect(HtmlRules.END_LI, RootAnnotations.END_LI)
      ;
    return parser;
  }
}