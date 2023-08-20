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

public static class ResourceTableDefinitionParser
{
  internal static ParseAction DefinitionRow(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, ResourceAnnotations.S_RESOURCE_TR)
        .Expect( IdAndRefProd )

        .Expect( ResourceTemplateProd )

        .Expect( ConditionKeysProd )
      .Expect(HtmlRules.END_TR, ResourceAnnotations.E_RESOURCE_TR)
      ;
    return parser;
  }

  private static ParseAction SkipAwsIcon(ParseAction parser)
    => parser
    .MayExpect(HtmlRules.START_AWSUIICON, ResourceAnnotations.S_AWSICON)
    .MayExpect(HtmlRules.END_AWSUIICON, ResourceAnnotations.E_AWSICON)
    ;

  private static ParseAction IdAndRefProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_TYPE)
          .EitherProduction(IdWithNameProd, IdProd )
          .Expect( OptHrefNameDocLinkProd )
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_TYPE)
      ;
  
  private static ParseAction IdProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_A_ID, ResourceAnnotations.S_A_ID)
        .Expect(HtmlRules.END_A, ResourceAnnotations.E_A_ID)
        ;

  private static ParseAction IdWithNameProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_A_ID, ResourceAnnotations.S_A_ID)
        .Expect(HtmlRules.END_A_WITH_TEXT, ResourceAnnotations.E_A_ID_TEXT)
        ;

  private static ParseAction OptHrefNameDocLinkProd(ParseAction parser)
    => parser
        .MayExpect(HtmlRules.START_A_HREF, ResourceAnnotations.S_A_HREF_NAME)
          .Expect(SkipAwsIcon)
        .MayExpect(HtmlRules.END_A, ResourceAnnotations.E_A_HREF_NAME)
        ;

  private static ParseAction ResourceTemplateProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_ARN)
          .Expect(HtmlRules.START_CODE_ATTRIB_VALUE, ResourceAnnotations.S_CODE)
            .ExpectProductionUntil(CodeTextAndSpans,
          HtmlRules.END_CODE, ResourceAnnotations.E_CODE)
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_ARN)
        ;

  private static ParseAction ConditionKeysProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_TD, ResourceAnnotations.S_DATA_ROW_CK)
          .ProductionWhileMatch( CkParagraphProd )
        .Expect(HtmlRules.END_TD, ResourceAnnotations.E_DATA_ROW_CK)
        ;

  private static ParseAction CkParagraphProd(ParseAction parser)
    => parser
        .Expect(HtmlRules.START_PARA,ResourceAnnotations.S_P_CONDKEY)
          .Expect(HtmlRules.START_A_HREF,ResourceAnnotations.S_A_CONDKEY_HREF)
          .Expect(HtmlRules.END_A, ResourceAnnotations.E_A_CONDKEY_HREF)
        .Expect(HtmlRules.END_PARA, ResourceAnnotations.E_P_CONDKEY)
        ;

  private static ParseAction CodeTextAndSpans(ParseAction parser)
    => parser
      .MayExpect(HtmlRules.BODY_CODE_TEXT, ResourceAnnotations.TEXT_CODE)
      ;

}