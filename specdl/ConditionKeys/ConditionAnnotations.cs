/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;
using kondensor.Parser.HtmlParse;

namespace ConditionKeys;

public static class ConditionAnnotations
{
  internal const string
    S_TABLE_CK = "start:table:ck",
    E_TABLE_CK = "end:table:ck",
    S_THEAD = "start:thead:ck",
    E_THEAD = "end:thead:ck",
    S_TR_HDG = "start:tr:ck",
    E_TR_HDG = "end:tr:ck",
    S_TH_VALUE = "start:th:ck:value",
    E_TH = "end:th:ck",
    S_TR_DECL = "start:tr:ck:decl",
    E_TR_DECL = "end:tr:ck:decl",
    S_TD = "start:td:ck",
    E_TD = "end:td:ck",
    S_AID = "start:a-id:ck",
    E_AID = "ebd:a-id:ck",
    S_AHREF = "start:a-href:ck",
    E_AHREF = "end:a-href:ck",
    S_TD_DESC = "start:td-desc:ck",
    E_TD_DESC = "end:td-desc:ck",
    S_TD_TYPE = "start:td-type:ck",
    E_TD_TYPE = "end:td-type:ck",
    S_AWSICON = "start:awsui-icon:opt",
    E_AWSICON = "end:awsui-icon:opt"
    ;
}