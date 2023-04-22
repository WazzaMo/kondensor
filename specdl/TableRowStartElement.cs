/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableRowStartElement : IElement
{
  private readonly static Regex Table = new Regex(pattern: @"\<tr.*");

  public bool IsMatch(string line)
    => false;

  public bool IsFinalMatch(string line)
  {
    var match = Table.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    bool isMatch = IsFinalMatch(line);
    IContext result;
    result = isMatch ? new TableHeaderContext() : new NoneContext();
    return result;
  }
}