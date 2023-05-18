/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Parser;
using HtmlParse;

using Optional;
using Xunit;

using System;

using System.Text.RegularExpressions;

namespace test.Parser;

public class ParsingTest
{
  private HtmlPipe _HtmlPipe;
  private ReplayWrapPipe _Pipe;

  private static readonly Regex
    __TableReg = new Regex(pattern: @"\<table.*\>"),
    __TRow = new Regex(pattern: @"\<tr\>"),
    __TRowAttrib = new Regex(pattern: @"\<tr\s?(\w+)=?\""(\d+)\""?\>"),
    __EndTRow = new Regex(pattern: @"\<\/tr\>"),
    __TdNoAttribPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]*)"),
    __TdRowspanPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]+)");
  private static Matching _Table(string token)
  {
    Matching result = Utils.NoMatch();

    var match = __TableReg.Match(token);
    if (match != null && match.Length > 0)
    {
      result = new Matching() {
        MatcherName = nameof(_Table).Some(),
        MatchResult = MatchKind.SingularMatch,
        Parts = Utils.GetParts(match)
      };
    }
    return result;
  }

  public ParsingTest()
  {
    _HtmlPipe = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Pipe = new ReplayWrapPipe(_HtmlPipe);
  }

  [Fact]
  public void MatchesTableSkippingEarlierTags()
  {
    Parsing.Group(_Pipe)
      .SkipUntil(_Table)
      .Expect(_Table)
      .Then((matchingList,writer) => {
        Assert.Single(matchingList);

        var m = matchingList.First;
        if (m != null)
        {
          Matching mm = m.Value;
          writer.WriteFragmentLine($"Matched by: {mm.MatcherName}, ");
          mm.Parts.MatchSome( list => {
            var i = list.First;
            for(int index = 0; i != null && index < list.Count; index++)
            {
              var node = i != null ? i.Value : null;
              if (node != null)
                writer.WriteFragmentLine($"attrib: {node}");
              i = i != null ? i.Next : null;
            }
          });
        }
      });
  }

  [Fact]
  public void CreateMatcherFromRegex()
  {
    Matcher table = Utils.SingularMatchRule(__TableReg, "Table");
    Parsing.Group(_Pipe)
      .SkipUntil(table)
      .Expect(table)
      .Then((list, writer) => {
        Assert.Single(list);
        if (list.First != null)
        {
          Matching matching = list.First.Value;
          Assert.True(matching.IsMatch);
          matching.MatcherName.MatchSome( nm => Assert.Equal("Table", nm) );
        }
      });
  }

  [Fact]
  public void ParseAction_rewindsPipeOnElseForOtherMatch()
  {
    bool isExpectedHandlerUsed = false;

    Matcher
      _table = Utils.SingularMatchRule(HtmlPatterns.TABLE, name: "table"),
      _endTable = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end-table"),
      _tr = Utils.SingularMatchRule(HtmlPatterns.TR, "tr-only"),
      _trAll = Utils.ShortLongMatchRules(HtmlPatterns.TR, HtmlPatterns.TR_ATTRIB, "tr-all"),
      _endtr = Utils.SingularMatchRule(HtmlPatterns.END_TR, name: "end-tr"),
      _thead = Utils.SingularMatchRule(HtmlPatterns.THEAD, "thead"),
      _endThead = Utils.SingularMatchRule(HtmlPatterns.END_THEAD, "end-thead");
    
    Parsing.Group(_Pipe)
      .SkipUntil(_table)
      .Expect(_table)
      .Expect(_tr)
      .Then( (list, writer) => {
        Assert.True(false, userMessage: "No match - then block should be skipped");
      })
      .Else()
        .SkipUntil(_table)
        .Expect(_table)
        .Expect(_thead)
        .Expect(_tr)
        .Then((list, writer) => {
          Assert.Equal(expected: 3, list.Count);
          isExpectedHandlerUsed = true;
        });
    Assert.True(isExpectedHandlerUsed, "Expected handler must have been called.");
  }
}