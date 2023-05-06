/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct AnchorWithKeyHrefElement : IElement
{
  //     <a href="#awsaccountmanagement-account">account</a>
  private static readonly Regex _AId = new Regex(pattern: @"\<a\w+id=""([\-\w\s]*)"".*$");

  public bool IsFinalMatch(string line)
  {
    return Matches(out var match, line);
  }

  public bool IsMatch(string line)
    => false;

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    const int ID_INDEX = 1;

    IContext result;
    if (Matches(out var match, line) && context is ActionsTableContext actions)
    {
      string id = match.Groups[ID_INDEX].Value;
      actions.SetActionId(id);
      result = actions;
    }
    else
      throw new ArgumentException($"Bug: Context expected to be {nameof(ActionsTableContext)}");
    return result;
  }

  private bool Matches(out Match match, string line)
  {
    match = _AId.Match(line);
    return match != null && match.Length > 0;
  }
}