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
      .Expect(HtmlRules.END_TABLE, annotation: ResourceAnnotations.E_RESOURCE_TABLE);
    return parser;
  }

  private static ParseAction Headings(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_THEAD, ResourceAnnotations.S_RESOURCE_THEAD)
        .Expect(HtmlRules.START_TR)
          .Expect(HtmlRules.START_TR, ResourceAnnotations.S_RESOURCE_TR)
            .ExpectProductionUntil(OneHeading,
        HtmlRules.END_TR, ResourceAnnotations.E_RESOURCE_TR)
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
}
