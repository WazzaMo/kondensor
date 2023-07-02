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

public interface IYamlHierarchy
{
  IYamlHierarchy FieldAndValue(string field, string value);
  IYamlHierarchy List<T>(List<T> items, Action<T, IYamlValues> handler);
  
  IYamlHierarchy DeclarationLine(string declared, Action<IYamlHierarchy> handler);
  IYamlHierarchy Field(string field, Action<IYamlValues> handler);
}