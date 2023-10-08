/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System.Collections.Generic;
using Optional;

namespace kondensor.Parser;

/// <summary>
/// Represents a matcher outcome, a Matching of the token to the expected pattern.
/// </summary>
public struct Matching 
{
  public const string
    UNDEFINED_NAME = "UNDEFINED_MATCHER_NAME",
    UNDEFINED_ANNOTATION = "UNDEFINED_ANNOTATION",
    UNDEFINED_MISMATCH = "NO_MISMATCH";
  
  public const int IDX_NO_MATCH = -1;

  public bool IsMatch
    => MatchResult != MatchKind.NoMatchAttempted && MatchResult != MatchKind.Mismatch;
  
  public bool HasName
    => MatcherName != Matching.UNDEFINED_NAME;
  
  public bool HasAnnotation
    => Annotation != UNDEFINED_ANNOTATION;

  public MatchKind MatchResult;
  public Option<LinkedList<string>> Parts;
  public string MatcherName;
  public string Annotation;
  public string MismatchToken;
  public int MatchIndex;

  /// <summary>Used for REGEX patterns with named groups.</summary>
  private Dictionary<string,string> _NamedGroups;

  public Matching()
  {
    MatchResult = MatchKind.NoMatchAttempted;
    Parts = Option.None<LinkedList<string>>();
    MatcherName = UNDEFINED_NAME;
    Annotation = UNDEFINED_ANNOTATION;
    MismatchToken = UNDEFINED_MISMATCH;
    _NamedGroups = new Dictionary<string, string>();
    MatchIndex = IDX_NO_MATCH;
  }

  /// <summary>Property indicates if REGEX had named groups</summary>
  public bool HasNamedGroups => _NamedGroups.Count > 0;

  public bool TryGetNamedPart(string key, out string value)
  {
    bool result = false;
    if (HasNamedGroups && _NamedGroups.ContainsKey(key))
    {
      value = _NamedGroups[key];
      result = true;
    }
    else
      value = "";
    return result;
  }

  /// <summary>Used to add a named group value.</summary>
  /// <param name="key">REGEX group (key) name</param>
  /// <param name="part">Text matched by REGEX to store.</param>
  public void AddNamedPart(string key, string part)
  {
    if ( ! String.IsNullOrEmpty(part))
    {
      _NamedGroups.Add(key, part);
    }
  }
}
