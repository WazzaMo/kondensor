/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Linq;


using Parser;
using Actions;
using YamlWriters;
using System.Security.Cryptography;

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
    YamlFormatter yamlFormatter
  )
  {
    IYamlHierarchy yaml = (IYamlHierarchy) yamlFormatter;

    yaml
      .DeclarationLine(TABLE, yTable =>{
        yTable
          .Field(SRC_URL, yy => yy.Url(sourceUrl) )
          .DeclarationLine(HEADINGS, yy =>
            yy.List(headings, (hdg, y) => y.Value(hdg))
          )
        .DeclarationLine(ACTION_LIST, yActs => {

          yActs.List(actions, (act, yVal) => yVal.ObjectListItem(ACTION_DEF,()=> {
            yActs
              .FieldAndValue(ID, act.ActionId)
              .FieldAndValue(ACTION_NAME, act.Name)
              .FieldAndValue(API_URL, act.ApiLink)
              .DeclarationLine(RESOURCE_LIST, yRes => 
                WriteActionDeclarationLine(yRes, act)
              );
            CommentSkippedResourceProps(yaml, act);

            }) // List
          );
        });
      }
    );

  }

  private static void WriteActionDeclarationLine(
    IYamlHierarchy yRes,
    ActionType act
  )
  {
    var accessLevels = act.GetMappedAccessLevels();

    yRes.List(
      accessLevels,
      (accessLevel,yVal) =>
        yVal.ObjectListItem(accessLevel.ToString(),() =>{
          yRes.List(
            GetValidResourceTypesByAccessLevel(act, accessLevel),
            (resource, _)=> {

              yVal.ObjectListItem(
                RESOURCE_DEF,
                () => WriteResourceTypeProperties(yRes, resource)
              );

            });
          }
        )
    );
  }

  private static void CommentSkippedResourceProps(
    IYamlHierarchy yaml,
    ActionType actionType
  )
  {
    actionType.GetMappedAccessLevels().ForEach( (level, idx) => {
      var resources = FilterScenarios(actionType.GetResourceTypesForLevel(level));

      resources.ForEach( (resource, _) =>{
        yaml.Comment(message: $"Action resources skipped for: {level}");
        WriteResourceTypeComment(yaml, resource);
      }); // resources
    });
  }

  private static IEnumerable<ActionResourceType> GetValidResourceTypesByAccessLevel(
    ActionType act,
    ActionAccessLevel access
  )
    => FilterResourceDetailsForNonScenarioProperties(act.GetResourceTypesForLevel(access));

  private static IEnumerable<ActionResourceType> FilterResourceDetailsForNonScenarioProperties(
    IEnumerable<ActionResourceType> resourceTypes
  )
  {
    var query = from resource in resourceTypes
      where ! resource.IsScenarioAndShouldBeCommented
      select resource;
    return query;
  }

  private static IEnumerable<ActionResourceType> FilterScenarios(IEnumerable<ActionResourceType> resourceTypes)
  {
    var query = from resource in resourceTypes
      where resource.IsScenarioAndShouldBeCommented select resource;
    return query;
  }

  private static void WriteResourceTypeProperties(
    IYamlHierarchy yRes,
    ActionResourceType resource
  )
  {
    yRes
      .FieldAndValue(DESCRIPTION, resource.Description);
    if (resource.IsIdAndNameSet)
    {
      yRes
        .Field(ID, yy => yy.Quote(resource.ResourceTypeDefId))
        .FieldAndValue(RESOURCE_NAME, resource.ResourceTypeName);
    }
    if (resource.ConditionKeyIds().Count() > 0)
    {
      yRes.DeclarationLine(
        CONDITION_KEYS,
        yCK => yCK.List(resource.ConditionKeyIds(), (ck,yV) => yV.Quote(ck))
      );
    }
    if (resource.DependendActionIds().Count() > 0)
    {
      yRes.DeclarationLine(
        DEPENDENT_ACTIONS,
        yDep => yDep.List(resource.DependendActionIds(), (daId,yV)=>yV.Value(daId))
      );
    }
  }

  private static void WriteResourceTypeComment(
    IYamlHierarchy yRes,
    ActionResourceType resource
  )
  {
    string[] parts = resource.Description.Split(SCENARIO_PARTITION);
    string neededDescription = parts.Last().Trim();
    string descriptionComment = $"  -> {neededDescription}";
    yRes.Comment( descriptionComment );
  }

  const char SCENARIO_PARTITION = '\n';
}