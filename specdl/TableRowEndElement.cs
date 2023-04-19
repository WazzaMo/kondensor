/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Text.RegularExpressions;

public struct TableRowEndElement : IElement
{
  const string
    ACTIONS = "Actions",
    RESOURCE_TYPES = "Resource types",
    CONDITION_KEYS = "Condition keys";

  private readonly static Regex Pattern = new Regex(pattern: @"\<\/tr.*");

  public bool IsMatch(string line)
  {
    var match = Pattern.Match(line);
    return match != null && match.Length > 0;
  }

  public IContext Processed(string line, TextWriter output, IContext context)
  {
    bool isMatch = IsMatch(line);
    IContext result = context;

    if (isMatch && context is TableHeaderContext header)
    {
      string lead = header.Headings[0];
      TablePurpose kind = lead switch {
        ACTIONS => TablePurpose.Actions,
        RESOURCE_TYPES => TablePurpose.ResourceTypes,
        CONDITION_KEYS => TablePurpose.ConditionKeys,
        _ => TablePurpose.Unknown
      };
      header.Kind = kind;
      result = header;
    }
    return result;
  }
}