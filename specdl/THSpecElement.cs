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
    IContext result = new NoneContext();

    var match = Pattern.Match(line);
    if (match != null && match.Length > 0 && context is TableHeaderContext header)
    {
      header.Headings = AddMatchTo(header.Headings, match);
      result = header;
    }
    else if (match != null && match.Length > 0 && context is NoneContext)
    {
      header = new TableHeaderContext()
      {
        Headings = AddMatchTo(new List<string>(), match),
        Kind = TablePurpose.Unknown
      };
      result = header;
    }
    return result;
  }

  private List<string> AddMatchTo(List<string> list, Match match)
  {
    const int STRING_INDEX = 1;
    string content = match.Groups[STRING_INDEX].Value;

    list.Add(content);
    return list;
  }
}