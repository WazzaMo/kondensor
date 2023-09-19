/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.Text.RegularExpressions;

using kondensor.Parser;


namespace kondensor.Parser.AwsHtmlParse.Frag;

public static class UtilsFragHtml
{
  public static bool TryGetText(Matching matching, string key, out string text)
  {
    bool isOk = matching.HasNamedGroups;
    if (isOk)
    {
      isOk = matching.TryGetNamedPart(key, out text);
    }
    else
      text = "";
    return isOk;
  }
}