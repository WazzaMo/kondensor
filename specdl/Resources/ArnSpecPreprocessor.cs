/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;

using kondensor.Pipes;

namespace Resources;

/// <summary>
/// ARN resource specifications are templates with text that includes a <span> element for presenation reasons.
/// This preprocessor strips the presentation element, leaving just the template.
/// </summary>
public struct ArnSpecPreprocessor : IPreprocessor
{
  private static readonly char[]
    SEARCH = "$<span>{</span>".ToCharArray(),
    REPLACE = "${".ToCharArray();

  public bool IsMatch(char[] textToMatch)
  {
    Span<char> search = new Span<char>(SEARCH);
    Span<char> text = new Span<char>( textToMatch );
    
    return PreprocessorUtils.FindNextMatch(text, search, startIndex: 0, out int index);
  }

  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    Span<char>
      search = new Span<char>( SEARCH ),
      text = new Span<char>( inputText );
    
    bool isMatch = PreprocessorUtils.FindNextMatch( text, search, startIndex: 0, out int matchIndex);

    if (isMatch)
    {
      var replacement = new Span<char>( REPLACE );
      var processed = PreprocessorUtils.ReplaceFull(text, search, replacement);
      processedText = processed.ToArray();
    }
    else
      processedText = new char[0];
    return isMatch;
  }
}