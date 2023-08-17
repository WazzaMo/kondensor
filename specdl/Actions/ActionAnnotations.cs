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
public static class ActionAnnotations
{
  internal const string
    START_ACTION_TABLE_ANNOTATION = "start:table:actions",
    END_ACTION_TABLE_ANNOTATION = "end:table:actions",
    START_ACTION_THEAD_ANNOTATION = "start:thead:actions",
    END_ACTION_THEAD_ANNOTATION = "end:thead:actions",
    START_HEADER_TR_ANNOTATION = "start:tr:header",
    END_HEADER_TR_ANNOTATION = "end:tr:header",

    START_HEADING_ANNOTATION = "start:th:heading",
    END_HEADING_ANNOTATION = "end:th:heading",
    START_ROW_ANNOTATION = "start:tr:row",
    END_ROW_ANNOTATION = "end:tr:row",
    START_ACTION_ROW_ANNOTATION = "start:tr:action:row",
    START_ACTION_PROP_ROW_ANNOTATION = "start:tr:action-prop:row",
    END_ACTION_PROP_ROW_ANNOTATION = "end:tr:action-prop:row",

    START_CELL_ACTION_ANNOTATION = "start:td:action:decl",
    START_ID_ACTION_ANNOTATION = "start:a-id:action",
    END_ID_ACTION_ANNOTATION = "end:a-id:action",
    START_HREF_ACTION_ANNOTATION = "start:a-href:action",
    END_HREF_ACTION_ANNOTATION = "end:a-href:action",
    END_CELL_ACTION_ANNOTATION = "end:td:action",
    START_CELL_ACTIONDESC_ANNOTATION = "start:td:description",
    START_CELL_ACTIONDESC_ROWSPAN_ANNOTATION = "start:td:rowspan:description",
    START_CELL_ACTION_ROWSPAN_ANNOTATION = "start:td:rowspan",
    START_CELL_ACTION_NEWDESC_ANNOTATION = "start:td:new-description",
    END_CELL_ACTIONDESC_ANNOTATION = "end:td:description",
    START_ACCESSLEVEL_ANNOTATION = "start:td:accesslevel",
    START_ACCESSLEVEL_ROWSPAN_ANNOTATION = "start:td:rowspan:accesslevel",
    START_ACCESSLEVEL_EMPTY_ANNOTATION = "start:td:accesslevel:empty",
    END_ACCESSLEVEL_ANNOTATION = "end:td:accesslevel",
    
    START_TD_RESOURCETYPE = "start:td:resourcetype",
    END_TD_RESOURCETYPE = "end:td:resourcetype",
    START_TD_CONDKEY = "start:td:condkey",
    END_TD_CONDKEY = "end:td:condkey",
    START_TD_DEPACT = "start:td:depact",
    END_TD_DEPACT = "end:td:depact",

    A_HREF_RESOURCE = "a:href:resourece",
    A_HREF_CONDKEY = "a:href:condkey",
    END_A = "end:a",
    START_PARA = "start:p",
    END_PARA = "end:p",
    START_PARA_DEPENDENT = "p:dependentaction",
    START_NEWDECL_PARA = "start:p:value:declaration",
    START_AWSICON = "start:awsicon:optional",
    END_AWSICON = "start:awsicon:optional"
    ;

}
