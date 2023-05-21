/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using Optional;

 namespace Parser
 {

  public static class Exprn
  {
    /// <summary>
    /// Return result of first matching Matcher.
    /// </summary>
    /// <param name="token">token string to attempt matching</param>
    /// <param name="matchers">Matcher functions to attempt</param>
    /// <returns>Matching result</returns>
    public static Matching Or(string token, params Matcher[] matchers)
    {
      Matcher matcher;
      string name;
      bool isMatch =false;
      Matching result = Utils.NoMatch();
      
      for(int index = 0; !isMatch && index < matchers.Length; index++)
      {
        matcher = matchers[index];
        name = Utils.GetMatcherName(matcher);
        result = matcher.Invoke(token);
        isMatch = result.IsMatch;
        result.MatcherName = name;
      }
      return result;
    }

  }

 }