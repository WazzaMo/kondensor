/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;
using Optional;

using Parser;
using HtmlParse;

namespace Actions;

/// <summary>
/// Placeholder for action table processing logic.
/// </summary>
public struct ActionTable
{
  const string HEADING_ANNOTATION = "start:th:heading";

  private List<string> _HeadingNames;

  public ActionTable()
  {
    _HeadingNames = new List<string>();
  }

  public ParseAction ActionsHeader(ParseAction parser)
  {
    var headingList = _HeadingNames;

    parser
      .Expect(HtmlRules.START_THEAD, annotation: "start:thead:actions")
        .Expect(HtmlRules.START_TR, annotation: "start:tr:header")
          .ExpectProductionUntil(Heading,
        endRule: HtmlRules.END_TR, endAnnodation: "end:tr:header")
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead:actions")
      .AllMatchThen( (list, writer) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == HEADING_ANNOTATION
          && node.Parts.HasValue
          select node;
        query.ForEach( (node, idx) => {
          node.Parts.MatchSome( parts => headingList.Add(parts.ElementAt(HtmlRules.TH_VALUE_INDEX)));
        });
      });
    return parser;
  }

  private ParseAction Heading(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TH_VALUE, annotation: HEADING_ANNOTATION)
      .Expect(HtmlRules.END_TH, annotation: "end:th");

}
