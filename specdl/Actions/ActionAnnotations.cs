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
    ACTION_HEADING = "heading:action",
    OTHER_HEADING = "heading:other",
    END_ACTION_TABLE = "table:end:actions",
    START_ROW_ACTION = "row:start:newaction",
    END_ROW_ACTION = "row:end:newAction",
    /// <summary>Extended action props with desc and access level</summary>
    START_ROW_EXTENDED_ACTION = "row:start:extendedAction",
    END_ROW_EXTENDED_ACTION = "row:end:extendedAction",
    START_ROW_ADD_PROPS_ACTION = "row:start:additionalPropsAction",
    END_ROW_ADD_PROPS_ACTION = "row:end:additionalPropsAction",
    ID_ACTION = "id:action",
    HREF_ACTION = "href:action",
    NAME_ACTION = "tag:name:action",
    ACTION_DESCRIPTION_FIRST_ENTRY = "tag:value:description",
    ACTION_ACCESS_LEVEL = "tag:value:accessLevel",
    NEW_ACTION_DESCRIPTION_SAME_ENTRY = "tag:value:sameAction:newDescription",
    RESOURCE_START = "td:start:resource",
    RESOURCE_END = "td:end:resource",
    RESOURCE_HREF = "href:resource:value",
    RESOURCE_NAME = "tag:value:resourceName",
    CONDKEY_START = "td:start:conditionKey",
    CONDKEY_END = "td:end:conditionKey",
    CONDKEY_HREF = "href:conditionKey:value",
    CONDKEY_NAME = "name:conditionKey:value",
    DEPACT_START = "td:start:depAction",
    DEPACT_END = "td:end:depAction",
    DEPACT_VALUE = "tag:value:dependentAction"
    ;

}
