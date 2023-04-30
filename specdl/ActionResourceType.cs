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
public struct ActionResourceType
{
  private bool _IsAllResourceTypes;
  private Option<string> _ResourceTypeDefinitionId;
  private Option<string> _ResourceTypeName;
  private List<string> _SpecificConditionKeyIds;
  private List<string> _DependentActionIds;

  public string ResourceTypeDefId {
    get {
      string value = "";
      _ResourceTypeDefinitionId.MatchSome(id => value = id);
      return value;
    }
  }

  public string ResourceTypeName {
    get {
      string value = "";
      _ResourceTypeName.MatchSome(n => value = n);
      return value;
    }
  }

  public void SetTypeIdAndName(string id, string name)
  {
    _ResourceTypeDefinitionId = Option.Some(id);
    _ResourceTypeName = Option.Some(name);
  }

  public void AddConditionKeyId(string id)
    => _SpecificConditionKeyIds.Add(id);

  public void AddDependentActionId(string dep)
    => _DependentActionIds.Add(dep);

  public ActionResourceType()
  {
    _IsAllResourceTypes = false;
    _ResourceTypeDefinitionId = Option.None<string>();
    _ResourceTypeName = Option.None<string>();
    _SpecificConditionKeyIds = new List<string>();
    _DependentActionIds = new List<string>();
  }
}