/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;
using Optional;

namespace Parser
{

  /// <summary>
  /// Represents a matcher outcome, a Matching of the token to the expected pattern.
  /// </summary>
  public record struct Matching (bool IsMatch, Option<LinkedList<string>> Parts, Option<string> MatcherName);


  public delegate Matching Matcher(string token );

}
