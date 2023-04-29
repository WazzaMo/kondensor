/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableDataActionDescriptionAndAccessLevelElement : IElement
{
  private readonly static Regex TdataPattern = new Regex(pattern: @"\<td\>([\w\s\(\*\)]+)");

  public bool IsMatch(string line)
    => false;

  public bool IsFinalMatch(string line)
  {
    var match = TdataPattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result;
    Match match;

    match = TdataPattern.Match(line);
    if (match != null && match.Length > 0 && context is ActionsTableContext actions)
    {
      string value = match.Groups[1].Value;
      result = UpdateWithText(actions, value);
    }
    else
      result = context;

    return result;
  }

  private const string
    WRITE = "Write",
    READ = "Read",
    LIST = "List";

  private ActionsTableContext UpdateWithText(ActionsTableContext actions, string value)
  {
    if (actions.CurrentDescription.HasValue)
    {
      if (actions.CurrentAccessLevel == ActionAccessLevel.Unknown)
      {
        actions.CurrentAccessLevel = value switch {
          WRITE => ActionAccessLevel.Write,
          READ => ActionAccessLevel.Read,
          LIST => ActionAccessLevel.List,
          _ => ActionAccessLevel.Unknown
        };
        if (actions.CurrentAccessLevel == ActionAccessLevel.Unknown)
        {
          Console.WriteLine($"Could not interpret access level {value}");
          throw new Exception("Bug: td value interpreted as access level but cannot be resolved: " + value);
        }
      }
      else
      {
        throw new Exception($"Bug: td value {value} found but both description and access level are set!");
      }
    }
    else
    {
      actions.SetDescription(value);
    }
    return actions;
  }
}