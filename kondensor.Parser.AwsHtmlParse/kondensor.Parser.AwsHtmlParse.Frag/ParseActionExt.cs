/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using kondensor.Parser;


namespace kondensor.Parser.AwsHtmlParse.Frag;

/// <summary>
/// Convenience extension methods for the <see href="ParseAction" />
/// type to make writing fragment HTML parsers easier.
/// </summary>
public static class ParseActionExt
{
  public const string
    ATN_CLOSE_TAG = "tag:close";

  public static ParseAction TagClose(this ParseAction parser)
    => parser.Expect(HtmlFragRules.TAG_CLOSE, ATN_CLOSE_TAG);
}