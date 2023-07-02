/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Text;
using System.Collections.Generic;

using Parser;

namespace YamlWriters;

public ref struct YamlFormatter
{
  private int _Indent;
  private IPipeWriter _Writer;

  public YamlFormatter(IPipeWriter writer)
  {
    _Writer = writer;
    _Indent = 0;
  }

  public YamlFormatter Declaration(string key)
  {
    _Writer.Indent(_Indent).KeyLine(key);
    _Indent++;
    return this;
  }

  public YamlFormatter EndDecl()
  {
    _Indent--;
    return this;
  }

  public YamlFormatter Field(string fieldName)
  {
    _Writer.Indent(_Indent).Key(fieldName);
    return this;
  }

  public YamlFormatter Quote( string value)
  {
    _Writer.Quote(value);
    return this;
  }

  public YamlFormatter Line()
  {
    _Writer.EndLine();
    return this;
  }
}