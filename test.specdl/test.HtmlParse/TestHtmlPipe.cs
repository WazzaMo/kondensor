/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Xunit;

using HtmlParse;
using Parser;

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace test.HtmlParse;


public struct FooPreprocessor : IPreprocessor
{
  const string SEARCH = "Foo";

  const string REPLACEMENT = "Fighter";

  public bool IsMatch(char[] textToMatch)
    => IsMatched(textToMatch, out int index);
  
  private bool IsMatched(char[] textToMatch, out int matchIndex)
  {
    bool matched = false;
    matchIndex = -1;

    for(int index = 0; !matched && index < textToMatch.Length - 3; index++)
    {
      matched = isMatch(index, textToMatch);
      if (matched)
        matchIndex = index;
    }
    return matched;
  }

  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    if ( inputText.Length > 0 && IsMatched(inputText, out int matchIndex))
    {
      char[] target = new char[inputText.Length + REPLACEMENT.Length - SEARCH.Length + 1];
      Span<char> destination = new Span<char>(target);
      Span<char> source = new Span<char>(inputText);

      source.Slice(0, matchIndex).CopyTo(destination);
      Span<char> replacement = new Span<char>( REPLACEMENT.ToCharArray() );
      var destReplacement = destination.Slice(matchIndex, replacement.Length);
      replacement.CopyTo(destReplacement);

      var destRemaining = destination.Slice(matchIndex + replacement.Length);
      source.Slice(matchIndex).CopyTo(destRemaining);
      processedText = destination.ToArray();
      return true;
    }
    else{
      processedText = new char[] {};
      return false;
    }
  }

  private bool isMatch(int startIndex, char[] text)
  {
    Span<char> seeking = new Span<char>(SEARCH.ToCharArray());
    Span<char> domain = new Span<char>(text).Slice(startIndex);
    bool isMatch = true;
    for(int index = 0; isMatch && index < seeking.Length && index < domain.Length; index++)
    {
      isMatch = seeking[index] == domain[index];
    }
    return isMatch;
  }
}

public class TestHtmlPipe
{
  private HtmlPipe _Subject;

  public TestHtmlPipe()
  {}

  [Fact]
  public void HtmlPipe_breaks_input_to_regular_elements()
  {
    const string Input = "<th>Resource types</th>";
    const string EXPECTED_Element = "<th>Resource types";

    _Subject = PipeFor(Input);
    Assert.True( _Subject.ReadToken(out string token));
    Assert.Equal( EXPECTED_Element, token);
  }

  [Fact]
  public void HtmlPipe_readsCodeStrippingSpanElements()
  {
    const string CodeFragment = "<code class=\"code\">arn:$<span>{</span>Partition}:account::$<span>{</span>Account}:account</code>";
    // const string StrippedValue = "<code class=\"code\">arn:${Partition}:account::${Account}:account";

    _Subject = PipeFor(CodeFragment);
    Assert.True( _Subject.ReadToken(out string token));
    // Assert.Equal( StrippedValue, token);
  }

  [Fact]
  public void HtmlPipe_handlesPreprocessors()
  {
    const string Fragment = "<p>Hi there Foo, this is Foo</p>";
    const string ExpectedToken = "<p>Hi there Fighter, this is Foo";

    _Subject = PipeFor(Fragment);
    _Subject.AddPreprocessor(new FooPreprocessor() );
    Assert.True(_Subject.ReadToken(out string token));
    Assert.Equal(ExpectedToken, token);
  }

  private HtmlPipe PipeFor(string value)
    => new HtmlPipe(ReaderFor(value), Console.Out);

  private StringReader ReaderFor(string value)
    => new StringReader(value);
}
