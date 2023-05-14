/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
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
    
    internal static string GetMatcherName(Matcher matcher)
      => matcher.Method.GetGenericMethodDefinition().Name;
  }

}