/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableHeadEndElement : IElement
{
  private readonly static Regex Table = new Regex(pattern: @"\<\/thead.*");

  public bool IsMatch(string line)
  {
    var match = Table.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    bool result = IsMatch(line);
    return new NoneContext();
  }
}