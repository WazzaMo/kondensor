/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;

using Parser;

namespace test.HtmlParse;

/*
public struct OneMatchPreprocessor : IPreprocessor
{
  private string _Search;
  private string _Replacement;

  public OneMatchPreprocessor(string search, string replacement)
  {
    _Search = search;
    _Replacement = replacement;
  }

  public bool IsMatch(char[] textToMatch)
  {
    Span<char> text = new Span<char>(textToMatch);
    Span<char> search = new Span<char>(_Search.ToCharArray());

    bool result = 
      text.Length > 0
      && search.Length > 0
      && PreprocessorUtils.FindNextMatch(text, search, 0, out int match);
    return result;
  }

  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    bool result;
    Span<char> text = new Span<char>(inputText);
    Span<char> search = new Span<char>(_Search.ToCharArray());

    if (PreprocessorUtils.FindNextMatch(text, search, 0, out int matchIndex))
    {
      Span<char> replaced
    }
  }
}

*/