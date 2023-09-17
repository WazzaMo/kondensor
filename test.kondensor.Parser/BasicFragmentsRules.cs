/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse.Frag;

using test.kondensor.fixtures;

using System;
using System.Text.RegularExpressions;
using kondensor.Parser.AwsHtmlParse;
using NuGet.Frameworks;

namespace test.kondensor.Parser;

public static class BasicFragmentsRules
{
  public static readonly Matcher
    TAG_CLOSE = Utils.SingularMatchRule(FragmentsHtml.TAG_CLOSE, name:"tag:close"),
    START_TABLE = Utils.SingularMatchRule(FragmentsHtml.START_TABLE, name: "start:table"),
    END_TABLE = Utils.SingularMatchRule(FragmentsHtml.END_TABLE, name: "end:table"),
    START_THEAD = Utils.SingularMatchRule(FragmentsHtml.START_THEAD, name: "start:thead"),
    END_THEAD = Utils.SingularMatchRule(FragmentsHtml.END_THEAD, name: "end:thead"),
    START_TR = Utils.SingularMatchRule(FragmentsHtml.START_TR, name: "start:tr"),
    END_TR = Utils.SingularMatchRule(FragmentsHtml.END_TR, name: "end:tr"),
    START_TH = Utils.SingularMatchRule(FragmentsHtml.START_TH, name: "start:th"),
    END_TH = Utils.SingularMatchRule(FragmentsHtml.END_TH, name: "end:th"),
    TAG_VALUE = Utils.NamedGroupRule(FragmentsHtml.TAG_VALUE, name: "tag:value"),
    ACTIONS_TAG_VALUE = Utils.NamedGroupRule(FragmentsHtml.ACTIONS_TAG_VALUE, name: "actions:tag:value"),
    START_TD = Utils.SingularMatchRule(FragmentsHtml.START_TD, name: "start:td"),
    END_TD = Utils.SingularMatchRule(FragmentsHtml.END_TD, name: "end:td"),
    START_A = Utils.SingularMatchRule(FragmentsHtml.START_A, name: "start:a"),
    END_A = Utils.SingularMatchRule(FragmentsHtml.END_A, name: "end:a"),
    ID_VALUE = Utils.NamedGroupRule(FragmentsHtml.ID_VALUE, name:"id:value"),
    HREF_VALUE = Utils.NamedGroupRule(FragmentsHtml.HREF_VALUE, name:"href:value"),
    ROWSPAN_VALUE = Utils.NamedGroupRule(FragmentsHtml.ROWSPAN_VALUE, name: "rowspan:value");
}