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

  private readonly string[] TOKENS = {
    "<table",
    "id=\"actionFixture\"",
    ">",
    "<thead",
    ">",
    "<tr",
    ">",
    "<th",
    ">",
    "Actions",
    "</th",
    ">",
    "<th",
    ">",
    "Description",
    "</th",
    ">",
    "<th",
    ">",
    "Access level",
    "</th",
    ">",
    "<th",
    ">",
    "Resource types (*required)",
    "</th",
    ">"
  };


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

  [Fact]
  public void third_token_is_tag_end()
  {
    const string EXPECTED = ">";
    string token;

    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.Equal(EXPECTED, token);
  }

  [Fact]
  public void fourth_token_is_tag_thead()
  {
    const string EXPECTED = "<thead";
    string token;

    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.Equal(EXPECTED, token);
  }

  [Fact]
  public void fifth_token_is_end_tag()
  {
    const string EXPECTED = ">";
    string token;

    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.Equal(EXPECTED, token);
  }

  [Fact]
  public void sixth_token_is_start_tag_tr()
  {
    const string EXPECTED = "<tr";
    string token;

    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.True(Subject.ReadToken(out token));
    Assert.Equal(EXPECTED, token);
  }

  [Fact]
  public void tokens_match_known_set()
  {
    string token;

    for(int index = 0; index < TOKENS.Length; index++)
    {
      token = "";
      Assert.True( Subject.ReadToken(out token) );
      Assert.Equal(TOKENS[index], token);
    }
  }

}