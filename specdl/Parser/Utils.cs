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
        IsMatch = false,
        Parts = Option.None<LinkedList<string>>()
      };
    
    public static Option<LinkedList<string>> GetParts(Match? match)
    {
      Option<LinkedList<string>> result;
      GroupCollection group;
      if (match != null && match.Length > 0)
      {
        group = match.Groups;
        LinkedList<string> list = new LinkedList<string>();
        
        for(int index = 0; index < group.Count; index++)
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

    public static Matcher MatchRule(Regex rule, string name)
    {
      Matcher matcher = (string token) => {
        Matching result = Utils.NoMatch();

        var match = rule.Match(token);
        if (match != null && match.Length > 0)
        {
          result = new Matching() {
            MatcherName = Option.Some(name),
            IsMatch = true,
            Parts = Utils.GetParts(match)
          };
        }
        return result;
      };
      return matcher;
    }
    
    internal static string GetMatcherName(Matcher matcher)
      => matcher.Method.GetGenericMethodDefinition().Name;
  }

}