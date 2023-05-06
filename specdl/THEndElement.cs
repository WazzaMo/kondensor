/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct THEndElement : IElement
{

  private readonly static Regex __EndPattern = new Regex(pattern: @"\<\/th\>");

  public bool IsMatch(string line)
    => false;

  public bool IsFinalMatch(string line)
  {
    return IsEndTrMatch(line);
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    return context;
  }

  private bool IsEndTrMatch(string line)
  {
    bool result = false;
    var match = __EndPattern.Match(line);
    result = match != null && match.Length > 0;

    return result;
  }
}