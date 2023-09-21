/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Xunit;

using kondensor.Pipes;

using System;

namespace test.kondensor.pipes;

public class TestPreprocessorUtils
{
  [Fact]
  public void IsContentEqual_matches_shortest_common_chars()
  {
    Span<char> seg1, seg2;

    seg1 = SpanFor("abcd");
    seg2 = SpanFor("ab");
    Assert.True(PreprocessorUtils.IsContentEqual(seg1, seg2));

    seg1 = SpanFor("1234 abcd");
    seg2 = SpanFor("1234 abc");
    Assert.True(PreprocessorUtils.IsContentEqual(seg1, seg2));
  }

  [Fact]
  public void IsContentEqual_Does_not_match_unequal_early_chars()
  {
    Span<char> seg1 = SpanFor("xyz");
    Span<char> seg2 = SpanFor("*xyz");
    Assert.False(PreprocessorUtils.IsContentEqual(seg1, seg2));

    seg1 = SpanFor("abcd");
    seg2 = SpanFor("ab1");
    Assert.False(PreprocessorUtils.IsContentEqual(seg1, seg2));
  }

  [Fact]
  public void FindNextMatch_finds_first_match()
  {
    Span<char> text = SpanFor("abc def longganisa is yummy");
    Span<char> search = SpanFor("longganisa");
    const int findLongganisa = 8;
    Assert.True(PreprocessorUtils.FindNextMatch(text, search, 0, out int longganisaPos));
    Assert.Equal(findLongganisa, longganisaPos);
  }

  [Fact]
  public void FindNextMatch_finds_first_and_second_match()
  {
                                //    0       8       17       26       35
    Span<char> text = SpanFor("abc def longganisa is yummy; I eat longganisa...");
    Span<char> search = SpanFor("longganisa");
    const int index1 = 8, index2 = 35;
    Assert.True(PreprocessorUtils.FindNextMatch(text, search, 0, out int longganisaPos));
    Assert.Equal(index1, longganisaPos);
    Assert.True(PreprocessorUtils.FindNextMatch(text, search, longganisaPos + 1, out longganisaPos));
    Assert.Equal(index2, longganisaPos);
  }

  [Fact]
  public void CountMatches_finds_and_counts_correctly()
  {
                                //    0       8       17       26       35
    Span<char> textWith1 = SpanFor("abc def longganisa...");
    Span<char> textWith2 = SpanFor("abc def longganisa is yummy; I eat longganisa...");
    Span<char> search = SpanFor("longganisa");

    Assert.Equal(expected: 1, PreprocessorUtils.CountMatches(textWith1, search));
    Assert.Equal(expected: 2, PreprocessorUtils.CountMatches(textWith2, search));
  }

  [Fact]
  public void ReplaceTextCut_does_not_replace_mismatch()
  {
    string Text = "Hi there <span>Captain Strange</span>";
    string Expected1 = "Hi there <h1>Captain Strange</span>";
    string Expected2 = "Hi there <h1>Captain Strange</h1>";
    string Replacement = "h1",
          Search = "span";

    Span<char> text = SpanFor(Text);
    Span<char> search = SpanFor(Search);
    Span<char> replacement = SpanFor(Replacement);
    char[] buffer = new char[ text.Length + replacement.Length - search.Length ];
    Span<char> target = new Span<char>(buffer);

    Assert.True(PreprocessorUtils.FindNextMatch(text, search, 0, out int cut));
    Assert.True(PreprocessorUtils.ReplaceTextCut(text, replacement, cut, search.Length, target, out int targetLength));
    Assert.Equal(Expected1, target.ToString());

    Assert.True(PreprocessorUtils.FindNextMatch(target, search, cut + 1, out cut));
    Assert.True(PreprocessorUtils.ReplaceTextCut(target, replacement, cut, search.Length, target, out targetLength));
    target = target.Slice(0, targetLength);
    Assert.Equal(Expected2, target.ToString());
  }

  [Fact]
  public void ReplaceFull_replaces_all_instances_of_search_text()
  {
    string Text = "Hi there <span>Captain Strange</span>";
    string ExpectedShorterReplace = "Hi there <h1>Captain Strange</h1>";
    string ExpectedLongerReplace = "Hi there <amazing>Captain Strange</amazing>";
    string ReplacementShorter = "h1",
      ReplacementLonger = "amazing",
      Search = "span";

    Span<char> text = SpanFor(Text);
    Span<char> search = SpanFor(Search);
    Span<char> shortReplacement = SpanFor(ReplacementShorter);
    Span<char> longReplacement = SpanFor(ReplacementLonger);

    var result = PreprocessorUtils.ReplaceFull(text, search, shortReplacement);
    Assert.Equal(ExpectedShorterReplace, result.ToString());

    result = PreprocessorUtils.ReplaceFull(text, search, longReplacement);
    Assert.Equal(ExpectedLongerReplace, result.ToString());
  }

  private Span<char> SpanFor(string value)
    => new Span<char>(value.ToCharArray());
}