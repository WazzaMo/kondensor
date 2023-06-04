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
  const string
    HEADING_ANNOTATION = "start:th:heading",
    START_ROW_ANNOTATION = "start:tr:row",
    START_CELL_ACTION_ANNOTATION = "start:td:action",
    START_ID_ACTION_ANNOTATION = "start:a-id:action",
    END_ID_ACTION_ANNOTATION = "end:a-id:action",
    START_HREF_ACTION_ANNOTATION = "start:a-href:action",
    END_HREF_ACTION_ANNOTATION = "end:a-href:action",
    END_CELL_ACTION_ANNOTATION = "end:td:action",
    START_CELL_ACTIONDESC_ANNOTATION = "start:td:description",
    END_CELL_ACTIONDESC_ANNOTATION = "end:td:description",
    START_ACCESSLEVEL_ANNOTATION = "start:td:accesslevel",
    END_ACCESSLEVEL_ANNOTATION = "end:td:accesslevel",
    START_REPEAT_RESOURCE_CELL = "start:td:resource",
    END_REPEAT_RESOURCE_CELL = "end:td:resource",
    START_REPEAT_CONDKEY_CELL = "start:td:condkey",
    END_REPEAT_CONDKEY_CELL = "end:td:condkey",
    START_REPEAT_DEPEND_CELL = "start:td:dependentaction",
    END_REPEAT_DEPEND_CELL = "end:td:dependentaction",
    A_HREF_RESOURCE = "a:href:resourece",
    A_HREF_CONDKEY = "a:href:condkey",
    P_DEENDENT = "p:dependentaction",
    END_ROW_ANNOTATION = "end:tr:row";

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
          headingList.Add( HtmlPartsUtils.GetThTagValue(node.Parts));
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
      .Expect(HtmlRules.START_TR, annotation: START_ROW_ANNOTATION)
        .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: START_CELL_ACTION_ANNOTATION)
          .Expect(HtmlRules.START_A_ID, annotation: START_ID_ACTION_ANNOTATION)
          .Expect(HtmlRules.END_A, annotation: END_ID_ACTION_ANNOTATION)
          .Expect(HtmlRules.START_A_HREF, annotation: START_HREF_ACTION_ANNOTATION)
          .Expect(HtmlRules.END_A, annotation: END_HREF_ACTION_ANNOTATION)
        .Expect(HtmlRules.END_TD, annotation: END_CELL_ACTION_ANNOTATION)
        .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: START_CELL_ACTIONDESC_ANNOTATION)
        .Expect(HtmlRules.END_TD, annotation: END_CELL_ACTIONDESC_ANNOTATION)
        .Expect(HtmlRules.START_TD_VALUE, annotation: START_ACCESSLEVEL_ANNOTATION)
        .Expect(HtmlRules.END_TD, annotation: END_ACCESSLEVEL_ANNOTATION)
      .Expect(HtmlRules.END_TR, annotation: END_ROW_ANNOTATION)
      ;
    return parser;
  }

  private ParseAction RepeatRowData(ParseAction parser)
  {
    parser
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE);
    return parser;
  }
}
