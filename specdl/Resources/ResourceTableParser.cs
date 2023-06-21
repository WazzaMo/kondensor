/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using HtmlParse;

namespace Resources;

public static class ResourceTableParser
{
  public static ParseAction ResourceTable(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation:ResourceAnnotations.S_RESOURCE_TABLE)
        .Expect(Headings)
        .ExpectProductionUntil(DataRow,
      HtmlRules.END_TABLE, ResourceAnnotations.E_RESOURCE_TABLE);
    return parser;
  }

  private static ParseAction Headings(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_THEAD, ResourceAnnotations.S_RESOURCE_THEAD)
        .Expect(HtmlRules.START_TR, ResourceAnnotations.S_RESOURCE_TR_HEADING)
          .ExpectProductionUntil(OneHeading,
        HtmlRules.END_TR, ResourceAnnotations.E_RESOURCE_TR_HEADING)
      .Expect(HtmlRules.END_THEAD, ResourceAnnotations.E_RESOURCE_THEAD)
      ;
    return parser;
  }

  private static ParseAction OneHeading(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TH_VALUE, ResourceAnnotations.S_RESOURCE_TH)
      .Expect(HtmlRules.END_TH, ResourceAnnotations.E_RESOURCE_TH);
    return parser;
  }

  private static ParseAction DataRow(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, ResourceAnnotations.S_RESOURCE_TR)
        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_TYPE)
          .Expect(HtmlRules.START_A_ID, ResourceAnnotations.S_A_ID)
          .Expect(HtmlRules.END_A, ResourceAnnotations.E_A_ID)
          .Expect(HtmlRules.START_A_HREF, ResourceAnnotations.S_A_HREF_NAME)
          .Expect(HtmlRules.END_A, ResourceAnnotations.E_A_HREF_NAME)
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_TYPE)

        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_ARN)
          .Expect(HtmlRules.START_CODE_ATTRIB_VALUE, ResourceAnnotations.S_CODE)
            .ExpectProductionUntil(CodeTextAndSpans,
          HtmlRules.END_CODE, ResourceAnnotations.E_CODE)
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_ARN)

        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_CK)
          .MayExpect(HtmlRules.START_PARA,ResourceAnnotations.S_P_CONDKEY)
            .MayExpect(HtmlRules.START_A_HREF,ResourceAnnotations.S_A_CONDKEY_HREF)
            .MayExpect(HtmlRules.END_A, ResourceAnnotations.E_A_CONDKEY_HREF)
          .MayExpect(HtmlRules.END_PARA, ResourceAnnotations.E_P_CONDKEY)
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_CK)
      .Expect(HtmlRules.END_TR, ResourceAnnotations.E_RESOURCE_TR)
      ;
    return parser;
  }

  private static ParseAction CodeTextAndSpans(ParseAction parser)
  {
    parser
      .MayExpect(HtmlRules.START_SPAN, ResourceAnnotations.S_SPAN)
      .If(
        (match) => match.HasAnnotation && match.Annotation == ResourceAnnotations.S_SPAN,
        (parser, list) => {
          if (list.Last().Annotation == ResourceAnnotations.S_SPAN)
          {
            parser.Expect(HtmlRules.END_SPAN, ResourceAnnotations.E_SPAN);
          }
          return parser;
        }
      )

      .MayExpect(HtmlRules.BODY_CODE_TEXT, ResourceAnnotations.TEXT_CODE)
      ;
    return parser;
  }
}
