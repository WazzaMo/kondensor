/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

/// <summary>
/// IsMatch flags the td start
/// IsFinalMatch flags the /td end
/// </summary>
public struct TableDataOrAnchorElement : IElement
{
  private readonly static Regex TdataStartPattern = new Regex(pattern: @"\<td\>");
  private readonly static Regex TdataEndPattern = new Regex(pattern: @"\<\/td\>");

  public bool IsMatch(string line)
  {
    var match = TdataStartPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public bool IsFinalMatch(string line)
  {
    var match = TdataEndPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    return context;
  }
}