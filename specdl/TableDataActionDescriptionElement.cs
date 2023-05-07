/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System.Text.RegularExpressions;

public struct TableDataActionDescriptionElement : IElement
{
  private readonly static Regex
    __TdNoAttribPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]*)"),
    __TdRowspanPattern = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>([\w\s\(\*\)]+)");

  private readonly static Regex __TEndTd = new Regex(pattern: @"\<\/td\>");

  private Option<string> _Text;

  public TableDataActionDescriptionElement()
  {
    _Text = Option.None<string>();
  }

  public bool IsMatch(string line)
  {
    string description;
    bool isMatch = IsNoAttribTd(out description, line)
      || IsAttribeTd(out description, line);
    if (isMatch)
    {
      _Text = Option.Some(description);
    }
    return isMatch;
  }

  public bool IsFinalMatch(string line)
  {
    var match = __TEndTd.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result;

    if (_Text.HasValue && context is ActionsTableContext actions)
    {
      string value = "UNKNOWN";
      _Text.MatchSome( t => value = t);
      result = UpdateWithText(actions, value);
    }
    else
      result = context;

    return result;
  }

  const int NO_ATTRIB_DESC_INDEX = 1,
    ATTRIB_NAME_INDEX = 1,
    ATTRIB_VALUE_INDEX = 2,
    ATTRIB_THEN_DESC_INDEX = 3;

  private bool IsNoAttribTd(out string description, string line)
  {
    var match = __TdNoAttribPattern.Match(line);
    bool isMatch = false;
    if (match != null && match.Length > 0)
    {
      isMatch = true;
      description = match.Groups[NO_ATTRIB_DESC_INDEX].Value;
    }
    else
      description = "";
    return isMatch;
  }

  private bool IsAttribeTd(out string description, string line)
  {
    var match = __TdRowspanPattern.Match(line);
    bool isMatch = false;
    if (match != null && match.Length > 0)
    {
      string attrib = match.Groups[ATTRIB_NAME_INDEX].Value;
      string value = match.Groups[ATTRIB_VALUE_INDEX].Value;
      description = match.Groups[ATTRIB_THEN_DESC_INDEX].Value;
      isMatch = true;
    }
    else
      description = "";
    return isMatch;
  }

  private ActionsTableContext UpdateWithText(ActionsTableContext actions, string value)
  {
    actions.SetDescription(value);
    return actions;
  }
}