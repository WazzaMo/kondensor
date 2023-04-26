/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct AnchorWithDocHrefElement : IElement
{
  //        <a href="https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html">CloseAccount</a> [permission only]</td>
  private static readonly Regex _AHrefName = new Regex(pattern: @"\<a\w+href=""([\-\w\s]*)""\>([\-\w\s]*)$");

  public bool IsFinalMatch(string line)
  {
    return Matches(out var match, line);
  }

  public bool IsMatch(string line)
    => false;

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    const int
      HREF_TEXT = 1,
      VALUE_TEXT = 2;

    IContext result;
    if (Matches(out var match, line) && context is ActionsTableContext actions)
    {
      string docUrl = match.Groups[HREF_TEXT].Value;
      string ActionName = match.Groups[VALUE_TEXT].Value;
      actions.SetDocLinkAndName(docUrl, ActionName);
      result = actions;
    }
    else
      throw new ArgumentException($"Bug: Context expected to be {nameof(ActionsTableContext)}");
    return result;
  }

  private bool Matches(out Match match, string line)
  {
    match = _AHrefName.Match(line);
    return match != null && match.Length > 0;
  }
}