/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using System.Text.RegularExpressions;

using Parser;

namespace HtmlParse;

public static class HtmlRules
{  
  public static readonly Matcher
    START_TABLE = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, name:"start:table"),
    END_TABLE = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end:table"),
    START_THEAD = Utils.SingularMatchRule(HtmlPatterns.THEAD, name: "start:thead"),
    END_THEAD = Utils.SingularMatchRule(HtmlPatterns.END_THEAD, name: "end:thead"),
    START_TH_VALUE = Utils.SingularMatchRule(HtmlPatterns.TH_VALUE, name: "start:th-value"),
    END_TH = Utils.SingularMatchRule(HtmlPatterns.END_TH, name: "end:th"),
    START_TR = Utils.SingularMatchRule(HtmlPatterns.TR, name: "start:tr"),
    END_TR = Utils.SingularMatchRule(HtmlPatterns.END_TR, name: "end:tr"),
    START_TD = Utils.SingularMatchRule(HtmlPatterns.TD, name: "start:td"),
    START_TD_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD_VALUE, HtmlPatterns.TD_ATTRIB_VALUE, name: "start:td-value"),
    START_TD_ATTRIB_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_ATTRIB_VALUE, name: "start:td-attrib-value"),
    END_TD = Utils.SingularMatchRule(HtmlPatterns.END_TD, name: "end:td"),
    START_A_ID = Utils.SingularMatchRule(HtmlPatterns.A_ID, name: "start:a-id"),
    START_A_HREF = Utils.ShortLongMatchRules(
      HtmlPatterns.A_HREF,
      HtmlPatterns.A_HREF_LONG, // covers cases where other attributes were given
      name: "start:a-href"
    ),
    END_A = Utils.SingularMatchRule(HtmlPatterns.END_A, name: "end:a"),
    START_PARA = Utils.SingularMatchRule(HtmlPatterns.PARA,name: "start:p"),
    START_PARA_VALUE = Utils.SingularMatchRule(HtmlPatterns.PARA_VALUE, name: "start:p-value"),
    END_PARA = Utils.SingularMatchRule(HtmlPatterns.END_PARA, name: "end:p"),
    START_CODE_ATTRIB_VALUE = Utils.SingularMatchRule(HtmlPatterns.CODE, name:"start:code-attrib-value"),
    END_CODE = Utils.SingularMatchRule(HtmlPatterns.END_CODE, name: "end:code"),
    START_SPAN = Utils.SingularMatchRule(HtmlPatterns.SPAN, name: "start:span"),
    END_SPAN = Utils.SingularMatchRule(HtmlPatterns.END_SPAN, name: "end:span"),
    BODY_CODE_TEXT = Utils.SingularMatchRule(HtmlPatterns.CODE_TEXT, name: "body:code-text"),
    START_H6_VALUE = Utils.SingularMatchRule(HtmlPatterns.H6, name: "start:h6:value"),
    END_H6 = Utils.SingularMatchRule(HtmlPatterns.END_H6, name: "end:h6"),
    START_UL = Utils.SingularMatchRule(HtmlPatterns.UL, name: "start:ul"),
    END_UL = Utils.SingularMatchRule(HtmlPatterns.END_UL, "end:ul"),
    START_LI = Utils.SingularMatchRule(HtmlPatterns.LI, name: "start:li"),
    END_LI = Utils.SingularMatchRule(HtmlPatterns.END_LI, name: "end:li"),
    START_BOLD = Utils.SingularMatchRule(HtmlPatterns.BOLD_VALUE, name: "start:bold"),
    END_BOLD = Utils.SingularMatchRule(HtmlPatterns.END_BOLD, name: "end:bold"),
    TEXT = Utils.SingularMatchRule(HtmlPatterns.TEXT, name: "text-data")
    ;
}