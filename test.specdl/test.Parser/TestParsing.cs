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
    Console.Out.Flush();
    _HtmlPipe = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Pipe = new ReplayWrapPipe(_HtmlPipe);
  }

  [Fact]
  public void MatchesTableSkippingEarlierTags()
  {
    Parsing.Group(_Pipe)
      .SkipUntil(_Table)
      .Expect(_Table)
      .AllMatchThen((matchingList,writer) => {
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
      .AllMatchThen((list, writer) => {
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
      .AllMatchThen( (list, writer) => {
        Assert.True(false, userMessage: "No match - then block should be skipped");
      })
      .MismatchesThen()
        .SkipUntil(_table)
        .Expect(_table)
        .Expect(_thead)
        .Expect(_tr)
        .AllMatchThen((list, writer) => {
          Assert.Equal(expected: 3, list.Count);
          isExpectedHandlerUsed = true;
        });
    Assert.True(isExpectedHandlerUsed, "Expected handler must have been called.");
  }

  readonly Matcher
    TABLE = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB,name: "table"),
    END_TABLE = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end:table"),
    THEAD = Utils.SingularMatchRule(HtmlPatterns.THEAD, name: "thead"),
    END_THEAD = Utils.SingularMatchRule(HtmlPatterns.END_THEAD, name: "end:thead"),
    TR = Utils.ShortLongMatchRules(HtmlPatterns.TR, HtmlPatterns.TR_ATTRIB, name: "tr"),
    END_TR = Utils.SingularMatchRule(HtmlPatterns.END_TR, name: "end:tr"),
    TH = Utils.SingularMatchRule(HtmlPatterns.TH_VALUE, name: "th"),
    END_TH = Utils.SingularMatchRule(HtmlPatterns.END_TH, name: "end:th"),
    TD = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_ATTRIB_VALUE, name: "td"),
    END_TD = Utils.SingularMatchRule(HtmlPatterns.END_TD, name: "end:td"),
    PARA = Utils.SingularMatchRule(HtmlPatterns.PARA, name: "p"),
    END_PARA = Utils.SingularMatchRule(HtmlPatterns.END_PARA, name: "end:p"),
    ANCHOR_HREF = Utils.SingularMatchRule(HtmlPatterns.A_HREF, name: "ahref"),
    ANCHOR_ID = Utils.SingularMatchRule(HtmlPatterns.A_ID, name: "aid"),
    END_ANCHOR = Utils.SingularMatchRule(HtmlPatterns.END_A, name: "end:ahref");

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
    
    parser3.AllMatchThen( (list, writer) => {
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
      .AllMatchThen((list,writer)=>{
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
    (Matching node) => node.HasAnnotation && node.Annotation == "th:description",
    (int numMatches) => numMatches > 0,
    (parser, nodes) => {
      isIfClauseDone = true;
      return parser;
    });

    parser.AllMatchThen( (list, writer) => {
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
      (Matching node) => node.HasAnnotation && node.Annotation == "th:action",
      (parser, nodes) => {
        if (nodes.Count() > 0)
        {
          isIfClauseDone = true;
          parser
          .Expect(TH, annotation: "th:description")
          .Expect(END_TH, annotation: "th:description:end");
        }
        return parser;
      });

    parser.AllMatchThen( (list, writer) => {
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

  [Fact]
  public void ParseAction_ExpectProductionNTimes_matches()
  {
    Production Heading = (ParseAction parser) => {
      return parser
        .Expect(TH, annotation: "heading")
        .Expect(END_TH, annotation: "end-th")
        ;
    };

    List<string> Annotations = new List<string>();
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .Expect(TABLE)
      .SkipUntil(TR)
      .Expect(TR, annotation: "tr:headings")
      .ExpectProductionNTimes(numExpected: 6, Heading)
      .Expect(END_TR);
    
    parser.AllMatchThen( (list, writer) => {
      var annotations =
        from node in list
        where node.HasAnnotation
        select node;
      annotations.ForEach((m,i) => Annotations.Add(m.Annotation));
    });
    Assert.Collection( Annotations,
      a1 => Assert.Equal(expected: "tr:headings", a1),
      a2 => Assert.Equal(expected: "heading", a2), // 1
      a3 => Assert.Equal(expected: "end-th", a3),
      a4 => Assert.Equal(expected: "heading", a4), // 2
      a5 => Assert.Equal(expected: "end-th", a5),
      a6 => Assert.Equal(expected: "heading", a6), // 3
      a7 => Assert.Equal(expected: "end-th", a7),
      a8 => Assert.Equal(expected: "heading", a8), // 4
      a9 => Assert.Equal(expected: "end-th", a9),
      a10 => Assert.Equal(expected: "heading", a10), // 5
      a11 => Assert.Equal(expected: "end-th", a11),
      a12 => Assert.Equal(expected: "heading", a12), // 6
      a13 => Assert.Equal(expected: "end-th", a13)
    );
  }

  [Fact]
  public void ParseAction_MayExpect_appliesAnnotationWhenMatching()
  {
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .MayExpect(TABLE, annotation: "start-table")
      .MayExpect(THEAD, annotation: "head")
      .SkipUntil(END_TR)
      .MayExpect(END_TR, annotation: "end-row")
      .MayExpect(END_THEAD, annotation: "end-head")
      .AllMatchThen( (list, writer) =>{
        isMatched = true;

        var query =
          from node in list where node.IsMatch && node.HasAnnotation
          && node.Annotation.Contains(value: "head") select node.Annotation;
        Assert.Collection(query,
          h1 => Assert.Equal(expected: "head", h1),
          h2 => Assert.Equal(expected: "end-head", h2)
        );
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void ParseAction_MayExpect_makesNoChangeOnMismatch()
  {
    bool isMatched = false;
    ParseAction parser = Parsing.Group(_Pipe)
      .SkipUntil(TABLE)
      .Expect(TABLE, annotation:"table-start")
      .MayExpect(TR, annotation: "missed-thead-tr")
      .MayExpect(PARA, annotation: "para")
      .Expect(THEAD, annotation: "thead-found")
      .AllMatchThen( (list, writer) => {
        isMatched = true;

        var query = 
          from matching in list where matching.HasAnnotation
          select matching.Annotation;
        Assert.Collection(query,
          a1 => Assert.Equal(expected: "table-start", a1),
          a2 => Assert.Equal(expected: "thead-found", a2)
        );
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void ParseAction_IfElse_handlesOptionalParagraphs()
  {
    List<string> hrefList = new List<string>();

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(TD)
      .ExpectProductionNTimes(numExpected: 10, p => {
        DataHref(p, hrefList);
        return p;
      })
      ;
      Console.WriteLine(value: $"Number hrefs: {hrefList.Count}");
  }

  private void DataHref(ParseAction parser, List<string> hrefs)
  {
    MatchCondition haveHref = new MatchCondition(
      searchMethod: matching => matching.HasAnnotation && matching.Annotation == "data-href",
      conditionOnRecent: matching => matching.Parts.Exists( p => p.Count > 0 && p.ElementAt(1) == "href")
    );

    parser
      .SkipUntil(TD)
      .Expect(TD, annotation: "td-action-id")
        .Expect(ANCHOR_ID, annotation:"a-id").Expect(END_ANCHOR, annotation:"end-a-id")
        .Expect(ANCHOR_HREF, annotation:"a-href").Expect(END_ANCHOR, annotation:"end-a-href")
      .Expect(END_TD, annotation: "td-actionid-end")
      .AllMatchThen( (list, writer) => {
        Console.WriteLine(value: "TD - Action ID, HREF");
      })
      .MismatchesThen()
        .Expect(TD, annotation: "td-start")
          .Expect(PARA, annotation: "data-p")
            .Expect(ANCHOR_HREF, annotation:"data-href")
            .Expect(END_ANCHOR, annotation: "data-href-end")
          .Expect(END_PARA, annotation: "data-p-end")
        .Expect(END_TD, annotation: "td-end")
        .IfElse( haveHref, (parser, match) => {
            match.Parts.MatchSome(parts => hrefs.Add(parts.ElementAt(index: 2)));
            match.Parts.MatchSome(parts => Console.WriteLine(value: $"Href value: {parts}"));
            return parser;
          },
          (parser) => parser
        )
        .MismatchesThen()
          .SkipUntil(TD)
          .Expect(TD, annotation: "td-empty-start")
          .Expect(END_TD, annotation:"td-empty-end")
          .AllMatchThen( (list, writer) => {
            Console.WriteLine(value:"Empty TD");
          })
        .MismatchesThen()
          .SkipUntil(TD)
          .Expect(TD, annotation: "td-nondata-start")
          .SkipUntil(END_TD)
          .Expect(END_TD, annotation:"td-nondata-end")
          .AllMatchThen( (list, writer) => {
            Console.WriteLine(value:"other TD");
          });
  }
}