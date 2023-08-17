/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;

using Parser;

namespace Actions;

/// <summary>
/// This preprocessor handles <span> elements that sometimes appear in
/// Action HREFs and they block parsing. This replaces them with "".
/// <td>
///    <a id="action-id"></a>
///    <a href="https://base.url/path/" rel="noopener noreferrer" target="_blank"><span>ActionName</span></a>
/// </td>
/// </summary>
public struct ActionSpanPreprocessor : IPreprocessor
{
  private static readonly char[]
    SEARCH_START = "<span>".ToCharArray(),
    SEARCH_END = "</span>".ToCharArray(),
    REPLACE = "".ToCharArray();

  public bool IsMatch(char[] textToMatch)
    => MatchesStartAndEnd(textToMatch);

  /// <summary>
  /// Apply the preprocessor by searching and removing the offending
  /// part of the token text.
  /// </summary>
  /// <param name="inputText">Input token text</param>
  /// <param name="processedText">Output processed text</param>
  /// <returns>True if updated.</returns>
  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    bool isTextUpdated = false;
    if (MatchesStartAndEnd(inputText))
    {
      Span<char> replace = new Span<char>(REPLACE);
      Span<char> text = new Span<char>(inputText);
      Span<char> searchStart = new Span<char>(SEARCH_START);
      Span<char> searchEnd = new Span<char>(SEARCH_END);

      var processedStart = PreprocessorUtils.Remove(text, searchStart);
      var processedEnd = PreprocessorUtils.Remove(processedStart, searchEnd);
      processedText = processedEnd.ToArray();
      isTextUpdated = true;
    }
    else
    {
      processedText = new char[0];
    }
    return isTextUpdated;
  }

  private bool MatchesStartAndEnd(char[] textToMatch)
  {
    int index1, index2;

    Span<char> searchStart = new Span<char>(SEARCH_START);
    Span<char> searchEnd = new Span<char>(SEARCH_END);
    Span<char> text = new Span<char>( textToMatch );
    
    bool isMatchStart = PreprocessorUtils.FindNextMatch(
      text,
      searchStart,
      startIndex: 0,
      out index1
    );

    bool isMatchEnd = PreprocessorUtils.FindNextMatch(
      text,
      searchStart,
      startIndex: index1,
      out index2
    );

    return isMatchStart && isMatchEnd;
  }
}