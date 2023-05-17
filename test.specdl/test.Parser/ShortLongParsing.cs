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

  public ShortLongParsing()
  {}

  [Fact]
  public void SimpleMatcher_givesDetailedResult_onMismatch()
  {
    Matcher subject = Utils.SingularMatchRule(HtmlTablePatterns.TR, name:"tr");
    Matching result = subject.Invoke("something-else");

    Assert.Equal(MatchKind.Mismatch, result.MatchResult);
    result.MatcherName.MatchSome( name => Assert.Equal(expected: "tr", name) );
  }

  [Fact]
  public void SimpleMatcher_givesDetailedResult_onMatch()
  {
    Matcher subject = Utils.SingularMatchRule(HtmlTablePatterns.TR, name:"tr");
    Matching result = subject.Invoke("<tr>");

    Assert.Equal(MatchKind.SingularMatch, result.MatchResult);
    result.MatcherName.MatchSome( name => Assert.Equal(expected: "tr", name) );
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