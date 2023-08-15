/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Optional;
using Parser;

namespace HtmlParse;

public static class HtmlPartsUtils
{
  const string EMPTY_STRING = "__EMPTY__";
  const int EMPTY_INT = -9999;

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
    Parts.MatchSome(list =>
      {
        if (list.Count >= HtmlPatterns.TD_ATTRIB_TAG_VALUE_IDX)
        {
          tagValue = list.ElementAt(HtmlPatterns.TD_ATTRIB_TAG_VALUE_IDX);
        }
        else if (list.Count == 1)
        {
          tagValue = list.ElementAt(HtmlPatterns.TD_TAG_VALUE_IDX);
        }
      }
    );
    return tagValue;
  }

  public static string GetTdAttribName(Option<LinkedList<string>> Parts)
  {
    string attribName = EMPTY_STRING;
    Parts.MatchSome(list =>
      attribName = list.Count >= HtmlPatterns.TD_ATTRIB_TAG_VALUE_IDX
        ? list.ElementAt(HtmlPatterns.TD_ATTRIB_NAME_IDX)
        : EMPTY_STRING
    );
    return attribName;
  }

  public static string GetTdAttribValue(Option<LinkedList<string>> Parts)
  {
    string attribValue = EMPTY_STRING;
    Parts.MatchSome(
      list => attribValue = list.Count > 0
       ? list.ElementAt(HtmlPatterns.TD_ATTRIB_VALUE_IDX)
       : EMPTY_STRING
    );
    return attribValue;
  }

  public static int GetTdAttribIntValue(Option<LinkedList<string>> Parts)
  {
    int value = EMPTY_INT;
    Parts.MatchSome(list => {
      string attribValue = list.ElementAt(HtmlPatterns.TD_ATTRIB_VALUE_IDX);
      value = Int32.Parse(attribValue);
    });
    return value;
  }

  public static string GetAIdAttribValue(Option<LinkedList<string>> Parts)
  {
    string attribValue = EMPTY_STRING;
    Parts.MatchSome(list => attribValue = list.ElementAt(HtmlPatterns.A_ID_VALUE_IDX));
    return attribValue;
  }

  public static string GetAHrefAttribValue(Option<LinkedList<string>> Parts)
  {
    string attribValue = EMPTY_STRING;
    Parts.MatchSome(list => attribValue = list.Count > 0
      ? list.ElementAt(HtmlPatterns.A_HREF_ATTRIB_VALUE_IDX)
      : EMPTY_STRING
    );
    return attribValue;
  }

  public static string GetAHrefTagValue(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.Last() );
    // list.ElementAt(HtmlPatterns.A_HREF_TAG_VALUE_IDX));
    return tagValue;
  }

  public static string GetAHrefTagValueLongShort(Matching matching)
  {
    string tagValue = EMPTY_STRING;
    if (matching.MatchResult == MatchKind.LongMatch && matching.Parts.HasValue)
    {
      matching.Parts.MatchSome(
        list => tagValue = list.ElementAt(HtmlPatterns.A_HREF_LONG_TAG_VALUE_IDX)
      );
    }
    else
    {
      matching.Parts.MatchSome(
        list => tagValue = list.ElementAt(HtmlPatterns.A_HREF_TAG_VALUE_IDX)
      );
    }
    return tagValue;
  }

  public static string GetPTagValue(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.ElementAt(HtmlPatterns.PARA_VALUE_IDX).Trim());
    return tagValue;
  }

  public static string GetCodeTagValue(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.ElementAt(HtmlPatterns.CODE_TAG_VALUE).Trim() );
    return tagValue;
  }

  public static string GetH6Value(Option<LinkedList<string>> Parts)
  {
    string tagValue = EMPTY_STRING;
    Parts.MatchSome(list => tagValue = list.ElementAt(HtmlPatterns.H6_TAG_VALUE).Trim());
    return tagValue;
  }

  public static bool IsEmptyPartsValue(this string value)
    => value == EMPTY_STRING;
  
  public static bool IsEmptyIntValue(this int value)
    => value == EMPTY_INT;
  
}