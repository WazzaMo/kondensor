/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.IO;

using Xunit;

using YamlWriters;
using Parser;

namespace test.YamlWriters;

public class TestYamlFormatter
{
  private TextWriter _TextWriter;

  public IPipeWriter Writer => (IPipeWriter) _TextWriter;

  public TestYamlFormatter()
  {
    _TextWriter = new TextWriter();
  }

  [Fact]
  public void Declaration_writes_key_and_indents_next_line()
  {
    const string
      KEY1 = "Document",
      KEY2 = "Heading",
      EXPECT = @"Document:
  Heading:
";

    var subject = new YamlFormatter(Writer);
    subject.Declaration(KEY1)
      .Declaration(KEY2);
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void EndDecl_reduces_indent()
  {
    const string
      KEY1 = "Document",
      KEY2 = "Heading",
      KEY3 = "NextHeading",
      EXPECT =
@"Document:
  Heading:
  NextHeading:
";

    var subject = new YamlFormatter(Writer);
    subject.Declaration(KEY1)
      .Declaration(KEY2)
      .EndDecl()
      .Declaration(KEY3);
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void Field_writes_key_and_value_line()
  {
    const string
      KEY1 = "Document",
      KEY2 = "Heading",
      FLD1 = "Name",
      VAL1 = "Fred",
      EXPECT = @"Document:
  Heading:
    Name: 'Fred'
";

    var subject = new YamlFormatter(Writer);
    subject.Declaration(KEY1)
      .Declaration(KEY2)
        .Field(FLD1).Quote(VAL1).Line();
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }
}