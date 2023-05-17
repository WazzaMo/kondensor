/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Optional;

namespace Parser
{

  public static class Utils
  {
    public static Matching NoMatch()
      => new Matching() {
        MatchResult = MatchKind.NoMatchAttempted,
        MatcherName = Option.None<string>(),
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
    /// <returns>Viable matcher to use in parsing.</returns>
    public static Matcher SingularMatchRule(Regex rule, string name)
    {
      Matcher matcher = (string token) => {
        Matching result = Utils.NoMatch();

        var match = rule.Match(token);
        if (match != null && match.Length > 0)
        {
          result = new Matching() {
            MatcherName = Option.Some(name),
            MatchResult = MatchKind.SingularMatch,
            Parts = Utils.GetParts(match)
          };
        }
        else
        {
          result = new Matching() {
            MatchResult = MatchKind.Mismatch,
            MatcherName = name.Some()
          };
        }
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
    /// <returns>Matcher that can handle simple and complex tags with parts</returns>
    public static Matcher ShortLongMatchRules(Regex shortRule, Regex longRule, string name)
    {
      Matcher matcher = (string token) => {
        Matching result = Utils.NoMatch();
        Match match;

        match = shortRule.Match(token);
        if (match.Length > 0)
        {
          result = new Matching() {
            MatcherName = Option.Some(name),
            MatchResult = MatchKind.ShortMatch
          };
        }
        else {
          match = longRule.Match(token);
          if (match.Length > 0)
          {
            result = new Matching() {
              MatcherName = Option.Some(name),
              MatchResult = MatchKind.LongMatch,
              Parts = GetParts(match)
            };
          }
        }
        return result;
      };
      return matcher;
    }

    
    internal static string GetMatcherName(Matcher matcher)
      => matcher.Method.GetGenericMethodDefinition().Name;
  }

}