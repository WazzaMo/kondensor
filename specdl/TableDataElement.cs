/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableDataElement : IElement
{
  private readonly static Regex _TdPattern = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>|\<td\>");

  public bool IsMatch(string line) => false;

  public bool IsFinalMatch(string line)
  {
    var match = _TdPattern.Match(line);
    if (match.Groups.Count>1)
    {
      var groups = match.Groups;
      string span = groups[0].Value;
      string size = groups[1].Value;
      Console.WriteLine($"TD: {span} and {size}");
    }
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
    => context;
}