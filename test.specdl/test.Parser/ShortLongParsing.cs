/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Xunit;

using Optional;

using Parser;
using HtmlParse;

using System;
using System.Text.RegularExpressions;

namespace test.Parser;

public class ShortLongParsing
{
  private HtmlPipe _Html;
  private ReplayWrapPipe _Pipe;

  public ShortLongParsing()
  {
    _Html = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Pipe = new ReplayWrapPipe(_Html);
  }

  [Fact]
  public void CanMatchShortAndLongTR()
  {
    Matching result;
    Regex
      trRule = HtmlTablePatterns.TR,
      trAttribsRule = HtmlTablePatterns.TR_ATTRIB;

    Matcher tr = Utils.ShortLongMatchRules(trRule, trAttribsRule, name: "tr");

    
    result = tr.Invoke("<tr>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.ShortMatch, result.MatchResult);
    Assert.False(result.Parts.HasValue);

    result = tr.Invoke(token: "<tr rowspan=\"10\">");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.LongMatch, result.MatchResult);
    Assert.True(result.Parts.HasValue);
    result.Parts.MatchSome( list => {
      var itr = list.GetEnumerator();
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "rowspan", itr.Current);
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "10", itr.Current);
    });
  }

  [Fact]
  public void CanMatchShortAndLongTD()
  {
    Matching result;
    Regex
      tdRule = HtmlTablePatterns.TD,
      tdAttribsRule = HtmlTablePatterns.TD_ATTRIB;

    Matcher tr = Utils.ShortLongMatchRules(tdRule, tdAttribsRule, name: "td");

    
    result = tr.Invoke("<td>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.ShortMatch, result.MatchResult);
    Assert.False(result.Parts.HasValue);
    Assert.True(result.MatcherName.HasValue);
    result.MatcherName.MatchSome(name => Assert.Equal(expected:"td", name));

    result = tr.Invoke(token: "<td rowspan=\"99\">");
    Assert.True(result.IsMatch);
    Assert.True(result.MatcherName.HasValue);
    result.MatcherName.MatchSome(name => Assert.Equal(expected:"td", name));
    Assert.Equal(MatchKind.LongMatch, result.MatchResult);
    Assert.True(result.Parts.HasValue);
    result.Parts.MatchSome( list => {
      var itr = list.GetEnumerator();
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "rowspan", itr.Current);
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "99", itr.Current);
    });
  }
}