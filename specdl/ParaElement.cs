/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct ParaElement : IElement
{
  private readonly static Regex _ParaPattern = new Regex(pattern: @"\<p\>.*$");

  public bool IsMatch(string line) => false;

  public bool IsFinalMatch(string line)
  {
    var match = _ParaPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
    => context;
}