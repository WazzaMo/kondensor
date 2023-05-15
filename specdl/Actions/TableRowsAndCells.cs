/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Optional;

using Parser;


namespace Actions
{

  /// <summary>
  /// Provide matchers for HTML table elements as used in the Actions table.
  /// </summary>
  public static class TableRowsAndCells
  {
    public static readonly Regex
      __TR = new Regex(pattern: @"\<tr\>"),
      __TD = new Regex(pattern: @"\<td\>"),
      __TdEnd = new Regex(pattern: @"\<\/td\>"),
      __TdRowSpan = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>"),
      __TrEnd = new Regex(pattern: @"\<\/tr\>");

    public static Matching TR(string token)
    {
      Option<LinkedList<string>> list = Option.None<LinkedList<string>>();
      bool isMatch = false;
      var match = __TR.Match(token);
      isMatch = match != null && match.Length > 0;

      return new Matching() {
        IsMatch = isMatch,
        Parts = list
      };
    }

    public static Matching TdRowSpan(string token)
    {
      Option<LinkedList<string>> list = Option.None<LinkedList<string>>();
      bool isMatch = false;

      return new Matching() {
        IsMatch = isMatch,
        Parts = list
      };
    }
    
  }
}