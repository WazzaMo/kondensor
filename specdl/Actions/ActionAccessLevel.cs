/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

namespace Actions;


/// <summary>
/// A fixed, known set of access levels that can be considered
/// for each IAM action.
/// </summary>
public enum ActionAccessLevel
{
  Unknown,
  NotSpecified,
  Read,
  List,
  ArrayOfString,
  Tagging,
  Write,
  PermissionsManagement
}

  public static class ExtActionAccessLevel
  {
    public static ActionAccessLevel GetLevelFrom(this string value)
    {
      return value switch {
        "Permissions management" => ActionAccessLevel.PermissionsManagement,
        _ => Enum.TryParse<ActionAccessLevel>(value, out ActionAccessLevel _level)
          ? _level
          : ActionAccessLevel.Unknown
      };
    }
  }