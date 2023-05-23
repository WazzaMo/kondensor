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
  private Production _ResourceTable;
  private Production _ConditionKeyTable;
  private Matcher _Table, _endTable, 
    _Thead, _endThead,
    _Tr, _endTr,
    _Th, _endTh;

  const string
    ANO_ACT_ACTIONS = "action",
    ANO_ACT_DESC = "act:description",
    ANO_ACT_ACCESS = "access",
    ANO_ACT_RESOURCE = "act:resource",
    ANO_ACT_CONDKEY = "act:conditionkey",
    ANO_ACT_DEPENDENTS = "act:dependent",
    ANO_RT_RESTYPE = "rt:resource",
    ANO_RT_ARN = "rt:arn",
    ANO_RT_CONDKEY = "rt:conditionkey",
    ANO_CK_CONDKEY = "ck:conditionkey",
    ANO_CK_DESC = "ck:description",
    ANO_CK_TYPE = "ck_type";

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
    _ResourceTable = ResourceTable;
    _ConditionKeyTable = ConditionKeyTable;
  }

  private ParseAction ActionTable(ParseAction parser)
  {
    return parser
      .Expect(_Thead)
        .Expect(_Tr)
          .Expect(_Th, ANO_ACT_ACTIONS) // action
          .Expect(_endTh)
          .Expect(_Th, ANO_ACT_DESC) // description
          .Expect(_endTh)
          .Expect(_Th, ANO_ACT_ACCESS) // access level
          .Expect(_endTh)
          .Expect(_Th, ANO_ACT_RESOURCE) // resource type
          .Expect(_endTh)
          .Expect(_Th, ANO_ACT_CONDKEY) // condition keys
          .Expect(_endTh)
          .Expect(_Th, ANO_ACT_DEPENDENTS) // dependent actions
          .Expect(_endTh)
        .Expect(_endTr)
      .Expect(_endThead);
  }

  private ParseAction ResourceTable(ParseAction parser)
  {
    return parser
      .Expect(_Thead)
        .Expect(_Tr)
          .Expect(_Th, ANO_RT_RESTYPE).Expect(_endTh)
          .Expect(_Th, ANO_RT_ARN).Expect(_endTh)
          .Expect(_Th, ANO_RT_CONDKEY).Expect(_endTh)
        .Expect(_endTr)
      .Expect(_endThead);
  }

  private ParseAction ConditionKeyTable(ParseAction parser)
  {
    return parser
      .Expect(_Thead)
        .Expect(_Tr)
          .Expect(_Th, ANO_CK_CONDKEY).Expect(_endTh)
          .Expect(_Th, ANO_CK_DESC).Expect(_endTh)
          .Expect(_Th, ANO_CK_TYPE).Expect(_endTh)
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

  [Fact]
  public void ActionProduction_findMatchByAnnotation()
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
          where (element.Annotation == ANO_ACT_ACTIONS || element.Annotation == ANO_ACT_RESOURCE)
            && element.Parts.Exists(l => l.Count == 1)
          select element;
        for(int index = 0; index < found.Count(); index++)
        {
          found.ElementAt(index: index).Parts.MatchSome( listTxt => headings.Add( listTxt.Aggregate((a,b) => a+b) ));
        }
      });
    Assert.True(isMatched);
    Assert.Equal(expected: 2, headings.Count);
  }

  [Fact]
  public void ResourceProduction_matchHeadingsWithAnnotation()
  {
    bool isMatched = false;
    List<string> headings = new List<string>();

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(_Table) // actions table
      .SkipUntil(_endTable) // end actions table
      .SkipUntil(_Table) // resource table
      ;

    Assert.False(_Pipe.IsInFlowEnded);

    parser
      .Expect(_Table)
        .Expect(_ResourceTable)
        .SkipUntil(_endTable)
      .Expect(_endTable)
      .Then( (list, writer) => {
        isMatched = true;
        var found =
          from element in list
          where element.HasAnnotation && element.Parts.Exists(p => p.Count == 1)
          select element;
        for(int index = 0; index < found.Count(); index++)
        {
          found.ElementAt(index).Parts.MatchSome( p => headings.Add(p.Aggregate( (a,b) => a + b)) );
        }
      });
    Assert.True(isMatched);
    Assert.Equal(expected: 3, headings.Count);
    Assert.Collection(headings,
      h1 => Assert.Equal(expected: "Resource types", h1),
      h2 => Assert.Equal(expected: "ARN", h2),
      h3 => Assert.Equal(expected:"Condition keys",h3)
    );
  }
}
