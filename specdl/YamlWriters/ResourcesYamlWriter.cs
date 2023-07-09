/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using Actions;
using Resources;

namespace YamlWriters;

public static class ResourcesYamlWriter
{
  const string
    RESOURCES_TABLE = "ResourcesTable",
    HEADINGS = "Headings",
    RESOURCES = "Resources",
    RES_ITEM = "Resource",
    ID = "Id",
    LINK = "Link",
    NAME = "Name",
    ARN = "ArnSpec",
    CONDITION_KEYS = "ConditionKey",
    CK_TEMPLATE = "Template"
    ;
  public static void WriteTable(
    List<string> headings,
    List<ResourceDefinition> resources,
    YamlFormatter formatter
  )
  {
    IYamlHierarchy yaml = formatter;

    yaml.DeclarationLine(RESOURCES_TABLE, _ => {
      yaml.DeclarationLine(HEADINGS, _ => {
        yaml.List(headings, (hdg, yVal) => yVal.Quote(hdg));
      });
      yaml.DeclarationLine(RESOURCES, _ => {
        yaml.List(resources, (resrc, yVal) => {
          yVal.ObjectListItem(RES_ITEM, () => {
            yaml
              .Field(ID, yVal => yVal.Quote(resrc.Id) )
              .Field(NAME, yVal => yVal.Value(resrc.Name))
              .Field(LINK, yVal => yVal.Url(resrc.ApiLink));
            resrc.ConditionKey.MatchSome(rCondKey => {
              yaml.DeclarationLine( CONDITION_KEYS, _ => {
                yaml
                  .Field(ID, yVal => yVal.Quote( rCondKey.Id ) )
                  .Field(CK_TEMPLATE, yVal => yVal.Quote( rCondKey.Template));
              });
            });
          });
        });
      });
    });
  }

}