/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using HtmlParse;
using Xunit;

using System.Text.RegularExpressions;

namespace test.HtmlParse;

public class TestHtmlTablePatterns
{
  [Fact]
  public void match_table_variations()
  {
    var match = HtmlTablePatterns.TABLE.Match("<table>");
    Assert.True(match != null && match.Length > 0);
    Assert.Equal(1, match?.Groups.Count);

    match = HtmlTablePatterns.TABLE.Match("<table id=\"foo\">");
    Assert.True(match != null && match.Length > 0);
    Assert.Equal(1, match?.Groups.Count);
  }

  [Fact]
  public void match_end_table()
  {
    Match match = HtmlTablePatterns.END_TABLE.Match("</table>");
    Assert.True(match.Length > 0);
    Assert.Single(match.Groups);
  }

  [Fact]
  public void match_thead()
  {
    Match match = HtmlTablePatterns.THEAD.Match(input: "<thead>");
    CheckMatches(count: 1, match);
  }

  [Fact]
  public void match_end_thead()
  {
    Match match = HtmlTablePatterns.END_THEAD.Match(input: "</thead>");
    CheckMatches(count: 1, match);
  }

  [Fact]
  public void match_tr_variations()
  {
    Match match;
    match = HtmlTablePatterns.TR.Match("<tr>");
    CheckMatches(1, match);

    match = HtmlTablePatterns.TR_ATTRIB.Match("<tr rowspan=\"3\">");
    CheckMatches(3, match);
  }

  [Fact]
  public void match_td_variations()
  {
    Match match;
    match = HtmlTablePatterns.TD.Match(input:"<td>");
    CheckMatches(1, match);

    match = HtmlTablePatterns.TD_ATTRIB.Match(input: "<td rowspan=\"4\">");
    CheckMatches(count: 3, match);
  }

  [Fact]
  public void no_match_td_attrib_mismatched()
  {
    Match match;
    match = HtmlTablePatterns.TD.Match(input: "<td rowspan=\"3\">");
    Assert.False(match.Length > 0);
    Assert.Single(match.Groups);
    Assert.Equal(0, match.Groups[0].Length);

    match = HtmlTablePatterns.TD_ATTRIB.Match(input: "<td>");
    Assert.Equal(0, match.Groups[0].Length);
    Assert.Equal(0, match.Groups[1].Length);
    Assert.Equal(0, match.Groups[2].Length);
  }

  private void CheckMatches(int count, Match match)
  {
    Assert.True(match.Length > 0);
    Assert.Equal(count, match.Groups.Count);
  }
}