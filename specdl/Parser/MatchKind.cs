/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

 namespace Parser;

/// <summary>
/// Indicates if any matching tried and to what extent a match was made.
/// </summary>
 public enum MatchKind
 {
  NoMatchAttempted,

  Mismatch,

  /// <summary>Matcher has only one regex and it matched the token.</summary>
  SingularMatch,

  /// <summary>Matcher had a short and long regex and short matched.</summary>
  ShortMatch,

  /// <summary>Matcher had short and long regex and long matched.</summary>
  LongMatch
 }