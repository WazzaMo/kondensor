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
    const string SEARCH = "$<span>{</span>"; //"Foo";
    const string REPLACEMENT = "${";//"Fighter";
    
    const string CodeFragment = "<code class=\"code\">arn:$<span>{</span>Partition}:account::$<span>{</span>Account}:account</code>";
    const string StrippedValue = "<code class=\"code\">arn:${Partition}:account::${Account}:account";

    _Subject = PipeFor(CodeFragment);
    _Subject.AddPreprocessor(new ConfigurablePreprocessor(SEARCH, REPLACEMENT) );
    Assert.True( _Subject.ReadToken(out string token));
    Assert.Equal( StrippedValue, token);
  }

  [Fact]
  public void HtmlPipe_handlesPreprocessors()
  {
    const string Fragment = "<p>Hi there Foo, this is Foo</p>";
    const string ExpectedToken = "<p>Hi there Fighter, this is Fighter",
      Search = "Foo",
      Replace = "Fighter";

    _Subject = PipeFor(Fragment);
    _Subject.AddPreprocessor(new ConfigurablePreprocessor(Search, Replace) );
    Assert.True(_Subject.ReadToken(out string token));
    Assert.Equal(ExpectedToken, token);
  }

  private HtmlPipe PipeFor(string value)
    => new HtmlPipe(ReaderFor(value), Console.Out);

  private StringReader ReaderFor(string value)
    => new StringReader(value);
}

