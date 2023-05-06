/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;

public struct AnchorWithDocHrefElement : IElement
{
  //        <a href="https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html">CloseAccount</a> [permission only]</td>
  private static readonly Regex _AHrefName = new Regex(pattern: @"\<a\shref=""([*:-_./*\w\s\./\-^\""]+).\>([\-\w\s]*)$");
  private static readonly Regex _AEnd = new Regex(pattern: @"\<\/a\>.*$");

  private Option<string> _DocLink;
  private Option<string> _Name;

  public AnchorWithDocHrefElement()
  {
    _DocLink = Option.None<string>();
    _Name = Option.None<string>();
  }

  public bool IsFinalMatch(string line)
    => FinalMatch(line, out var match);

  public bool IsMatch(string line)
  {
    const int
      HREF_TEXT = 1,
      VALUE_TEXT = 2;

    bool isMatch = Matches(out var match, line);
    if (isMatch)
    {
      string docUrl = match.Groups[HREF_TEXT].Value;
      string ActionName = match.Groups[VALUE_TEXT].Value;
      _DocLink = Option.Some(docUrl);
      _Name = Option.Some(ActionName);
    }
    return isMatch;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {

    IContext result;

    if ( ! (context is ActionsTableContext) )
      throw new ArgumentException($"Bug: Context expected to be {nameof(ActionsTableContext)} and values were expected");

    if (FinalMatch(line, out Match match) && _DocLink.HasValue && _Name.HasValue && context is ActionsTableContext actions )
    {
      string docUrl = "UNKNOWN";
      string ActionName = "NONAME";

      _DocLink.Match(
        some: a => docUrl = a,
        none: () => throw new Exception("Documentation link missing in line: " + line)
      );
      _Name.Match(
        some: n => ActionName = n,
        none: () => throw new Exception("Action name missing in line: " + line)
      );
      actions.SetDocLinkAndName(docUrl, ActionName);
      result = actions;
    }
    else
      result = context;
      
    return result;
  }

  private bool Matches(out Match match, string line)
  {
    match = _AHrefName.Match(line);
    return match != null && match.Length > 0;
  }

  private bool FinalMatch(string line, out Match match)
  {
    match = _AEnd.Match(line);
    return match != null && match.Length > 0;
  }
}