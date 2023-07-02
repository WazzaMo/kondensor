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
  IYamlValues List();
  
  IYamlHierarchy DeclarationLine(string declared);
  IYamlValues Field(string field);

  IYamlHierarchy EndDecl();
}