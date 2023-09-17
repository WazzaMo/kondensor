/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Text.RegularExpressions;


namespace kondensor.Parser.AwsHtmlParse.Frag;

public static class FragmentsHtml
{
  public static readonly Regex
    TAG_CLOSE = new Regex(pattern: ">"),
    START_TABLE = new Regex(pattern: "<table"),
    END_TABLE = new Regex(pattern: "</table"),
    START_THEAD = new Regex(pattern: "<thead"),
    END_THEAD = new Regex(pattern: "</thead"),
    START_TR = new Regex(pattern: "<tr"),
    END_TR = new Regex(pattern: "</tr"),
    START_TH = new Regex(pattern: "<th"),
    END_TH = new Regex(pattern: "</th"),
    START_TD = new Regex(pattern: "<td"),
    END_TD = new Regex(pattern: "</td"),
    START_P = new Regex(pattern: "<p"),
    END_P = new Regex(pattern: "</p"),
    START_A = new Regex(pattern: "<a"),
    END_A = new Regex(pattern: "</a"),
    START_CODE = new Regex(pattern: "<code"),
    END_CODE = new Regex(pattern: "</code"),
    TAG_VALUE = new Regex(pattern: "(?<tagValue>.*)"),
    ACTIONS_TAG_VALUE = new Regex(pattern: "(?<tagValue>Actions)"),
    ID_VALUE = new Regex( pattern: "id=\"(?<idValue>.*)\"" ),
    HREF_VALUE = new Regex(pattern: "href=\"(?<hrefValue>.*)\""),
    ROWSPAN_VALUE = new Regex(
      pattern: "rowspan=\"(?<rowspanValue>[*\\-*\\d] *)\""
    );
}