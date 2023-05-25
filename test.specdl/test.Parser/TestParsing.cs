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
using System.Collections.Generic;
using System.Linq;

namespace test.Parser;

public class TestParsing
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
        MatcherName = nameof(_Table),
        MatchResult = MatchKind.SingularMatch,
        Parts = Utils.GetParts(match)
      };
    }
    return result;
  }

  public TestParsing()
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
          Assert.Equal("Table", matching.MatcherName);
        }
      });
  }

  [Fact]
  public void ParseAction_rewindsPipeOnElseForOtherMatch()
  {
    bool isExpectedHandlerUsed = false;

    Matcher
      _table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, name: "table"),
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

  readonly Matcher
    TABLE = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB,name: "table"),
    END_TABLE = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end:table"),
    TR = Utils.ShortLongMatchRules(HtmlPatterns.TR, HtmlPatterns.TR_ATTRIB, name: "tr"),
    END_TR = Utils.SingularMatchRule(HtmlPatterns.END_TR, name: "end:tr"),
    TH = Utils.SingularMatchRule(HtmlPatterns.TH_VALUE, name: "th"),
    END_TH = Utils.SingularMatchRule(HtmlPatterns.END_TH, name: "end:th");

  [Fact]
  public void ParseAction_SkipUntil_canBeCalledManyTimesToSkipAhead()
  {
    List<string> Annotations = new List<string>();
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE);
    
    var parser2 = parser.Expect(TABLE, annotation: "p2:table");

    var parser3 = parser2
      .SkipUntil(TR)
      .Expect(TR, annotation: "p3:tr");
    
    parser3.Then( (list, writer) => {
      var annotated =
        from node in list
        where node.HasAnnotation
        select node;
      for(int index = 0; index < annotated.Count(); index++)
      {
        Annotations.Add(annotated.ElementAt(index).Annotation);
      }
    });

    Assert.True(Annotations.Count > 0);
    Assert.Equal(expected: "p2:table", Annotations[0]);
    Assert.Equal(expected: "p3:tr", Annotations[1]);
  }

  [Fact]
  public void ParseAction_SkipUntil_scansAllThreeTablesAndAnotherSkipRunsToEnd()
  {
    List<string> Annotations = new List<string>();
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .Expect(TABLE, annotation:"table:1")
      .SkipUntil(TABLE);

      Assert.False(_Pipe.IsInFlowEnded);
      parser.Expect(TABLE, annotation:"table:2")
      .SkipUntil(TABLE)
      .Expect(TABLE, annotation: "table:3")
      .SkipUntil(TABLE)
      .Then((list,writer)=>{
        var found =
          from node in list
          where node.HasAnnotation
          select node;
        for(int index = 0; index < found.Count(); index++)
        {
          Annotations.Add(found.ElementAt(index).Annotation);
        }
      });
    Assert.True(_Pipe.IsInFlowEnded);
    Assert.Equal(expected: 3, Annotations.Count);
    Assert.Collection(Annotations,
      t1 => Assert.Equal(expected:"table:1", t1),
      t2 => Assert.Equal(expected:"table:2", t2),
      t3 => Assert.Equal(expected:"table:3", t3)
    );
  }

  [Fact]
  public void ParseAction_If_falseCondition_continues_same()
  {
    bool isIfClauseDone = false;

    List<string> Annotations = new List<string>();
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .Expect(TABLE)
      .SkipUntil(TR)
      .Expect(TR, annotation: "tr:headings")
      .Expect(TH, annotation: "th:action")
      .Expect(END_TH, annotation:"th:action:end")
      .SkipUntil(END_TR)
      .Expect(END_TR);
    
    parser.If(
      list => {
        var query =
          from node in list
          where node.HasAnnotation && node.Annotation == "th:description"
          select node;
        return query.Count() > 0;
      }, pp => {
        isIfClauseDone = true;
        return pp;
      });

    parser.Then( (list, writer) => {
      var annotations =
        from node in list
        where node.HasAnnotation
        select node;
      annotations.ForEach((m,i) => Annotations.Add(m.Annotation));
    });
    Assert.Collection( Annotations,
      a1 => Assert.Equal(expected: "tr:headings", a1),
      a2 => Assert.Equal(expected: "th:action", a2),
      a3 => Assert.Equal(expected: "th:action:end", a3)
    );

    Assert.False(isIfClauseDone);
  }

  [Fact]
  public void ParseAction_If_trueCondition_doesBranchedParsing()
  {
    bool isIfClauseDone = false;

    List<string> Annotations = new List<string>();
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .Expect(TABLE)
      .SkipUntil(TR)
      .Expect(TR, annotation: "tr:headings")
      .Expect(TH, annotation: "th:action")
      .Expect(END_TH, annotation:"th:action:end")
      ;
    
    parser.If(
      list => {
        var query =
          from node in list
          where node.HasAnnotation && node.Annotation == "th:action"
          select node;
        return query.Count() > 0;
      }, pp => {
        isIfClauseDone = true;
        return pp
          .Expect(TH, annotation: "th:description")
          .Expect(END_TH, annotation: "th:description:end")
        ;
      });

    parser.Then( (list, writer) => {
      var annotations =
        from node in list
        where node.HasAnnotation
        select node;
      annotations.ForEach((m,i) => Annotations.Add(m.Annotation));
    });

    Assert.True(isIfClauseDone);
    Assert.Collection( Annotations,
      a1 => Assert.Equal(expected: "tr:headings", a1),
      a2 => Assert.Equal(expected: "th:action", a2),
      a3 => Assert.Equal(expected: "th:action:end", a3),
      a4 => Assert.Equal(expected: "th:description", a4),
      a5 => Assert.Equal(expected: "th:description:end", a5)
    );
  }
}