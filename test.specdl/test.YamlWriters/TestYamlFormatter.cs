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

    IYamlHierarchy subject = new YamlFormatter(Writer);
    subject.DeclarationLine(KEY1)
      .DeclarationLine(KEY2);
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

    IYamlHierarchy subject = new YamlFormatter(Writer);
    subject.DeclarationLine(KEY1)
      .DeclarationLine(KEY2)
      .EndDecl()
      .DeclarationLine(KEY3);
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

    IYamlHierarchy subject = new YamlFormatter(Writer);
    subject.DeclarationLine(KEY1)
      .DeclarationLine(KEY2)
        .Field(FLD1).Quote(VAL1).Line();
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void List_withNoParams_writes_indent_keyVal_in_one()
  {
    const string
      DECL = "List",
      OBJECT = "ListItem",
      OBJ_KEY1 = "Name", Name = "Cloud",
      EXPECT =
@"List:
  - ListItem: 
  Name: Cloud
";
    IYamlHierarchy subject = new YamlFormatter(_TextWriter);
    subject.DeclarationLine(DECL)
      .List().Field(OBJECT).Line()
      .FieldAndValue(OBJ_KEY1, Name)
      ;
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void List_withNoParams_writes_indent_keyVal_in_parts()
  {
    const string
      DECL = "List",
      OBJECT = "ListItem",
      OBJ_KEY1 = "Link", link = "https://github.com",
      EXPECT =
@"List:
  - ListItem: 
  Link: 'https://github.com'
";
    IYamlHierarchy subject = new YamlFormatter(_TextWriter);
    subject.DeclarationLine(DECL)
      .List().Field(OBJECT).Line()
      .Field(OBJ_KEY1).Url(link).Line()
      ;
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

}