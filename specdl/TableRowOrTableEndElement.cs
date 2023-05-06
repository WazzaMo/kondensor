/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

/// <summary>
/// Match means new row. FinalMatch means end table.
/// </summary>
public struct TableRowOrTableEndElement : IElement
{
  private readonly static Regex __EndTablePattern = new Regex(pattern: @"\<\/table\>");
  private readonly static Regex __StartRowPattern = new Regex(pattern: @"\<tr\>");

  public bool IsMatch(string line)
    => IsStartRow(line);

  public bool IsFinalMatch(string line)
    => IsTableEnd(line);

  public IContext Processed(string line, TextWriter output, IContext context)
    => context;

  private bool MatchResult(Match match) => match != null && match.Length > 0;

  private bool IsStartRow(string line)
  {
    var match = __StartRowPattern.Match(line);
    return MatchResult(match);
  }

  private bool IsTableEnd(string line)
  {
    var match = __EndTablePattern.Match(line);
    return MatchResult(match);
  }
}