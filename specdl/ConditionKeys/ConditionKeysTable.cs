/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;

namespace ConditionKeys;

public struct ConditionKeysTable
{
  internal class InternalData
  {
    internal List<ConditionKeyEntry> _Entries;

    internal InternalData()
    {
      _Entries = new List<ConditionKeyEntry>();
    }
  }

  private InternalData _Data;

  public ConditionKeysTable()
  {
    _Data = new InternalData();
  }

  public ParseAction ParseConditionKeysTable(ParseAction parser)
  {
    parser
      .Expect(ConditionKeysTableParser.Parser)
      .AllMatchThen( (list, writer) => {
        //
        Console.WriteLine($"{list.Count} annotations found");
      });
    return parser;
  }

}