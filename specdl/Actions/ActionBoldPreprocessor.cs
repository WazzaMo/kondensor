/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;

using kondensor.Parser;
using kondensor.Pipes;

namespace Actions;

public struct ActionBoldPreprocessor : IPreprocessor
{
  private static readonly char[]
    SEARCH_START = "<b>".ToCharArray(),
    SEARCH_END = "</b>".ToCharArray(),
    REPLACE = "!".ToCharArray();

  public bool IsMatch(char[] textToMatch)
  {
    Span<char> text = new Span<char>(textToMatch);
    Span<char>
      start = new Span<char>(SEARCH_START),
      end = new Span<char>(SEARCH_END);
    
    bool hasStartAndEnd =
      PreprocessorUtils.FindNextMatch(text, start, startIndex:0, out int matchStart)
      && PreprocessorUtils.FindNextMatch(text, end, startIndex: 0, out int matchEnd);
    return hasStartAndEnd;
  }

  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    Span<char> text = new Span<char>(inputText);
    Span<char>
      start = new Span<char>(SEARCH_START),
      end = new Span<char>(SEARCH_END);

    bool hasStartAndEnd =
      PreprocessorUtils.FindNextMatch(text, start, startIndex:0, out int matchStart)
      && PreprocessorUtils.FindNextMatch(text, end, startIndex: 0, out int matchEnd);
    if (hasStartAndEnd)
    {
      Span<char> replacement = new Span<char>(REPLACE);
      var processedStart = PreprocessorUtils.ReplaceFull(text, start, replacement);
      var processedEnd = PreprocessorUtils.ReplaceFull(processedStart, end, replacement);
      processedText = processedEnd.ToArray();
    }
    else
      processedText = new char[0];
    return hasStartAndEnd;
  }
}