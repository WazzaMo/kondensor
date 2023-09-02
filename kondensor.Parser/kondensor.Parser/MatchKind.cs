/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

namespace kondensor.Parser;

/// <summary>
/// Indicates if any matching tried and to what extent a match was made.
/// </summary>
 public enum MatchKind
 {
  NoMatchAttempted,

  Mismatch,

  /// <summary>Matcher has only one regex and it matched the token.</summary>
  SingularMatch,

  ///<summary>REGEX has named match groups.</summary>
  NamedGroupMatch,

  /// <summary>Matcher had a short and long regex and short matched.</summary>
  ShortMatch,

  /// <summary>Matcher had short and long regex and long matched.</summary>
  LongMatch
 }