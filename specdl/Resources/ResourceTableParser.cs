/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;

using kondensor.Parser;
using kondensor.Parser.HtmlParse;
using System.Web;

namespace Resources;

public static class ResourceTableParser
{
  const string FIRST_RESOURCE_TABLE_HEADING = "Resource types";

  public static ParseAction ResourceTable(ParseAction parser)
  {
    parser
      // .Expect(HtmlRules.START_TABLE, annotation:ResourceAnnotations.S_RESOURCE_TABLE)
      .Expect( ParseTable );
    return parser;
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
          .Expect( ResourceTableHeadingProd )
          .ExpectProductionUntil(OneHeading,
        HtmlRules.END_TR, ResourceAnnotations.E_RESOURCE_TR_HEADING)
      .Expect(HtmlRules.END_THEAD, ResourceAnnotations.E_RESOURCE_THEAD)
      ;
    return parser;
  }

  private static ParseAction ResourceTableHeadingProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_TH_RESOURCES, ResourceAnnotations.S_RESOURCE_RESOURCES_TH)
        .Expect(HtmlRules.END_TH, ResourceAnnotations.E_RESOURCE_TH)
        ;

  private static ParseAction OneHeading(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TH_VALUE, ResourceAnnotations.S_RESOURCE_TH)
      .Expect(HtmlRules.END_TH, ResourceAnnotations.E_RESOURCE_TH);
    return parser;
  }

}
