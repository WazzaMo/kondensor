/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using ConditionKeys;

namespace YamlWriters;



public static class ConditionKeysYamlWriter
{
  const string
    TABLE = "ConditionKeyDefinitions",
    HEADINGS = "Headings",
    CONDITIONS = "ConditionKeys",
    CK_ENTRY = "ConditionKey",
    ID = "Id",
    LINK = "ApiDocLink",
    DESCRIPTION = "Description",
    NAME = "Name",
    TYPE = "ValueType";
  
  public static void WriteYaml(
    List<string> headings,
    List<ConditionKeyEntry> entries,
    YamlFormatter formatter
  )
  {
    IYamlHierarchy yaml = formatter;
    Action<IYamlHierarchy> writeHeadings = (yy) => DeclareHeadings(yy,headings);
    Action<IYamlHierarchy> writeEntries = (yy) => DeclareEntries(yy, entries);

    yaml
      .DeclarationLine( TABLE, yObj => {
        yObj
          .DeclarationLine(HEADINGS, writeHeadings)
          .DeclarationLine(CONDITIONS, writeEntries);
      });
  }

  private static void DeclareHeadings(
    IYamlHierarchy yaml,
    List<string> headings
  )
  {
    yaml.List(headings,
      (item, yVal) => yVal.Quote(item)
    );
  }

  private static void DeclareEntries(
    IYamlHierarchy yaml,
    List<ConditionKeyEntry> entries
  )
  {
    Action<ConditionKeyEntry,IYamlValues> writeItem = CkEntry;
    yaml
      .List(entries,
        (item, yVal) => yVal.ObjectListItem(CK_ENTRY, () => writeItem(item,yVal))
      );
  }

  private static void CkEntry(
    ConditionKeyEntry entry,
    IYamlValues yVal
  )
  {
    YamlFormatter formatter = (YamlFormatter) yVal;
    IYamlHierarchy yaml = formatter;
    yaml
      .Field(ID, yy => yy.Quote(entry.Id))
      .Field(LINK, yy => yy.Url(entry.DocLink))
      .Field(NAME, yy => yy.Value(entry.Name))
      .Field(DESCRIPTION, yy => yy.Quote(entry.Description))
      .Field(TYPE, yy => yy.Value(entry.CkType.ToString()))
      ;
  }
}