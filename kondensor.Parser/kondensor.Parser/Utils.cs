/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Optional;

namespace kondensor.Parser;

public static class Utils
{
  public static Matching NoMatch()
    => new Matching() {
      MatchResult = MatchKind.NoMatchAttempted,
      MatcherName = Matching.UNDEFINED_NAME,
      Parts = Option.None<LinkedList<string>>()
    };
  
  /// <summary>
  /// Collect the group strings from the Regex rule, skipping the overall match (0);
  /// </summary>
  /// <param name="match">Match result</param>
  /// <returns>Linklist of string groups matched, 0 index is first group.</returns>
  public static Option<LinkedList<string>> GetParts(Match? match)
  {
    Option<LinkedList<string>> result;
    GroupCollection group;
    if (match != null && match.Length > 0)
    {
      group = match.Groups;
      LinkedList<string> list = new LinkedList<string>();
      
      for(int index = 1; index < group.Count; index++)
      {
        list.AddLast( group[index].Value );
      }
      result = Option.Some(list);
    }
    else
    {
      result = Option.None<LinkedList<string>>();
    }
    return result;
  }

  /// <summary>
  /// Create a matcher from a single regex rule.
  /// </summary>
  /// <param name="rule">Regex rule based on simple pattern.</param>
  /// <param name="name">Name of matcher to create.</param>
  /// <param name="annotation">Optional annotation string for results searching</param>
  /// <returns>Viable matcher to use in parsing.</returns>
  public static Matcher SingularMatchRule(Regex rule, string name, string? annotation = null)
  {
    Matcher matcher = (string token) => {
      Matching result = Utils.NoMatch();

      var match = rule.Match(token);
      if (match != null && match.Length > 0)
      {
        result = new Matching() {
          MatcherName = name,
          MatchResult = MatchKind.SingularMatch,
          Parts = Utils.GetParts(match)
        };
      }
      else
      {
        result = new Matching() {
          MatchResult = MatchKind.Mismatch,
          MatcherName = name
        };
      }
      if (annotation != null)
      {
        result.Annotation = annotation;
      }
      return result;
    };
    return matcher;
  }

  /// <summary>Creates matcher for REGEX with named groups.</summary>
  /// <param name="rule">Regex object to use (with named groups)</param>
  /// <param name="name">Name for matching rule</param>
  /// <param name="annotation">Default annotation to apply (opt)</param>
  /// <returns>Matcher function</returns>
  public static Matcher NamedGroupRule(
    Regex rule,
    string name,
    string? annotation = null
  )
  {
    Matcher matcher = (string token) => {
      Matching result = Utils.NoMatch();

      var match = rule.Match(token);
      GroupCollection groups = match.Groups;

      if (groups.Keys.Count() > 0)
      {
        result = new Matching()
        {
          MatcherName = name,
          MatchResult = MatchKind.NamedGroupMatch,
        };
        if (annotation != null)
          result.Annotation = annotation;
        
        var keys = match.Groups.Keys.ForEach(
          (key, idx) => result.AddNamedPart(key, groups[key].Value)
        );
      }
      else
        throw new ArgumentException(
          message: $"Not a {nameof(NamedGroupRule)}: {rule}"
        );
      return result;
    };
    return matcher;
  }

  /// <summary>
  /// Create a short/long matcher that can handle a tag or tag with attribs.
  /// </summary>
  /// <param name="shortRule">Basic regex rule for tag</param>
  /// <param name="longRule">Complex rule that gets attributes and other parts</param>
  /// <param name="name">Name of mather to create.</param>
  /// <param name="annotation">Optional annotation string for results searching</param>
  /// <returns>Matcher that can handle simple and complex tags with parts</returns>
  public static Matcher ShortLongMatchRules(Regex shortRule, Regex longRule, string name, string? annotation = null)
  {
    Matcher matcher = (string token) => {
      Matching result = Utils.NoMatch();
      Match match;

      match = longRule.Match(token);
      if (match.Length > 0)
      {
        result = new Matching() {
          MatcherName = name,
          MatchResult = MatchKind.LongMatch,
          Parts = GetParts(match)
        };
      }
      else {
        match = shortRule.Match(token);
        if (match.Length > 0)
        {
          result = new Matching() {
            MatcherName = name,
            MatchResult = MatchKind.ShortMatch,
            Parts = GetParts(match)
          };
        }
        else
        {
          result = new Matching() {
            MatcherName = name,
            MatchResult = MatchKind.Mismatch,
            MismatchToken = token
          };
        }
      }
      if (annotation != null)
      {
        result.Annotation = annotation;
      }
      return result;
    };
    return matcher;
  }

  
  internal static string GetMatcherName(Matcher matcher)
    => matcher.Method.GetGenericMethodDefinition().Name;
}

