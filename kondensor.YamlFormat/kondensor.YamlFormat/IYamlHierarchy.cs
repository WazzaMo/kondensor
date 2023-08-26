/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Text;
using System.Collections.Generic;


namespace kondensor.YamlFormat;


public interface IYamlHierarchy
{
  IYamlHierarchy FieldAndValue(string field, string value);
  IYamlHierarchy List<T>(List<T> items, Action<T, IYamlValues> handler);
  IYamlHierarchy List<T>(IEnumerable<T> items, Action<T, IYamlValues> handler);
  
  IYamlHierarchy DeclarationLine(string declared, Action<IYamlHierarchy> handler);
  IYamlHierarchy Field(string field, Action<IYamlValues> handler);

  IYamlHierarchy Comment(string message);
}