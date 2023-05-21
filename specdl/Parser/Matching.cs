/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;
using Optional;

namespace Parser;

/// <summary>
/// Represents a matcher outcome, a Matching of the token to the expected pattern.
/// </summary>
public struct Matching 
{
  public const string UNDEFINED_NAME = "UNDEFINED_MATCHER_NAME";

  public bool IsMatch
    => MatchResult != MatchKind.NoMatchAttempted && MatchResult != MatchKind.Mismatch;
  
  public bool HasName
    => MatcherName != Matching.UNDEFINED_NAME;

  public MatchKind MatchResult;
  public Option<LinkedList<string>> Parts;
  public string MatcherName;

  public Matching()
  {
    MatchResult = MatchKind.NoMatchAttempted;
    Parts = Option.None<LinkedList<string>>();
    MatcherName = UNDEFINED_NAME;
  }
}
