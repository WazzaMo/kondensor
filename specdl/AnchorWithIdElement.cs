/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;

public struct AnchorWithIdElement : IElement
{
  //        <a id="awsaccountmanagement-CloseAccount"></a>
  private static readonly Regex _AId = new Regex(pattern: @"\<a\w+id=""([\-\w\s]*)"".*$");

  private static readonly Regex _AEnd = new Regex(pattern: @"\<\/a\>");

  private Option<string> _Id;

  public AnchorWithIdElement()
  {
    _Id = Option.None<string>();
  }

  public bool IsFinalMatch(string line)
  {
    var match = _AEnd.Match(line);
    return match != null && match.Length > 0;
  }

  public bool IsMatch(string line)
  {
    bool isOk = Matches(out Match match, line);
    if (isOk)
    {
      string id = match.Groups[1].Value;
      _Id = Option.Some(id);
    }
    return isOk;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    IContext result;
    var match = _AEnd.Match(line);
    if (IsFinalMatch(line) && context is ActionsTableContext actions)
    {
      string id = "UNKNOWN";
      _Id.MatchSome( i => id = i);
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