/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.IO;
using System.Collections.Generic;

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
      EXPECT =
@"Document:
  Heading:
";

    IYamlHierarchy subject = new YamlFormatter(Writer);
    subject.DeclarationLine(
      KEY1,
      y1 => y1.DeclarationLine(KEY2,_ => {})
    );
    string text = _TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void DeclarationLine_blockend_reduces_indent()
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
    subject.DeclarationLine(KEY1, yaml =>{
      yaml
        .DeclarationLine(KEY2, _ =>{})
        .DeclarationLine(KEY3, _ => {});
    });
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
    subject.DeclarationLine(KEY1, yaml => {
      yaml
        .DeclarationLine(KEY2, _ =>
          yaml.Field(FLD1, yval => yval.Quote(VAL1))
        );
    });
      
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
@"
List:
  - ListItem:
      Name: Cloud
";
    Tuple<string, string> pair = new Tuple<string, string>(OBJ_KEY1, Name);
    List<Tuple<string,string>> theList = new List<Tuple<string, string>>();

    theList.Add(pair);

    IYamlHierarchy subject = new YamlFormatter(_TextWriter);
    subject.DeclarationLine(DECL, yaml => {
      yaml.List(
        theList,
        (p, yVal) => yVal.ObjectListItem(OBJECT, ()=> yaml.FieldAndValue(p.Item1, p.Item2))
      );
    });

    string text = "\n"+_TextWriter.ToString();
    Assert.Equal(EXPECT, text);
  }

  [Fact]
  public void ShortComment_truncates_long_statements_at_space()
  {
    const string
      LongStatement = "Hi there, this statement is too long and should be truncated.",
      Truncated = "Hi there, this statement is too long and...\n",
      Expected = "# " + Truncated;

    IYamlValues subject = new YamlFormatter(_TextWriter);
    subject.ShortComment(LongStatement);
    string text = _TextWriter.ToString();
    Assert.Equal(Expected, text);
  }

  [Fact]
  public void ShortComment_does_not_truncate_short_comments()
  {
    const string
      Statement = "Hi there, this statement is Short.",
      Truncated = "Hi there, this statement is Short.\n",
      Expected = "# " + Truncated;

    IYamlValues subject = new YamlFormatter(_TextWriter);
    subject.ShortComment(Statement);
    string text = _TextWriter.ToString();
    Assert.Equal(Expected, text);
  }

  [Fact]
  public void Comment_does_not_wrap_short_messages()
  {
    const string
      Comment = "Hi there, this statement is Short.",
      Expected = "# Hi there, this statement is Short.\n";
    
    IYamlHierarchy subject = new YamlFormatter(_TextWriter);
    subject.Comment(Comment);
    string text = _TextWriter.ToString();
    Assert.Equal(Expected, text);
  }

  [Fact]
  public void Comment_wraps_long_statements_at_space()
  {
    const string
      LongStatement = "Hi there, this statement is too long and should be wrapped.",
      Expected = "# " + "Hi there, this statement is too long and\n"
                +"# " + " should be wrapped.\n";

    IYamlHierarchy subject = new YamlFormatter(_TextWriter);
    subject.Comment(LongStatement);
    string text = _TextWriter.ToString();
    Assert.Equal(Expected, text);
  }
}