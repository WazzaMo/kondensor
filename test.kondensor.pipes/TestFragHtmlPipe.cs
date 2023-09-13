/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Xunit;

using kondensor.Pipes;

using System;
using System.IO;
using System.Dynamic;

namespace test.kondensor.pipes;

public class TestFragHtmlPipe
{
  private FragHtmlPipe Subject;

  public TestFragHtmlPipe()
  {
    var output = new TextPipeWriter(Console.Out);

    Subject = new FragHtmlPipe(
      FixtureFragHtml.HtmlToFrag(),
      output
    );
  }

  [Fact]
  public void Start_of_table_tag_is_first_token()
  {
    const string EXPECTED = "<table";
    Assert.True(Subject.ReadToken(out string token));
    Assert.Equal(EXPECTED, token);
  }

  [Fact]
  public void second_token_is_table_id_attribute()
  {
    const string EXPECTED = "id=\"actionFixture\"";
    string token;

    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.Equal(EXPECTED, token);
  }
}