/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Optional;

namespace HtmlParse;

public static class HtmlPartsUtils
{
  const string EMPTY_STRING = "__EMPTY__";

  public static string GetTableAttribName(Option<LinkedList<string>> Parts)
  {
    string attribName = EMPTY_STRING;
    Parts.MatchSome(list => attribName = list.ElementAt(HtmlPatterns.TABLE_ATTRIB_NAME_IDX));
    return attribName;
  }

  public static string GetTableAttribValue(Option<LinkedList<string>> Parts)
  {
    string attribValue = EMPTY_STRING;
    Parts.MatchSome(list => attribValue = list.ElementAt(HtmlPatterns.TABLE_ATTRIB_VALUE_IDX));
    return attribValue;
  }

  public static string GetThTagValue(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.ElementAt(HtmlPatterns.TH_VALUE_INDEX_IDX));
    return tagValue;
  }

  public static string GetTdTagValue(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.ElementAt(HtmlPatterns.TD_TAG_VALUE_IDX));
    return tagValue;
  }

  public static string GetTdAttribName(Option<LinkedList<string>> Parts)
  {
    string attribName = EMPTY_STRING;
    Parts.MatchSome(list => attribName = list.ElementAt(HtmlPatterns.TD_ATTRIB_NAME_IDX));
    return attribName;
  }

  public static string GetTdAttribValue(Option<LinkedList<string>> Parts)
  {
    string attribValue = EMPTY_STRING;
    Parts.MatchSome(list => attribValue = list.ElementAt(HtmlPatterns.TD_ATTRIB_VALUE_IDX));
    return attribValue;
  }

  public static bool IsEmptyPartsValue(this string value)
    => value == EMPTY_STRING;
}