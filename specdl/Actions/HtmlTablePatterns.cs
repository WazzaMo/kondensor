/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Actions;

public static class HtmlTablePatterns
{
  public static readonly Regex
    TABLE = new Regex(pattern: @"\<table.*\>"),
    END_TABLE = new Regex(pattern: @"\<\/table\>"),
    TR = new Regex(pattern: @"\<tr\>"),
    TD = new Regex(pattern: @"\<td\>"),
    TD_ATTRIB = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>"),
    END_TD = new Regex(pattern: @"\<\/td\>"),
    TR_ATTRIB = new Regex(pattern: @"\<tr\s?(\w+)=?\""(\d+)\""?\>"),
    END_TR = new Regex(pattern: @"\<\/tr\>");

}