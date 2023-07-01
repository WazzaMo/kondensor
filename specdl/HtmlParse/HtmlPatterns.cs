/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlParse;

public static class HtmlPatterns
{
  public static readonly Regex
    TABLE = new Regex(pattern: @"\<table\>"),
    TABLE_ATTRIB = new Regex(pattern: @"\<table (\w+)=\W(\w+)\W\>"),
    END_TABLE = new Regex(pattern: @"\<\/table\>"),
    THEAD = new Regex(pattern: @"\<thead\>"),
    END_THEAD = new Regex(pattern: @"\<\/thead\>"),
    TH_VALUE = new Regex(pattern: @"\<th\>([\w\s\(\*\)]+)"),
    END_TH = new Regex(pattern: @"\<\/th\>"),
    TD = new Regex(pattern: @"\<td\>"),
    TD_ATTRIB_VALUE = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>([\w\s\(\*\)]*)"),
    TD_VALUE = new Regex(pattern: @"\<td\>([\w\s\(\*\)]*)"),
    END_TD = new Regex(pattern: @"\<\/td\>"),
    TR = new Regex(pattern: @"\<tr\>"),
    END_TR = new Regex(pattern: @"\<\/tr\>"),
    A_ID = new Regex(pattern: @"\<a id=\""([\-\w\s]+)\""\>$"),
    A_HREF = new Regex(pattern: @"\<a href=""([*:\-_./#*\w\s]+)""\>(.*)"),
    END_A = new Regex(pattern: @"\<\/a\>"),
    PARA = new Regex(pattern: @"\<p\>"),
    PARA_VALUE = new Regex(pattern: @"\<p\>([*:*\w\s\(\*\)]+)"),
    END_PARA = new Regex(pattern: @"\<\/p\>"),
    CODE = new Regex(pattern: @"\<code (\w+)=\""(\w+)\""\>([*:${/}\-*\w\s\(\*\)]*)"),
    END_CODE = new Regex(pattern: @"\<\/code\>"),
    CODE_TEXT = new Regex(pattern: @"([*:${/}\-*\w\s\(\*\)]*)"),
    SPAN = new Regex(pattern: @"\<span\>{"),
    END_SPAN = new Regex(pattern: @"\<\/span\>")
    ;

  public const int
    TABLE_ATTRIB_NAME_IDX = 0,
    TABLE_ATTRIB_VALUE_IDX = 1,
    TH_VALUE_INDEX_IDX = 0,
    TD_ATTRIB_NAME_IDX = 0,
    TD_ATTRIB_VALUE_IDX = 1,
    TD_ATTRIB_TAG_VALUE_IDX = 2,
    TD_TAG_VALUE_IDX = 0,
    A_ID_VALUE_IDX = 0,
    A_HREF_ATTRIB_VALUE_IDX = 0,
    A_HREF_TAG_VALUE_IDX = 1,
    PARA_VALUE_IDX = 0,
    CODE_TAG_VALUE = 2;

}