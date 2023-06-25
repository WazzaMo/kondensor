/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Parser;

public static class PreprocessorUtils
{
  /// <summary>
  /// Matches two Span<char> values based on shortest common length.
  /// </summary>
  /// <param name="first">First Span<char></param>
  /// <param name="second">Second Span<char></param>
  /// <returns>True if shortest common length matches.</returns>
  public static bool IsContentEqual(Span<char> first, Span<char> second)
  {
    bool result = false;
    int min = Math.Min(first.Length, second.Length);

    if (min > 0)
    {
      result = true;
      for(int index = 0; result && index < min; index++)
      {
        result = first[index] == second[index];
      }
    }
    return result;
  }

  const int NO_MATCH_INDEX = -1;


  public static bool FindNextMatch(Span<char> longText, Span<char> search, int startIndex, out int matchIndex)
  {
    bool result = false;
    if (longText.Length > startIndex && search.Length > 0)
    {
      matchIndex = startIndex;
      while(! result && matchIndex < longText.Length)
      {
        var section = longText.Slice(matchIndex);
        result = IsContentEqual(section, search);
        if (! result)
          matchIndex++;
      }
      if (! result)
        matchIndex = NO_MATCH_INDEX;
    }
    else
    {
      matchIndex = NO_MATCH_INDEX;
      result = false;
    }
    return result;
  }

  public static int CountMatches(Span<char> longText, Span<char> search)
  {
    int count = 0;
    int index = 0;
    int matchIndex;

    while(index < longText.Length && FindNextMatch(longText, search, index, out matchIndex))
    {
      count++;
      index = matchIndex + 1;
    }
    return count;
  }

  /// <summary>
  /// Replaces text of cutLength as the given index in the outText.
  /// </summary>
  /// <param name="inText">Original text</param>
  /// <param name="replacement">text to replace at cut</param>
  /// <param name="cutIndex">Index of cut where replace will go</param>
  /// <param name="cutLength">Length of text to remove when inserting replacement.</param>
  /// <param name="outText">New version of text - must be pre-allocated</param>
  /// <returns>True if attempted when all the lengths, especially outText, are suitable.</returns>
  public static bool ReplaceText(Span<char> inText, Span<char> replacement, int cutIndex, int cutLength, Span<char> outText)
  {
    int lengthNeeded = inText.Length + replacement.Length - cutLength;
    bool isAttempted
      = inText.Length > 0
      && replacement.Length > 0
      && cutIndex > 0 && cutIndex < inText.Length
      && cutLength > 0 && (cutIndex + cutLength) > inText.Length
      && outText.Length >= lengthNeeded;

    if (isAttempted)
    {
      var passThrough = inText.Slice(0, cutIndex);
      passThrough.CopyTo(outText);

      var destReplacement = outText.Slice(cutIndex, replacement.Length);
      replacement.CopyTo(destReplacement);

      var destRemaining = outText.Slice(cutIndex + replacement.Length);
      var sourceRemainingAfterCut = inText.Slice(cutIndex + cutLength);
      sourceRemainingAfterCut.CopyTo(destRemaining);
    }
    return isAttempted;
  }
}
