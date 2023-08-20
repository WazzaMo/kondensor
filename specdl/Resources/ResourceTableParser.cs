/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using HtmlParse;
using System.Web;

namespace Resources;

public static class ResourceTableParser
{
  const string FIRST_RESOURCE_TABLE_HEADING = "Resource types";

  public static ParseAction ResourceTable(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .MayExpect(HtmlRules.START_TABLE, annotation:ResourceAnnotations.S_RESOURCE_TABLE)
      .IfThenProduction(
        // node => node.Annotation == ResourceAnnotations.S_RESOURCE_TABLE,
        IsResourceHeading,
        ParseTable
      );
    return parser;
  }

  private static bool IsResourceHeading(Matching node)
  {
    bool isOk = false;
    if (node.Annotation == ResourceAnnotations.S_RESOURCE_TH)
    {
      string heading = String.Empty;
      HtmlPartsUtils.GetThTagValue(node.Parts);
      isOk = heading == FIRST_RESOURCE_TABLE_HEADING;
    }
    return isOk;
  }

  private static ParseAction ParseTable(ParseAction parser)
    => parser
        .Expect(Headings)
        .ExpectProductionUntil(ResourceTableDefinitionParser.DefinitionRow,
      HtmlRules.END_TABLE, ResourceAnnotations.E_RESOURCE_TABLE)
      ;

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

}
