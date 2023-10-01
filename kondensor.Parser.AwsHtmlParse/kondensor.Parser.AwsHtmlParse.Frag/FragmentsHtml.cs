/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.Text.RegularExpressions;


namespace kondensor.Parser.AwsHtmlParse.Frag;

public static class FragmentsHtml
{
  public const string
    KEY_TAG_VALUE = "tagValue",
    KEY_ID_VALUE = "idValue",
    KEY_HREF_VALUE = "hrefValue",
    KEY_ROWSPAN_VALUE = "rowspanValue";

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
    TAG_VALUE = new Regex(pattern: "(?<"+ KEY_TAG_VALUE + ">.*)"),
    ACTIONS_TAG_VALUE = new Regex(pattern: "(?<" + KEY_TAG_VALUE + ">Actions)"),
    ID_VALUE = new Regex( pattern: "id=\"(?<" + KEY_ID_VALUE + ">.*)\"" ),
    HREF_VALUE = new Regex(pattern: "href=\"(?<" + KEY_HREF_VALUE + ">.*)\""),
    ROWSPAN_VALUE = new Regex(
      pattern: "rowspan=\"(?<" + KEY_ROWSPAN_VALUE + ">[*\\-*\\d] *)\""
    );
}