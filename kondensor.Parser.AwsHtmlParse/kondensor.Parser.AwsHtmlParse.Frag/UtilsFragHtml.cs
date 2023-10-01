/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.Text.RegularExpressions;

using kondensor.Parser;


namespace kondensor.Parser.AwsHtmlParse.Frag;

/// <summary>
/// Utilities for REXEX named groups used in Fragment HTML declarations.
/// </summary>
public static class UtilsFragHtml
{
  /// <summary>Get text from named group using the key for that group.</summary>
  /// <param name="matching"><see href="Matching"/> struct value to check</param>
  /// <param name="key">Key for named group, as defined in the REGEX.</param>
  /// <param name="text">Text to be returned.</param>
  /// <returns>True if found, meaning the matching had a named group value and if the key were found.</returns>
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

  /// <summary>Get tag value text for <see href="FragmentsHtml.TAG_VALUE"/></summary>
  /// <param name="matching">Matching node to check</param>
  /// <param name="value">out param to return</param>
  /// <returns>True if value found.</returns>
  public static bool TryGetTagValue(Matching matching, out string value)
    => TryGetText( matching, FragmentsHtml.KEY_TAG_VALUE, out value);
  
  public static bool TryGetIdValue(Matching matching, out string id)
    => TryGetText( matching, FragmentsHtml.KEY_ID_VALUE, out id);
  
  public static bool TryGetHrefValue(Matching matching, out string href)
    => TryGetText(matching, FragmentsHtml.KEY_HREF_VALUE, out href);
}