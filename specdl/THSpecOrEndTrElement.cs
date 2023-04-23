/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct THSpecOrEndTrElement : IElement
{
  const string
    ACTIONS = "Actions",
    RESOURCE_TYPES = "Resource types",
    CONDITION_KEYS = "Condition keys";
  

  private readonly static Regex SpecPattern = new Regex(pattern: @"\<th\>([\w\s\(\*\)]+)\<\/th\>");
  private readonly static Regex EndTrPattern = new Regex(pattern: @"<\/tr\>");

  public bool IsMatch(string line)
    => IsSpecMatch(line);

  public bool IsFinalMatch(string line)
  {
    return IsEndTrMatch(line);
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result = new NoneContext();
    Match match;
    
    if (IsSpecMatch(line) && context is NoneContext)
    {
      match = SpecPattern.Match(line);

      IContext newHeader = new TableHeaderContext()
      {
        Headings = AddMatchTo(new List<string>(), match),
        Kind = TablePurpose.Unknown
      };
      result = newHeader;
    }
    else if (IsSpecMatch(line) && context is TableHeaderContext header)
    {
      match = SpecPattern.Match(line);
      header.Headings = AddMatchTo(header.Headings, match);
      result = header;
    }
    else if (IsEndTrMatch(line) && context is TableHeaderContext completeHeader )
    {
      completeHeader.Kind = GetKindFrom(completeHeader.Headings);
      result = completeHeader;
    }

    return result;
  }

  private static TablePurpose GetKindFrom(List<string> headings)
  {
    if (headings.Count < 1)
      return TablePurpose.Unknown;
    
    TablePurpose kind = headings[0] switch {
      ACTIONS => TablePurpose.Actions,
      RESOURCE_TYPES => TablePurpose.ResourceTypes,
      CONDITION_KEYS => TablePurpose.ConditionKeys,
      _ => TablePurpose.Unknown
    };
    return kind;
  }

  private List<string> AddMatchTo(List<string> list, Match match)
  {
    const int STRING_INDEX = 1;
    string content = match.Groups[STRING_INDEX].Value;

    list.Add(content);
    return list;
  }

  private bool IsSpecMatch(string line)
  {
    bool result = false;
    var match = SpecPattern.Match(line);
    result = match != null && match.Length > 0;
    
    return result;

  }

  private bool IsEndTrMatch(string line)
  {
    bool result = false;
    var match = EndTrPattern.Match(line);
    result = match != null && match.Length > 0;

    return result;
  }
}