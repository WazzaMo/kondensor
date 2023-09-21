/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Text;
using System.Collections.Generic;


namespace kondensor.YamlFormat;

public interface IYamlValues
{

  IYamlValues Quote(string quoted);

  IYamlValues Url(string url);

  IYamlValues Value(string value);

  IYamlValues ObjectListItem(string key, Action handler);
  IYamlValues ShortComment(string message);
}