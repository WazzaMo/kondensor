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


  /// <summary>
  /// Finds next match in text, given a starting position and search text.
  /// </summary>
  /// <param name="longText">Original text.</param>
  /// <param name="search">Text to seek</param>
  /// <param name="startIndex">Starting position</param>
  /// <param name="matchIndex">Index of match</param>
  /// <returns>True if match found.</returns>
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

  /// <summary>
  /// Counts all matches possible for original text and search text.
  /// </summary>
  /// <param name="longText">Original text.</param>
  /// <param name="search">Search text.</param>
  /// <returns>Count of matches made</returns>
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
  public static bool ReplaceTextCut(Span<char> inText, Span<char> replacement, int cutIndex, int cutLength, Span<char> outText)
  {
    int lengthNeeded = ComputeTargetLengthForSingleReplacement(inText.Length, cutLength, replacement.Length);
    bool isAttempted
      = inText.Length > 0
      && replacement.Length > 0
      && cutIndex > 0 && cutIndex < inText.Length
      && cutLength > 0 && (cutIndex + cutLength) <= inText.Length
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
    else
    {
      if (inText.Length <= 0) throw new ArgumentException("inText too short");
      if (replacement.Length <= 0) throw new ArgumentException(message: "replacement too short");
      if (cutIndex < 0 || cutIndex >= inText.Length) throw new ArgumentException("cutIndex invalid for inText length");
      if (cutLength <= 0 || (cutIndex + cutLength) > inText.Length) throw new ArgumentException("cutLength goes outside inText");
      if (outText.Length < lengthNeeded) throw new ArgumentException("outText not long enough");
    }
    return isAttempted;
  }

  /// <summary>
  /// Replaces all instances of search with replacement.
  /// </summary>
  /// <param name="text">Original text to search</param>
  /// <param name="search">Text to seek.</param>
  /// <param name="replacement">replacement text to insert</param>
  /// <returns>New span buffer with replaced text.</returns>
  public static Span<char> ReplaceFull(Span<char> text, Span<char> search, Span<char> replacement)
  {
    AssertValidLengths(text, search, replacement);

    int finalLength = ComputeTargetLenFullReplacement(text, search, replacement);

    int workingBufferLength
      = (search.Length > replacement.Length) ? text.Length
      : finalLength;
    
    Span<char> target = new Span<char>( new char[workingBufferLength]);
    text.CopyTo(target);

    int countReplacements = CountMatches(text, search);
    for(int index = -1, count = 0; count < countReplacements && FindNextMatch(target, search, index + 1, out index); count++)
    {
      ReplaceTextCut(target, replacement, index, search.Length, target);
    }
    return target.Slice(start: 0, length: finalLength);
  }

  /// <summary>
  /// Compute the buffer needed to store result for single replacement.
  /// </summary>
  /// <param name="inTextLen">Original text length</param>
  /// <param name="searchLen">Search fragment length</param>
  /// <param name="replacementLen">Replacement fragment length</param>
  /// <returns>new length</returns>
  public static int ComputeTargetLengthForSingleReplacement(int inTextLen, int searchLen, int replacementLen)
    => inTextLen + replacementLen - searchLen;
  
  /// <summary>
  /// Computes buffer needed to store result after full replacement.
  /// </summary>
  /// <param name="text">Original text</param>
  /// <param name="search">Search text</param>
  /// <param name="replacement">Replacement text</param>
  /// <returns>Size in chars needed.</returns>
  public static int ComputeTargetLenFullReplacement(Span<char> text, Span<char> search, Span<char> replacement)
  {
    if (text.Length == 0) throw new ArgumentException("text is empty");
    if (search.Length == 0) throw new ArgumentException("search is empty");
    if (replacement.Length == 0) throw new ArgumentException("replacement is empty");

    int numMatches = CountMatches(text, search);

    return text.Length + numMatches * (replacement.Length - search.Length);
  }

  private static void AssertValidLengths(Span<char> text, Span<char> search, Span<char> replacement)
  {
    if (text.Length == 0) throw new ArgumentException("inText too short");
    if (replacement.Length == 0) throw new ArgumentException(message: "replacement too short");
    if (search.Length == 0 ) throw new ArgumentException("search too short");
  }
}
