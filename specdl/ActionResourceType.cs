/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

using Optional;

/// <summary>
/// For an Actions, Resources, Condition Keys table, this identifies
/// the resource type in the hierarchy and any list of specific condition keys
/// supported by that action-resoure-type pairing.
/// 
/// Is all resoure types - action applies to all available resource types, when false Id and name should be given.
/// associated def Id - used to look up the reource type definition.
/// ResourceTypeName could also look up resource type definition (independent check on id)
/// Specific conditionkey ids - list of condition keys 
/// Dependent actions - list of actions that are needed to enable this action, say on the same policy.
/// </summary>
public record struct ActionResourceType(
  bool IsAllResourceTypes,
  Option<string> AssociatedDefinitionId,
  Option<string> ResourceTypeName,
  List<string> SpecificConditionKeyIds,
  List<string> DependentActionIds
);