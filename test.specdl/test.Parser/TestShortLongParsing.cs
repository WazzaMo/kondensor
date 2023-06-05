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

public class TestShortLongParsing
{

  public TestShortLongParsing()
  {}

  [Fact]
  public void SimpleMatcher_givesDetailedResult_onMismatch()
  {
    Matcher subject = Utils.SingularMatchRule(HtmlPatterns.TR, name:"tr");
    Matching result = subject.Invoke("something-else");

    Assert.Equal(MatchKind.Mismatch, result.MatchResult);
    Assert.Equal(expected: "tr", result.MatcherName);
  }

  [Fact]
  public void SimpleMatcher_givesDetailedResult_onMatch()
  {
    Matcher subject = Utils.SingularMatchRule(HtmlPatterns.TR, name:"tr");
    Matching result = subject.Invoke("<tr>");

    Assert.Equal(MatchKind.SingularMatch, result.MatchResult);
    Assert.Equal(expected: "tr", result.MatcherName );
  }

  [Fact]
  public void CanMatchShortAndLongTR()
  {
    Matching result;
    Regex
      trRule = HtmlPatterns.TR;

    Matcher tr = Utils.SingularMatchRule(trRule, name: "tr");

    
    result = tr.Invoke("<tr>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.SingularMatch, result.MatchResult);
    Assert.True(result.Parts.HasValue);
    Assert.False(result.Parts.Exists(parts => parts.Count > 0));
  }

  [Fact]
  public void CanMatchShortAndLongTD()
  {
    Matching result;
    Regex
      tdRule = HtmlPatterns.TD,
      tdAttribsRule = HtmlPatterns.TD_ATTRIB_VALUE;

    Matcher tr = Utils.ShortLongMatchRules(tdRule, tdAttribsRule, name: "td");

    
    result = tr.Invoke("<td>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.ShortMatch, result.MatchResult);
    Assert.True(result.Parts.HasValue);
    Assert.True(result.HasName);
    Assert.Equal(expected:"td", result.MatcherName);
    Assert.False(result.HasAnnotation); // no annotation set

    result = tr.Invoke(token: "<td rowspan=\"99\">");
    Assert.True(result.IsMatch);
    Assert.True(result.HasName);
    Assert.Equal(expected:"td", result.MatcherName);
    Assert.Equal(MatchKind.LongMatch, result.MatchResult);
    Assert.False(result.HasAnnotation); // no annotation set
    Assert.True(result.Parts.HasValue);
    result.Parts.MatchSome( list => {
      var itr = list.GetEnumerator();
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "rowspan", itr.Current);
      Assert.True(itr.MoveNext());
      Assert.Equal(expected: "99", itr.Current);
    });
  }

  [Fact]
  public void Annotation_appearsInSingularMatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher table = Utils.SingularMatchRule(HtmlPatterns.TABLE, "table", annotation: EXPECTED_ANNOTATION);

    result = table.Invoke("<table>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.SingularMatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }

  [Fact]
  public void Annotation_appearsInSingularMisMatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher tr = Utils.SingularMatchRule(HtmlPatterns.TR, "tr", annotation: EXPECTED_ANNOTATION);

    result = tr.Invoke(token: "rodent");
    Assert.False(result.IsMatch);
    Assert.Equal(MatchKind.Mismatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: "tr", result.MatcherName);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }

  [Fact]
  public void Annotation_appearsInShortMatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, "table", annotation: EXPECTED_ANNOTATION);

    result = table.Invoke("<table>");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.ShortMatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }

  [Fact]
  public void Annotation_appearsInShortMatch_mismatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, "table", annotation: EXPECTED_ANNOTATION);

    result = table.Invoke(token: "no matching");
    Assert.False(result.IsMatch);
    Assert.Equal(MatchKind.Mismatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }

  [Fact]
  public void Annotation_appearsInLongMatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, "table", annotation: EXPECTED_ANNOTATION);

    result = table.Invoke("<table id=\"floppybunny\">");
    Assert.True(result.IsMatch);
    Assert.Equal(MatchKind.LongMatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }

  [Fact]
  public void Annotation_appearsInLongMatch_mismatch()
  {
    const string EXPECTED_ANNOTATION = "__test";
    Matching result;
    Matcher table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, "table", annotation: EXPECTED_ANNOTATION);

    result = table.Invoke(token: "bad token");
    Assert.False(result.IsMatch);
    Assert.Equal(MatchKind.Mismatch, result.MatchResult);
    Assert.True(result.HasAnnotation);
    Assert.Equal(expected: EXPECTED_ANNOTATION, result.Annotation);
  }
}