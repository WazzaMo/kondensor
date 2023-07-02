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

public struct YamlFormatter : IYamlHierarchy, IYamlValues
{
  private int _Indent;
  private IPipeWriter _Writer;

  public YamlFormatter(IPipeWriter writer)
  {
    _Writer = writer;
    _Indent = 0;
  }

  IYamlHierarchy IYamlValues.Line()
  {
    _Writer.EndLine();
    return (IYamlHierarchy) this;
  }

  IYamlValues IYamlValues.Field(string field)
  {
    _Writer.Key(field);
    return this;
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

  IYamlHierarchy IYamlValues.DeclarationLine(string field)
  {
    _Writer.KeyLine(field);
    _Indent++;
    return this;
  }

  IYamlHierarchy IYamlHierarchy.FieldAndValue(string field, string value)
  {
    _Writer.Indent(_Indent).Key(field).WriteFragmentLine(value);
    return this;
  }

  IYamlValues IYamlHierarchy.List()
  {
    _Writer.Indent(_Indent).WriteFragment(YamlUtils.SEQUENCE);
    return this;
  }

  IYamlHierarchy IYamlHierarchy.DeclarationLine(string declared)
  {
    _Writer.Indent(_Indent).KeyLine(declared);
    _Indent++;
    return this;
  }

  IYamlHierarchy IYamlHierarchy.EndDecl()
  {
    _Indent--;
    return this;
  }

  IYamlValues IYamlHierarchy.Field(string field)
  {
    _Writer.Indent(_Indent).Key(field);
    return this;
  }
}