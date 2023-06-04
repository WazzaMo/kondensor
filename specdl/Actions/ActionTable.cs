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

  public ParseAction ActionsTable(ParseAction parser)
  {
    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "start:table:actions")
        .Expect(production: ActionsHeader)
      .Expect(HtmlRules.END_TABLE, annotation: "end:table:actions")
      ;
    return parser;
  }

  private ParseAction ActionsHeader(ParseAction parser)
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

  private ParseAction TableData(ParseAction parser)
  {
    return parser;
  }

  private ParseAction RowData(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TR, annotation: "start:tr:row")
        .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "start:td:action")
          .Expect(HtmlRules.START_A_ID, annotation: "start:a-id:action")
          .Expect(HtmlRules.END_A, annotation:"end:a-id:action")
          .Expect(HtmlRules.START_A_HREF, annotation:"start:a-href:action")
          .Expect(HtmlRules.END_A, annotation: "end:a-href:action")
        .Expect(HtmlRules.END_TD, annotation: "end:td:action")
        .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "start:td:description")
        .Expect(HtmlRules.END_TD, annotation: "end:td:description")
      .Expect(HtmlRules.END_TR, annotation: "end:tr:row")
      ;
    return parser;
  }

  private ParseAction RepeatRowData(ParseAction parser)
  {
    return parser;
  }
}
