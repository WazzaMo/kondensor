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
    INDENT = "  ",
    SEP = ": ",
    MEMBER = "- ",
    TABLE = "ActionsTable",
    SRC_URL = "SourceUrl",
    HEADINGS = "Headings",
    DEFINITIONS = "Definitions",
    ACTION_DEF = "Action",
    ACTION_NAME = "Name",
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
    writer.WriteFragment(INDENT).WriteFragment(MEMBER).WriteFragment(SRC_URL).WriteFragment(SEP).WriteFragmentLine(sourceUrl);
    writer.WriteFragment(INDENT).WriteFragment(MEMBER).WriteFragment(HEADINGS).WriteFragmentLine(SEP);
  
    headings.ForEach(
      heading => writer
        .WriteFragment(INDENT).WriteFragment(INDENT)
        .WriteFragment(MEMBER)
        .WriteFragmentLine(heading)
    );

    writer.WriteFragment(INDENT).WriteFragment(MEMBER).WriteFragment(DEFINITIONS).WriteFragmentLine(SEP);
    actions.ForEach( _action => {
      writer.WriteFragment(INDENT).WriteFragment(INDENT)
        .WriteFragment(ACTION_DEF).WriteFragment(SEP).WriteFragmentLine(_action.ActionId);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(MEMBER)
        .WriteFragment(ACTION_NAME).WriteFragment(SEP).WriteFragmentLine(_action.Name);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(MEMBER)
        .WriteFragment(DESCRIPTION).WriteFragment(SEP).WriteFragmentLine(_action.Description);
      writer.WriteFragment(INDENT).WriteFragment(INDENT).WriteFragment(MEMBER)
        .WriteFragment(API_URL).WriteFragment(SEP).WriteFragmentLine(_action.ApiLink);

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