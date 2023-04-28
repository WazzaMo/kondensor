/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableDataOrParaElement : IElement
{
  private readonly static Regex TdataPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]+)");
  private readonly static Regex EndTrPattern = new Regex(pattern: @"\<\/tr\>");

  public bool IsMatch(string line)
  {
    var match = TdataPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public bool IsFinalMatch(string line)
  {
    var match = EndTrPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result;
    Match match;

    match = TdataPattern.Match(line);
    if (match != null && match.Length > 0 && context is ActionsTableContext actions)
    {
      // Generic case - revise!!
      result = actions;
    }
    else
      result = context;

    return result;
  }
}