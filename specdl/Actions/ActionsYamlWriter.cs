/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;

namespace Actions;

/// <summary>
/// Responsible only for the YAML output and formatting
/// of the actions table that was parsed.
/// </summary>
public static class ActionsYamlWriter
{
  public const string
    EMPTY = "",
    INDENT = "  ",
    SEP = ": ",
    CHILD = "  ",
    SEQUENCE = "- ",
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
    writer.WriteFragment(TABLE).WriteFragmentLine(SEP);
    writer.WriteFragment(INDENT).WriteFragment(CHILD).WriteFragment(SRC_URL).WriteFragment(SEP).Url(sourceUrl).WriteFragmentLine(EMPTY);
    writer.WriteFragment(INDENT).WriteFragment(CHILD).WriteFragment(HEADINGS).WriteFragmentLine(SEP);
  
    headings.ForEach(
      heading => writer
        .WriteFragment(INDENT).WriteFragment(INDENT)
        .WriteFragment(SEQUENCE)
        .WriteFragment(UNESCQUOTE).WriteFragment(heading).WriteFragmentLine(UNESCQUOTE)
    );

    writer.WriteFragment(INDENT).WriteFragment(CHILD).WriteFragment(DEFINITIONS).WriteFragmentLine(SEP);
    actions.ForEach( _action => {
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(CHILD)
        .WriteFragment(ACTION_DEF).WriteFragment(SEP).WriteFragmentLine(_action.ActionId);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(CHILD)
        .WriteFragment(ACTION_NAME).WriteFragment(SEP).WriteFragmentLine(_action.Name);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(CHILD)
        .WriteFragment(DESCRIPTION).WriteFragment(SEP).WriteFragmentLine(_action.Description);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(CHILD)
        .WriteFragment(API_URL).WriteFragment(SEP).Url(_action.ApiLink).WriteFragmentLine(EMPTY);
      _action.GetMappedAccessLevels().ForEach( (accessLevel, idex) => {

        _action.GetResourceTypesForLevel(accessLevel).ForEach( (resource, rIdx) => {
          writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(CHILD)
            .WriteFragment(RESOURCE_DEF + SEP).WriteFragmentLine(resource.ResourceTypeDefId);
          writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT)
            .WriteFragment(CHILD).WriteFragment(RESOURCE_NAME + SEP).WriteFragmentLine(resource.ResourceTypeName);

          writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT)
            .WriteFragment(CHILD).WriteFragmentLine(CONDITION_KEYS + SEP);
          
          resource.ConditionKeyIds().ForEach( (condkey, ckIdx) =>
            writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(INDENT)
              .WriteFragment(INDENT).StringList(condkey).WriteFragmentLine(EMPTY)
          );
        });
        
        //
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

  private static IPipeWriter Url(this IPipeWriter writer, string url)
    =>  writer.WriteFragment(UNESCQUOTE).WriteFragment(url).WriteFragment(UNESCQUOTE);
  
  private static IPipeWriter StringList(this IPipeWriter writer, string member)
    => writer.WriteFragment(SEQUENCE).WriteFragmentLine(fragment: $"{UNESCQUOTE}{member}{UNESCQUOTE}");
}