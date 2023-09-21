/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace kondensor.YamlFormat;


public static class IEnumerableExt
{
  public static void ForEach<T>( this IEnumerable<T> collection, Action<T> action)
  {
    for(int index = 0; index < collection.Count(); index++)
    {
      action(collection.ElementAt(index) );
    }
  }
}