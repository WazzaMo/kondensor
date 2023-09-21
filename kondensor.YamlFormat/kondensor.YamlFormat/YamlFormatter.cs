/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using kondensor.Pipes;

namespace kondensor.YamlFormat;

public struct YamlFormatter : IYamlHierarchy, IYamlValues
{
  const int SPLIT_LEN = 40;
  const char
    SPLIT_ON = ' ',
    COMMENT = '#';

  private readonly static Regex SUBLINE_SPLIT = new Regex(@"(.{"+ SPLIT_LEN + @"}\w*)");

  private int[] __Indent = new int[1]{0};
  private int _Indent {
    get => __Indent[0];
    set => __Indent[0] = value;
  }

  private IPipeWriter _Writer;

  public YamlFormatter(IPipeWriter writer)
  {
    _Writer = writer;
    _Indent = 0;
  }

  IYamlValues IYamlValues.Quote(string quoted)
  {
    _Writer.Quote(quoted);
    return this;
  }

  IYamlValues IYamlValues.Url(string url)
  {
    _Writer.Url(url);
    return this;
  }

  IYamlValues IYamlValues.Value(string value)
  {
    _Writer.WriteFragment(value);
    return this;
  }

  IYamlValues IYamlValues.ObjectListItem(string key, Action handler)
  {
    _Writer.KeyLine(key);

    IncIndent();
    handler();
    DecIndent();
    return this;
  }

  IYamlValues IYamlValues.ShortComment(string message)
  {
    string truncatedOrShort;

    if (message.Length > SPLIT_LEN)
    {
      int space = message.IndexOf(SPLIT_ON, SPLIT_LEN);
      truncatedOrShort = message.Substring(0, space) + "...";
    }
    else
      truncatedOrShort = message;
    CommentLine(truncatedOrShort);
    return this;
  }

  IYamlHierarchy IYamlHierarchy.FieldAndValue(string field, string value)
  {
    _Writer.Indent(_Indent).Key(field).WriteFragmentLine(value);
    return this;
  }

  IYamlHierarchy IYamlHierarchy.List<T>(List<T> items, Action<T, IYamlValues> handler)
  {
    IYamlValues yaml = this;
    IPipeWriter writer = _Writer;
    Action end = LineEnd;

    int indent = _Indent;
    IncIndent();

    items.ForEach( item => {
      writer.Indent(indent).WriteFragment(YamlUtils.SEQUENCE);
      handler(item, yaml);
      end();
    });

    DecIndent();
    return this;
  }

  IYamlHierarchy IYamlHierarchy.List<T>(
    IEnumerable<T> items,
    Action<T, IYamlValues> handler
  )
  {
    IYamlValues yaml = this;
    IPipeWriter writer = _Writer;
    Action end = LineEnd;

    int indent = _Indent;
    IncIndent();

    items.ForEach( item => {
      writer.Indent(indent).WriteFragment(YamlUtils.SEQUENCE);
      handler(item, yaml);
      end();
    });

    DecIndent();
    return this;
  }

  IYamlHierarchy IYamlHierarchy.DeclarationLine(string declared, Action<IYamlHierarchy> handler)
  {

    _Writer.Indent(_Indent).KeyLine(declared);

    IncIndent();
    IYamlHierarchy yaml= this;
    handler(yaml);
    DecIndent();
    return this;
  }

  IYamlHierarchy IYamlHierarchy.Field(string field, Action<IYamlValues> handler)
  {
    IYamlValues yaml = this;
    _Writer.Indent(_Indent).Key(field);
    handler(yaml);
    LineEnd();
    return this;
  }

  IYamlHierarchy IYamlHierarchy.Comment(string message)
  {
    if (message.Length > SPLIT_LEN)
      SplitComment(message);
    else
      CommentLine(message);

    return this;
  }

  private void CommentLine(string text)
  {
    _Writer.Indent(_Indent).WriteFragmentLine($"{COMMENT} {text}");
  }

  private void SplitComment(string original)
  {
    string[] segments = SUBLINE_SPLIT.Split(original);
    Action<string> makeComment = CommentLine;

    Array.ForEach(segments, part => {
      if (part.Length > 0)
        makeComment(part);
    });
  }

  private void LineEnd()
  {
    _Writer.EndLine();
  }

  private void IncIndent()
  {
    _Indent = _Indent + 1;
  }

  private void DecIndent()
  {
    _Indent = (_Indent > 0)
      ? _Indent - 1
      : 0;
  }
}