/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using Actions;

namespace YamlWriters;



/// <summary>
/// Responsible only for the YAML output and formatting
/// of the actions table that was parsed.
/// </summary>
public static class ActionsYamlWriter
{
  public const string
    TABLE = "ActionsTable",
    ID = "Id",
    SRC_URL = "SourceUrl",
    HEADINGS = "Headings",
    UNESCQUOTE = "'",
    ESCQUOTE = "\"",
    ACTION_LIST = "Actions",
    ACTION_DEF = "Action",
    ACTION_NAME = "Name",
    RESOURCE_LIST = "Resources",
    RESOURCE_DEF = "Resource",
    RESOURCE_NAME = "Name",
    CONDITION_KEYS = "ConditionKeys",
    DEPENDENT_ACTIONS = "DependentActionIds",
    DESCRIPTION = "Description",
    API_URL = "ApiUrl";

  /// <summary>
  /// Writes YAML output for Actions table.
  /// </summary>
  /// <param name="sourceUrl">source page for table</param>
  /// <param name="headings">Headings declared in actions table</param>
  /// <param name="actions">Action definitions</param>
  /// <param name="writer">Writer pipe</param>
  public static void WriteYaml(
    string sourceUrl,
    List<string> headings,
    List<ActionType> actions,
    IPipeWriter writer
  )
  {
    const int
      MBR_TABLE = 1,
      MBR_HEADINGS_LIST = MBR_TABLE + 1,
      MBR_ACTION_LIST = MBR_TABLE + 1,
      MBR_ACTION_DEF = MBR_ACTION_LIST + 2,
      MBR_RESOURCE_LIST = MBR_ACTION_DEF + 0,
      MBR_RESOURCE_REC = MBR_RESOURCE_LIST + 2;

    writer.KeyLine(TABLE)
      .Indent(MBR_TABLE).Key(SRC_URL).UrlLIne(sourceUrl)
      .Indent(MBR_TABLE).KeyLine(HEADINGS);
 
    headings.ForEach(
      heading => writer
        .Indent(MBR_HEADINGS_LIST).StringListLine(heading)
    );

    writer.Indent(MBR_TABLE).KeyLine(ACTION_LIST);

    actions.ForEach( _action => {
      writer
        .ListItem(MBR_ACTION_LIST, act => act.KeyLine(ACTION_DEF))
          .Indent(MBR_ACTION_DEF).Key(ID).QuoteLine(_action.ActionId)
          .Indent(MBR_ACTION_DEF).Key(ACTION_NAME).WriteFragmentLine(_action.Name)
          .Indent(MBR_ACTION_DEF).Key(DESCRIPTION).WriteFragmentLine(_action.Description)
          .Indent(MBR_ACTION_DEF).Key(API_URL).UrlLIne(_action.ApiLink)
          .Indent(MBR_ACTION_DEF).KeyLine(RESOURCE_LIST);

      _action.GetMappedAccessLevels().ForEach( (accessLevel, idex) => {

        _action.GetResourceTypesForLevel(accessLevel).ForEach( (resource, rIdx) => {
          writer.ListItem( MBR_RESOURCE_LIST, list => list.KeyLine(RESOURCE_DEF) );
          writer
            .Indent(MBR_RESOURCE_REC).Key(ID).QuoteLine(resource.ResourceTypeDefId)
            .Indent(MBR_RESOURCE_REC).Key(RESOURCE_NAME).WriteFragmentLine(resource.ResourceTypeName);

          if (resource.ConditionKeyIds().Count() > 0)
          {
            writer.Indent(MBR_RESOURCE_REC).KeyLine(CONDITION_KEYS);
            resource.ConditionKeyIds().ForEach( (condkey, ckIdx) =>
              writer.Indent(MBR_RESOURCE_REC).StringListLine(condkey)
            );
          }

          if (resource.DependendActionIds().Count() > 0)
          {
            writer.Indent(MBR_RESOURCE_REC).KeyLine( DEPENDENT_ACTIONS );
            resource.DependendActionIds().ForEach( (depAction, idxDa)
              => writer.Indent(MBR_RESOURCE_REC).StringListLine(depAction)
            );
          }
        });
      });

      // _action.GetMappedAccessLevels().ForEach( (accessLevel, idx) => {
      //   writer.WriteFragmentLine($"  {accessLevel}:");
      //   _action.GetResourceTypesForLevel(accessLevel).ForEach(
      //     (rsrcType, rsrcIdx)
      //       => writer.WriteFragmentLine($"    #{rsrcIdx}: {rsrcType.ResourceTypeName} - {rsrcType.ResourceTypeDefId}")
      //     );
      // } );
    });
  }

}