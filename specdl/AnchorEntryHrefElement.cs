/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;

public struct AnchorEntryHrefElement : IElement
{
  //    <a href="#awsaccountmanagement-account">account
  private static readonly Regex _AHrefName = new Regex(pattern: @"\<a\s+href=\""(\#[\-\s\w]+)\""\>([\w]+)");
  private static readonly Regex _AEnd = new Regex(pattern: @"\<\/a\>");

  private Option<string> _EntityRef;
  private Option<string> _EntityName;

  public AnchorEntryHrefElement()
  {
    _EntityRef = Option.None<string>();
    _EntityName = Option.None<string>();
  }

  public bool IsFinalMatch(string line)
    => FinalMatch(line, out var match);

  public bool IsMatch(string line)
  {
    const int HREF_TEXT = 1, REF_NAME = 2;

    bool isMatch = Matches(out var match, line);
    if (isMatch)
    {
      string refId = match.Groups[HREF_TEXT].Value;
      string name = match.Groups[REF_NAME].Value;
      Console.WriteLine($"Ref to entity {refId} of name {name}");
      _EntityRef = Option.Some(refId);
      _EntityName = Option.Some(name);
    }
    return isMatch;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {

    IContext result;

    if ( ! (context is ActionsTableContext) )
      throw new ArgumentException($"Bug: Context expected to be {nameof(ActionsTableContext)} and HREF value was expected");

    if (FinalMatch(line, out Match match) && _EntityRef.HasValue && context is ActionsTableContext actions )
    {
      string
        refId = "UNKNOWN",
        name = "UNKNOWN-NAME";

      _EntityRef.Match(
        some: a => refId = a,
        none: () => throw new Exception("Documentation link missing in line: " + line)
      );
      _EntityName.MatchSome(
        some: n => name = n
      );
      actions.SetResourceRefAndName(refId, name);
      
      Console.WriteLine($"Action ref to resource type: {refId} with name {name}");
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