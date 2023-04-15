/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct THSpecElement : IElement
{
  private readonly static Regex Pattern = new Regex(pattern: @"\<th\>([\w\s\(\*\)]+)\<\/th\>");

  public bool IsMatch(string line)
  {
    var match = Pattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    const int STRING_INDEX = 1;
    IContext result = new NoneContext();

    var match = Pattern.Match(line);
    if (match != null && match.Length > 0 && context is TableHeader header)
    {
      string content = match.Groups[STRING_INDEX].Value;
      List<string> list = header.Headings;
      list.Add(content);
      header.Headings = list;
      result = header;
    }
    return result;
  }
}