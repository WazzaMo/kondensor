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
    // IncIndent();
    handler();
    // DecIndent();
    DecIndent();
    return this;
  }

  IYamlHierarchy IYamlHierarchy.FieldAndValue(string field, string value)
  {
    _Writer.Indent(_Indent).Key(field).WriteFragment(value);
    return this;
  }

  IYamlHierarchy IYamlHierarchy.List<T>(List<T> items, Action<T, IYamlValues> handler)
  {
    IYamlValues yaml = this;
    IPipeWriter writer = _Writer;

    int indent = _Indent;
    IncIndent();

    items.ForEach( item => {
      writer.Indent(indent).WriteFragment(YamlUtils.SEQUENCE);
      handler(item, yaml);
      writer.EndLine();
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

    int indent = _Indent;
    IncIndent();

    items.ForEach( (item,_) => {
      writer.Indent(indent).WriteFragment(YamlUtils.SEQUENCE);
      handler(item, yaml);
      writer.EndLine();
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
    _Writer.EndLine(); // TODO
    return this;
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