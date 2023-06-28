/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;

using Parser;

namespace test.HtmlParse;


public struct ConfigurablePreprocessor : IPreprocessor
{
  private char[] _Search;
  private char[] _Replace;

  public ConfigurablePreprocessor(string search, string replace)
  {
    _Search = search.ToCharArray();
    _Replace = replace.ToCharArray();
  }

  public bool IsMatch(char[] textToMatch)
  {
    Span<char> search = new Span<char>( _Search );
    Span<char> text = new Span<char>(textToMatch);
    return PreprocessorUtils.FindNextMatch(text, search, startIndex: 0, out int index);
  }
  
  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    Span<char> text = new Span<char>(inputText);
    Span<char> search = new Span<char>( _Search );

    bool isMatch = PreprocessorUtils.FindNextMatch(text, search, startIndex: 0, out int index);
    if (isMatch)
    {
      Span<char> replacement = new Span<char>( _Replace );
      Span<char> processed = PreprocessorUtils.ReplaceFull(text, search, replacement);
      processedText = processed.ToArray();
    }
    else
      processedText = new char[0];
    return isMatch;
  }
}

