/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using Optional;


using kondensor.Parser;


namespace Spec;

/// <summary>
/// Validation routines for any part of a specification.
/// </summary>
public static class ValueValidators
{
  const int
    REASONABLE_DESCRIPTION_LENGTH = 160; // 144 is longest seen.

  const char
    BAD_DESC_NEWLINE = '\n',
    BAD_DESC_EXCLAMATION = '!';

  const string
    SCENARIO_MARKER = "!SCENARIO";

  public static bool IsValidActionDescription(string description)
    => description.Length < REASONABLE_DESCRIPTION_LENGTH
    && description.IndexOf(BAD_DESC_EXCLAMATION) == -1
    && description.IndexOf(BAD_DESC_NEWLINE) == -1;
  
  public static bool IsScenarioActionDescription(string description)
  {
    bool result = description.IndexOf(SCENARIO_MARKER) > -1;
    return result;
  }
}