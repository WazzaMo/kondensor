/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

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
  Write
}