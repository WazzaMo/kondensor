/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System.Text.RegularExpressions;

public struct TableDataActionAccessLevelElement : IElement
{
  private readonly static Regex 
      __TdNoAttribPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]*)"),
    __TdRowspanPattern = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>([\w\s\(\*\)]+)");
//TdataPattern = new Regex(pattern: @"\<td\s?(\w+)=?\""(\d+)\""?\>|\<td\>");
  private readonly static Regex TEndTd = new Regex(pattern: @"\<\/td\>");

  private Option<string> _Text;

  public TableDataActionAccessLevelElement()
  {
    _Text = Option.None<string>();
  }

  public bool IsMatch(string line)
  {
    string accessLevel;
    bool isMatch = IsAttribeTd(out accessLevel, line)
      || IsNoAttribTd(out accessLevel, line);
    if (isMatch)
      _Text = Option.Some(accessLevel);
    return isMatch;
  }

  public bool IsFinalMatch(string line)
  {
    var match = TEndTd.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result;

    if (_Text.HasValue && context is ActionsTableContext actions)
    {
      string? value = null;
      _Text.MatchSome( t => value = t);
      if (value != null)
        result = UpdateWithText(actions, value);
      else
      {
        actions.SetCurrentAccessLevel(ActionAccessLevel.Unknown);
        result = actions;
      }
    }
    else
      result = context;

    return result;
  }

  const int NO_ATTRIB_DESC_INDEX = 1,
    ATTRIB_NAME_INDEX = 1,
    ATTRIB_VALUE_INDEX = 2,
    ATTRIB_THEN_DESC_INDEX = 3;

  private bool IsNoAttribTd(out string accessLevel, string line)
  {
    var match = __TdNoAttribPattern.Match(line);
    bool isMatch = false;
    if (match != null && match.Length > 0)
    {
      isMatch = true;
      accessLevel = match.Groups[NO_ATTRIB_DESC_INDEX].Value;
    }
    else
      accessLevel = "";
    return isMatch;
  }

  private bool IsAttribeTd(out string accessLevel, string line)
  {
    var match = __TdRowspanPattern.Match(line);
    bool isMatch = false;
    if (match != null && match.Length > 0)
    {
      string attrib = match.Groups[ATTRIB_NAME_INDEX].Value;
      string value = match.Groups[ATTRIB_VALUE_INDEX].Value;
      accessLevel = match.Groups[ATTRIB_THEN_DESC_INDEX].Value;
      isMatch = true;
    }
    else
      accessLevel = "";
    return isMatch;
  }

  private const string
    WRITE = "Write",
    READ = "Read",
    LIST = "List",
    ARRAY_OF_STRING = "ArrayOfString",
    TAGGING = "Tagging";

  private ActionsTableContext UpdateWithText(ActionsTableContext actions, string value)
  {
    ActionAccessLevel accessLevel = value switch {
      "" => ActionAccessLevel.NotSpecified,
      WRITE => ActionAccessLevel.Write,
      READ => ActionAccessLevel.Read,
      LIST => ActionAccessLevel.List,
      ARRAY_OF_STRING => ActionAccessLevel.ArrayOfString,
      TAGGING => ActionAccessLevel.Tagging,
      _ => ActionAccessLevel.Unknown
    };

    if (accessLevel == ActionAccessLevel.Unknown)
    {
      Console.WriteLine($"Could not interpret access level {value}");
      throw new Exception("Bug: td value interpreted as access level but cannot be resolved: " + value);
    }

    actions.SetCurrentAccessLevel(accessLevel);

    return actions;
  }

}