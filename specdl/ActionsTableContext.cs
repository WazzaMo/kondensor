/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Collections.Generic;

public struct ActionsTableContext : IContext
{
  private const int
    UNSET_NUM = -1,
    DEFAULT_ACTION_COUNT = 1;

  private List<ActionType> Actions;

  private ActionType _CurrentAction;
  private int _ActionRows;
  private int _ResourceTypeRows;

  private ActionResourceType _CurrentResourceType;

  private ActionAccessLevel _CurrentAccessLevel;
  private List<ActionResourceType> ResourceTypes;

  public Option<Action<ActionsTableContext,TextWriter>> OutputTask;

  public Option<string> ConditionKeyId;
  public Option<string> DependentActionId;
  private Option<bool> WipIsAllResourceTypes;
  // // private Option<string> WipResourceTypeDefinitionId;
  // // private Option<string> WipResourceTypeName;
  // private Option<string> WipSpecificConditionKeyIds;
  // private Option<string> WipDependentActionIds;

  public ActionsTableContext()
  {
    Actions = new List<ActionType>();
    _CurrentAction = new ActionType();
    _ActionRows = DEFAULT_ACTION_COUNT;
    _ResourceTypeRows = UNSET_NUM;

    _CurrentResourceType = new ActionResourceType();
    _CurrentAccessLevel = ActionAccessLevel.Unknown;
    ResourceTypes = new List<ActionResourceType>();
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
    WipIsAllResourceTypes = Option.None<bool>();
    OutputTask = Option.None<Action<ActionsTableContext,TextWriter>>();
  }

  public void SetActionId(string id)
    => _CurrentAction.SetActionId(id);

  public void SetDocLinkAndName(string docLink, string name)
  {
    _CurrentAction.SetActionId(docLink);
    _CurrentAction.SetActionName(name);
  }

  public void SetResourceRefAndName(string idRef, string name)
    => _CurrentResourceType.SetTypeIdAndName(idRef, name);

  public void SetDescription(string description)
    => _CurrentAction.SetDescription(description);

  public bool HasDescription() => _CurrentAction.IsDescriptionSet;

  public void SetCurrentAccessLevel(ActionAccessLevel level)
    => _CurrentAccessLevel = level;
  
  public ActionAccessLevel CurrentAccessLevel => _CurrentAccessLevel;

  public void CollectActionTypeAndReset()
  {
    Actions.Add(_CurrentAction);
    _CurrentAction = new ActionType();
  }

  public void CollectResourceTypeAndReset()
  {
    _CurrentAction.MapAccessToResourceType(_CurrentAccessLevel, _CurrentResourceType);
    _CurrentResourceType = new ActionResourceType();
    ResetForNextAccessLevel();
  }

  private void ResetForNextAccessLevel()
  {
    ResourceTypes = new List<ActionResourceType>();
    _CurrentAccessLevel = ActionAccessLevel.Unknown;
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
  }

  private void ResetWipResourceType()
  {
    WipIsAllResourceTypes = Option.None<bool>();
  }
}