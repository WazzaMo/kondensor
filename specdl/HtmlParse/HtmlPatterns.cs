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
    END_TD = new Regex(pattern: @"\<\/td\>"),
    TR = new Regex(pattern: @"\<tr\>"),
    TR_ATTRIB = new Regex(pattern: @"\<tr\s?(\w+)=?\""(\d+)\""?\>"),
    END_TR = new Regex(pattern: @"\<\/tr\>"),
    A_ID = new Regex(pattern: @"\<a id=\""([\-\w\s]+)\""\>$"),
    A_HREF = new Regex(pattern: @"\<a href=""([*:\-_./#*\w\s]+)""\>(.*)"),
    END_A = new Regex(pattern: @"\<\/a\>"),
    PARA = new Regex(pattern: @"\<p\>"),
    END_PARA = new Regex(pattern: @"\<\/p\>")
    ;


}