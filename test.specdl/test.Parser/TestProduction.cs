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
  private Production _ActionData;

  private Matcher _Table, _endTable, 
    _Thead, _endThead,
    _Tr, _endTr,
    _Th, _endTh,
    _Td, _endTd,
    _A_id, _A_href, _endA,
    _P, _endP;

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
    _Td = Utils.ShortLongMatchRules(HtmlPatterns.TD, HtmlPatterns.TD_ATTRIB_VALUE, name: "td");
    _endTd = Utils.SingularMatchRule(HtmlPatterns.END_TD, name: "end:td");
    _A_id = Utils.SingularMatchRule(HtmlPatterns.A_ID, name: "a:id");
    _A_href = Utils.SingularMatchRule(HtmlPatterns.A_HREF, name: "a:href");
    _endA = Utils.SingularMatchRule(HtmlPatterns.END_A, name: "end:a");
    _P = Utils.SingularMatchRule(HtmlPatterns.PARA, name: "p");
    _endP = Utils.SingularMatchRule(HtmlPatterns.END_PARA, name: "end:p");
    _ActionTable = ActionTable;
    _ResourceTable = ResourceTable;
    _ConditionKeyTable = ConditionKeyTable;
    _ActionData = ActionData;
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

// <tr>
//     <td rowspan=""2"">
//         <a id=""awsaccountmanagement-GetContactInformation""></a>
//         <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_GetContactInformation.html"">GetContactInformation</a>
//     </td>
//     <td rowspan=""2"">Grants permission to retrieve the primary contact information for an account</td>
//     <td rowspan=""2"">Read</td>
//     <td>
//         <p>
//             <a href=""#awsaccountmanagement-account"">account</a>
//         </p>
//     </td>
//     <td></td>
//     <td></td>
// </tr>
  private ParseAction ActionData(ParseAction parser)
    => parser
        .Expect(_Tr)
          .Expect(_Td, annotation: "td:" + ANO_ACT_ACCESS)
            .SkipUntil(_endTd)
          .Expect(_endTd)
          .Expect(_Td, annotation: "td:" + ANO_ACT_DESC)
            .SkipUntil(_endTd)
          .Expect(_endTd)
          .Expect(_Td, annotation: "td:" + ANO_ACT_ACCESS)
            .SkipUntil(_endTd)
          .Expect(_endTd)
          .Expect(_Td, annotation: "td:" + ANO_ACT_RESOURCE)
            .SkipUntil(_endTd)
          .Expect(_endTd)
          .Expect(_Td, annotation: "td:" + ANO_ACT_CONDKEY)
            .SkipUntil(_endTd)
          .Expect(_endTd)
          .Expect(_Td, annotation: "td:" + ANO_ACT_DEPENDENTS)
            .SkipUntil(_endTd)
          .Expect(_endTd)
        .Expect(_endTr);

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
    Assert.Collection(headings,
      h1 => Assert.Equal(expected: "Actions", h1),
      h2 => Assert.Equal(expected: "Description", h2),
      h3 => Assert.Equal(expected: "Access level", h3),
      h4 => Assert.Equal(expected: "Resource types (*required)", h4),
      h5 => Assert.Equal(expected: "Condition keys", h5),
      h6 => Assert.Equal(expected: "Dependent actions", h6)
    );
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

    [Fact]
  public void ConditionKeyProduction_matchHeadingsWithAnnotation()
  {
    bool isMatched = false;
    List<string> headings = new List<string>();

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(_Table) // actions table
      .SkipUntil(_endTable) // end actions table
      .SkipUntil(_Table) // resource table
      .SkipUntil(_endTable) // end resource table
      .SkipUntil(_Table) // condition key table
      ;

    Assert.False(_Pipe.IsInFlowEnded);

    parser
      .Expect(_Table)
        .Expect(_ConditionKeyTable)
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
      h1 => Assert.Equal(expected: "Condition keys", h1),
      h2 => Assert.Equal(expected: "Description", h2),
      h3 => Assert.Equal(expected: "Type",h3)
    );
  }

  // ExpectProductionUntil
  // [Fact]
  public void ExpectProductionUntil_parsesProductionRepeatedlyStoppingOnSentinel()
  {
    bool isMatched = false;
    List<string> Annotations = new List<string>();
    
    var parser = Parsing.Group(_Pipe)
      .SkipUntil(_Table)
      .Expect(_Table, annotation: "action:table-start")
      .Expect(_ActionTable)
      .ExpectProductionUntil(_ActionData, _endTable, endAnnodation: "end:actionTable")
      .Then( (list, writer) => {
        var nodes =
          from matchNode in list
          where matchNode.HasAnnotation && matchNode.Annotation.StartsWith(value:"td:")
          select matchNode;

        isMatched = true;
        nodes.ForEach( (node, i) => Annotations.Add($"#{i}:{node.Annotation}"));
      });
    Assert.True(isMatched);
    Assert.Equal(expected: 6, Annotations.Count);
  }
}
