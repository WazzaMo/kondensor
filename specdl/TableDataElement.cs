/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

using Optional;

public struct TableDataElement : IElement
{
  private const string ROWSPAN = "rowspan";

  private readonly static Regex _TdPattern = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>|\<td\>");

  private Option<string> _NumberOfRows;


  public TableDataElement()
  {
    _NumberOfRows = Option.None<string>();
  }

  public bool IsMatch(string line) => false;

  public bool IsFinalMatch(string line)
  {
    var match = _TdPattern.Match(line);
    if (match.Groups.Count>1)
    {
      var groups = match.Groups;
      string attribute = groups[1].Value;
      string size = groups[2].Value;

      if (attribute.Length > 0)
      {
        if (attribute.Equals(ROWSPAN))
        {
          _NumberOfRows = Option.Some(size);
        }
        else
        {
          throw new Exception(message: $"Bug: expected rowspan attribute but had: {attribute}");
        }
      }
    }
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result = context;
    if (context is ActionsTableContext actions)
    {
      _NumberOfRows.MatchSome(numString => actions.SetResourceTypeExpectedNumRows(numString));
      result = actions;
    }
    return result;
  }
}