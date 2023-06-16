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
    SRC_URL = "SourceUrl",
    HEADINGS = "Headings",
    UNESCQUOTE = "'",
    ESCQUOTE = "\"",
    DEFINITIONS = "Definitions",
    ACTION_DEF = "Action",
    ACTION_NAME = "Name",
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
    writer.KeyLine(TABLE)
      .Indent(1).Key(SRC_URL).UrlLIne(sourceUrl)
      .Indent(count: 1).KeyLine(HEADINGS);
 
    headings.ForEach(
      heading => writer
        .Indent(count: 2).StringListLine(heading)
    );

    writer.Indent(count: 2).KeyLine(DEFINITIONS);

    actions.ForEach( _action => {
      writer
        .Indent(count: 3).Key(ACTION_DEF).QuoteLine(_action.ActionId)
        .Indent(count: 4).Key(ACTION_NAME).WriteFragmentLine(_action.Name)
        .Indent(count: 4).Key(DESCRIPTION).WriteFragmentLine(_action.Description)
        .Indent(count: 4).Key(API_URL).UrlLIne(_action.ApiLink);

      _action.GetMappedAccessLevels().ForEach( (accessLevel, idex) => {

        _action.GetResourceTypesForLevel(accessLevel).ForEach( (resource, rIdx) => {
          writer
            .Indent(count: 5).Key(RESOURCE_DEF).QuoteLine(resource.ResourceTypeDefId)
              .Indent(count: 6).Key(RESOURCE_NAME).WriteFragmentLine(resource.ResourceTypeName);

          if (resource.ConditionKeyIds().Count() > 0)
          {
            writer.Indent(count: 6).KeyLine(CONDITION_KEYS);
            resource.ConditionKeyIds().ForEach( (condkey, ckIdx) =>
              writer.Indent(count: 6).StringListLine(condkey)
            );
          }

          if (resource.DependendActionIds().Count() > 0)
          {
            writer.Indent(count: 6).KeyLine( DEPENDENT_ACTIONS );
            resource.DependendActionIds().ForEach( (depAction, idxDa)
              => writer.Indent(count: 6).StringListLine(depAction)
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