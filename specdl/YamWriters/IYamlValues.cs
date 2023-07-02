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

public interface IYamlValues
{

  IYamlValues Quote(string quoted);

  IYamlValues Url(string url);

  IYamlValues Value(string value);

  IYamlValues ObjectListItem<T>(T item, Action<T, IYamlHierarchy> handler);
}