/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableEndElement : IElement
{
  private readonly static Regex TableEnd = new Regex(pattern: @".*\<\/table.*");

  public bool IsMatch(string line)
    => false;

  public bool IsFinalMatch(string line)
  {
    var match = TableEnd.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    bool result = IsFinalMatch(line);
    return new NoneContext();
  }
}