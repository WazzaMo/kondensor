/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Xunit;

using Parser;
using HtmlParse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace test.Parser;

/// <summary>
/// Production in this case is a rule in a grammar
/// </summary>
public class TestProduction
{
  private HtmlPipe _Content;
  private ReplayWrapPipe _Pipe;
  private Production _ActionTable;
  private Matcher _Table, _endTable, 
    _Thead, _endThead,
    _Tr, _endTr,
    _Th, _endTh;

  public TestProduction()
  {
    _Content = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Pipe = new ReplayWrapPipe(_Content);
    _Table = Utils.ShortLongMatchRules(HtmlPatterns.TABLE, HtmlPatterns.TABLE_ATTRIB, name: "table");
    _endTable = Utils.SingularMatchRule(HtmlPatterns.END_TABLE, name: "end:table");
    _Thead = Utils.SingularMatchRule(HtmlPatterns.THEAD, "thead");
    _endThead = Utils.SingularMatchRule(HtmlPatterns.END_THEAD, name: "end:thead");
    _Tr = Utils.ShortLongMatchRules(HtmlPatterns.TR, HtmlPatterns.TR_ATTRIB, name: "tr");
    _endTr = Utils.SingularMatchRule(HtmlPatterns.END_TR, "end:tr");
    _Th = Utils.SingularMatchRule(HtmlPatterns.TH_VALUE, name: "th");
    _endTh = Utils.SingularMatchRule(HtmlPatterns.END_TH, "end:th");
    _ActionTable = ActionTable;
  }

  private ParseAction ActionTable(ParseAction parser)
  {
    return parser
      .Expect(_Thead)
        .Expect(_Tr)
          .Expect(_Th) // action
          .Expect(_endTh)
          .Expect(_Th) // description
          .Expect(_endTh)
          .Expect(_Th) // access level
          .Expect(_endTh)
          .Expect(_Th) // resource type
          .Expect(_endTh)
          .Expect(_Th) // condition keys
          .Expect(_endTh)
          .Expect(_Th) // dependent actions
          .Expect(_endTh)
        .Expect(_endTr)
      .Expect(_endThead);
  }

  [Fact]
  public void ActionProduction_finds_ActionHeadings()
  {
    bool isMatched = false;
    IEnumerable<Matching> found = new Matching[0];
    List<string> headings = new List<string>();

    Parsing.Group(_Pipe)
      .SkipUntil(_Table)
      .Expect(_Table)
      .Expect(_ActionTable)
      .SkipUntil(_endTable)
      .Expect(_endTable)
      .Then( (list, writer) =>{
        isMatched = true;
        found =
          from element in list
          where element.MatcherName == "th"
            && element.Parts.Exists(l => l.Count == 1)
          select element;
        for(int index = 0; index < found.Count(); index++)
        {
          found.ElementAt(index: index).Parts.MatchSome( listTxt => headings.Add( listTxt.Aggregate((a,b) => a+b) ));
        }
      });
    Assert.True(isMatched);
    Assert.Equal(6, headings.Count);
    Assert.Equal("Actions", headings[0]);
    Assert.Equal(expected: "Description", headings[1]);
    Assert.Equal(expected: "Access level", headings[2]);
    Assert.Equal(expected: "Resource types (*required)", headings[3]);
    Assert.Equal(expected: "Condition keys", headings[4]);
    Assert.Equal(expected: "Dependent actions", headings[5]);
  }
}
