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
    START_TD_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD_VALUE, HtmlPatterns.TD_ATTRIB_VALUE, name: "start:td-value"),
    // START_TD_VALUE = Utils.SingularMatchRule(HtmlPatterns.TD_VALUE, name: "start:td-value"),
    START_TD_ATTRIB_VALUE = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_ATTRIB_VALUE, name: "start:td-attrib-value"),
    END_TD = Utils.SingularMatchRule(HtmlPatterns.END_TD, name: "end:td"),
    START_A_ID = Utils.SingularMatchRule(HtmlPatterns.A_ID, name: "start:a-id"),
    START_A_HREF = Utils.SingularMatchRule(HtmlPatterns.A_HREF, name: "start:a-href"),
    END_A = Utils.SingularMatchRule(HtmlPatterns.END_A, name: "end:a"),
    START_PARA = Utils.SingularMatchRule(HtmlPatterns.PARA,name: "start:p"),
    START_PARA_VALUE = Utils.SingularMatchRule(HtmlPatterns.PARA_VALUE, name: "start:p-value"),
    END_PARA = Utils.SingularMatchRule(HtmlPatterns.END_PARA, name: "end:p");
}