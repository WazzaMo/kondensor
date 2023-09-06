/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


using System.Collections.Generic;
using Optional;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

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

    START_TH_ACTIONS = "start:th:actions",

    START_HEADING_ANNOTATION = "start:th:heading",
    END_HEADING_ANNOTATION = "end:th:heading",
    START_ROW_ANNOTATION = "start:tr:row",
    END_ROW_ANNOTATION = "end:tr:row",
    START_ACTION_ROW_ANNOTATION = "start:tr:action:row",
    START_TR_ACTION_PROP_ROW = "start:tr:action-prop:row",
    END_TR_ACTION_PROP_ROW = "end:tr:action-prop:row",

    START_TD_ID_ACTION = "start:td:action:decl",
    START_ID_ACTION_ANNOTATION = "start:a-id:action",
    END_ID_ACTION_ANNOTATION = "end:a-id:action",
    START_HREF_ACTION_ANNOTATION = "start:a-href:action",
    END_HREF_ACTION_ANNOTATION = "end:a-href:action",
    END_CELL_ACTION_ANNOTATION = "end:td:action",
    START_TD_ACTIONDESC = "start:td:description",
    START_CELL_ACTIONDESC_ROWSPAN_ANNOTATION = "start:td:rowspan:description",
    START_CELL_ACTION_ROWSPAN_ANNOTATION = "start:td:rowspan",
    START_TD_ACTION_NEWDESC = "start:td:new-description",
    END_TD_ACTIONDESC = "end:td:description",
    START_TD_ACCESSLEVEL = "start:td:accesslevel",
    START_ACCESSLEVEL_ROWSPAN_ANNOTATION = "start:td:rowspan:accesslevel",
    START_ACCESSLEVEL_EMPTY_ANNOTATION = "start:td:accesslevel:empty",
    END_TD_ACCESSLEVEL = "end:td:accesslevel",
    
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
