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

/*
public struct FooPreprocessor : IPreprocessor
{
  const string SEARCH = "$<span>{</span>"; //"Foo";

  const string REPLACEMENT = "${";//"Fighter";

  public bool IsMatch(Span<char> textToMatch)
    => IsMatched(textToMatch, 0, out int index);
  
  private bool IsMatched(Span<char> textToMatch, int startIndex, out int matchIndex)
  {
    bool matched = false;
    matchIndex = -1;

    Span<char> search = new Span<char>(SEARCH.ToCharArray());

    for(int index = startIndex; !matched && index < textToMatch.Length - search.Length; index++)
    {
      matched = isMatch(index, textToMatch);
      if (matched)
        matchIndex = index;
    }
    return matched;
  }

  public bool ProcessText(char[] inputText, out char[] processedText)
  {
    if ( inputText.Length > 0 && IsMatched(inputText, 0, out int matchIndex))
    {
      return ReplaceText(inputText, matchIndex, out processedText);
    }
    else{
      processedText = new char[] {};
      return false;
    }
  }

  private bool ReplaceText(Span<char> inText, int matchIndex, out Span<char> outText)
  {
    char[] target = new char[inputText.Length + REPLACEMENT.Length - SEARCH.Length + 1];
    Span<char> destination = new Span<char>(target);
    Span<char> source = new Span<char>(inputText);

    source.Slice(0, matchIndex).CopyTo(destination);
    Span<char> replacement = new Span<char>( REPLACEMENT.ToCharArray() );
    var destReplacement = destination.Slice(matchIndex, replacement.Length);
    replacement.CopyTo(destReplacement);

    var destRemaining = destination.Slice(matchIndex + replacement.Length);
    var sourceRemainingAfterCut = source.Slice(matchIndex + SEARCH.Length);
    sourceRemainingAfterCut.CopyTo(destRemaining);
    processedText = destination.ToArray();
    return true;
  }

  private bool isMatch(int startIndex, Span<char> text)
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
    const string StrippedValue = "<code class=\"code\">arn:${Partition}:account::${Account}:account";

    _Subject = PipeFor(CodeFragment);
    _Subject.AddPreprocessor(new FooPreprocessor() );
    Assert.True( _Subject.ReadToken(out string token));
    Assert.Equal( StrippedValue, token);
  }

  [Fact]
  public void HtmlPipe_handlesPreprocessors()
  {
    const string Fragment = "<p>Hi there Foo, this is Foo</p>";
    const string ExpectedToken = "<p>Hi there Fighter, this is Foo";

    _Subject = PipeFor(Fragment);
    _Subject.AddPreprocessor(new FooPreprocessor() );
    Assert.True(_Subject.ReadToken(out string token));
    // Assert.Equal(ExpectedToken, token);
  }

  private HtmlPipe PipeFor(string value)
    => new HtmlPipe(ReaderFor(value), Console.Out);

  private StringReader ReaderFor(string value)
    => new StringReader(value);
}

*/
