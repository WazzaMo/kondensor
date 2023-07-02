/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;
using Actions;

namespace YamlWriters;



/// <summary>
/// Responsible only for the YAML output and formatting
/// of the actions table that was parsed.
/// </summary>
public static class ActionsYamlWriter
{
  public const string
    TABLE = "ActionsTable",
    ID = "Id",
    SRC_URL = "SourceUrl",
    HEADINGS = "Headings",
    UNESCQUOTE = "'",
    ESCQUOTE = "\"",
    ACTION_LIST = "Actions",
    ACTION_DEF = "Action",
    ACTION_NAME = "Name",
    RESOURCE_LIST = "Resources",
    RESOURCE_DEF = "Resource",
    RESOURCE_NAME = "Name",
    CONDITION_KEYS = "ConditionKeys",
    DEPENDENT_ACTIONS = "DependentActionIds",
    DESCRIPTION = "Description",
    API_URL = "ApiUrl";

  /// <summary>
  /// Writes YAML output for Actions table.
  /// </summary>
  /// <param name="sourceUrl">source page for table</param>
  /// <param name="headings">Headings declared in actions table</param>
  /// <param name="actions">Action definitions</param>
  /// <param name="writer">Writer pipe</param>
  public static void WriteYaml(
    string sourceUrl,
    List<string> headings,
    List<ActionType> actions,
    IPipeWriter writer
  )
  {
    IYamlHierarchy yaml = new YamlFormatter(writer);

    yaml
      .DeclarationLine(TABLE, yTable =>{
        yTable
          .Field(SRC_URL, yy => yy.Url(sourceUrl) )
          .DeclarationLine(HEADINGS, yy =>
            yy.List(headings, (hdg, y) => y.Value(hdg))
          );
      });

    yaml
      .DeclarationLine(ACTION_LIST);
    
    actions.ForEach( _action => {
      yaml.List().DeclarationLine(ACTION_DEF)
        .FieldAndValue(ID, _action.Name)
        .FieldAndValue(ACTION_NAME, _action.Name)
        .FieldAndValue(DESCRIPTION, _action.Description)
        .FieldAndValue(API_URL, _action.ApiLink)
        .DeclarationLine(RESOURCE_LIST);
      
      _action.GetMappedAccessLevels().ForEach( (accessLevel, idx) => {
        _action.GetResourceTypesForLevel(accessLevel).ForEach( (resource, rIdx) => {
          yaml.List().DeclarationLine(RESOURCE_DEF)
            .Field(ID).Quote(resource.ResourceTypeDefId).Line()
            .Field(RESOURCE_NAME).Value(resource.ResourceTypeName).Line()
            ;
          if (resource.ConditionKeyIds().Count() > 0)
          {
            yaml.DeclarationLine(CONDITION_KEYS);

              resource.ConditionKeyIds().ForEach( (condKey, ckIdx) =>
                yaml.List().Value(condKey).Line()
              );

            yaml.EndDecl();
          }
          if (resource.DependendActionIds().Count() > 0)
          {
            yaml.DeclarationLine( DEPENDENT_ACTIONS );
              resource.DependendActionIds().ForEach( (depAction, _) 
                => yaml.List().Value(depAction).Line());
            yaml.EndDecl();
          }
        });
      });
    });
    yaml.EndDecl();

    // writer.Indent(MBR_TABLE).KeyLine(ACTION_LIST);

    // actions.ForEach( _action => {
    //   writer
    //     .ListItem(MBR_ACTION_LIST, act => act.KeyLine(ACTION_DEF))
    //       .Indent(MBR_ACTION_DEF).Key(ID).QuoteLine(_action.ActionId)
    //       .Indent(MBR_ACTION_DEF).Key(ACTION_NAME).WriteFragmentLine(_action.Name)
    //       .Indent(MBR_ACTION_DEF).Key(DESCRIPTION).WriteFragmentLine(_action.Description)
    //       .Indent(MBR_ACTION_DEF).Key(API_URL).UrlLIne(_action.ApiLink)
    //       .Indent(MBR_ACTION_DEF).KeyLine(RESOURCE_LIST);

    //   _action.GetMappedAccessLevels().ForEach( (accessLevel, idex) => {

    //     _action.GetResourceTypesForLevel(accessLevel).ForEach( (resource, rIdx) => {
    //       writer.ListItem( MBR_RESOURCE_LIST, list => list.KeyLine(RESOURCE_DEF) );
    //       writer
    //         .Indent(MBR_RESOURCE_REC).Key(ID).QuoteLine(resource.ResourceTypeDefId)
    //         .Indent(MBR_RESOURCE_REC).Key(RESOURCE_NAME).WriteFragmentLine(resource.ResourceTypeName);

    //       if (resource.ConditionKeyIds().Count() > 0)
    //       {
    //         writer.Indent(MBR_RESOURCE_REC).KeyLine(CONDITION_KEYS);
    //         resource.ConditionKeyIds().ForEach( (condkey, ckIdx) =>
    //           writer.Indent(MBR_RESOURCE_REC).StringListLine(condkey)
    //         );
    //       }

    //       if (resource.DependendActionIds().Count() > 0)
    //       {
    //         writer.Indent(MBR_RESOURCE_REC).KeyLine( DEPENDENT_ACTIONS );
    //         resource.DependendActionIds().ForEach( (depAction, idxDa)
    //           => writer.Indent(MBR_RESOURCE_REC).StringListLine(depAction)
    //         );
    //       }
    //     });
    //   });

    // });
  }

}