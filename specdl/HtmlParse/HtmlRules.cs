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
  public const int TH_VALUE_INDEX = 0;
  
  public static readonly Matcher
    START_TABLE = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, name:"start:table"),
    END_TABLE = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end:table"),
    START_THEAD = Utils.SingularMatchRule(HtmlPatterns.THEAD, name: "start:thead"),
    END_THEAD = Utils.SingularMatchRule(HtmlPatterns.END_THEAD, name: "end:thead"),
    START_TH_VALUE = Utils.SingularMatchRule(HtmlPatterns.TH_VALUE, name: "start:th-value"),
    END_TH = Utils.SingularMatchRule(HtmlPatterns.END_TH, name: "end:th"),
    START_TR = Utils.ShortLongMatchRules(HtmlPatterns.TR, HtmlPatterns.TR_ATTRIB, name: "start:tr"),
    END_TR = Utils.SingularMatchRule(HtmlPatterns.END_TR, name: "end:tr"),
    START_TD_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_VALUE, name: "start:td-value"),
    START_TD_ATTRIB_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_ATTRIB_VALUE, name: "start:td-attrib-value"),
    END_TD = Utils.SingularMatchRule(HtmlPatterns.END_TD, name: "end:td"),
    START_A_ID = Utils.SingularMatchRule(HtmlPatterns.A_ID, name: "start:a-id"),
    START_A_HREF = Utils.SingularMatchRule(HtmlPatterns.A_HREF, name: "start:a-href"),
    END_A = Utils.SingularMatchRule(HtmlPatterns.END_A, name: "end:a"),
    START_PARA = Utils.SingularMatchRule(HtmlPatterns.PARA, name: "start:p"),
    END_PARA = Utils.SingularMatchRule(HtmlPatterns.END_PARA, name: "end:p");
}