/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Collections.Generic;

public struct ActionsTableContext : IContext
{
  private List<ActionType> Actions;

  public Option<string> CurrentActionId;
  public Option<string> CurrentActionDocLink;
  public Option<string> CurrentActionName;
  public Option<string> CurrentDescription;
  public ActionAccessLevel CurrentAccessLevel;
  private List<ActionResourceType> ResourceTypes;

  public Option<Action<ActionsTableContext,string>> UpdateStringTask;
  public Option<Action<ActionsTableContext,TextWriter>> OutputTask;

  public Option<string> ConditionKeyId;
  public Option<string> DependentActionId;
  private Option<bool> WipIsAllResourceTypes;
  private Option<string> WipAssociatedDefinitionId;
  private Option<string> WipResourceTypeName;
  private Option<string> WipSpecificConditionKeyIds;
  private Option<string> WipDependentActionIds;

  public ActionsTableContext()
  {
    Actions = new List<ActionType>();
    CurrentActionId = Option.None<string>();
    CurrentActionDocLink = Option.None<string>();
    CurrentActionName = Option.None<string>();
    CurrentDescription = Option.None<string>();
    CurrentAccessLevel = ActionAccessLevel.Unknown;
    ResourceTypes = new List<ActionResourceType>();
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
    WipIsAllResourceTypes = Option.None<bool>();
    WipAssociatedDefinitionId = Option.None<string>();
    WipResourceTypeName = Option.None<string>();
    WipSpecificConditionKeyIds = Option.None<string>();
    WipDependentActionIds = Option.None<string>();
    UpdateStringTask = Option.None<Action<ActionsTableContext,string>>();
    OutputTask = Option.None<Action<ActionsTableContext,TextWriter>>();
  }

  public void Update(string text)
  {
    ActionsTableContext context = this;
    UpdateStringTask.MatchSome( task => task(context, text) );
  }

  public void SetActionId(string id)
    => CurrentActionId = Option.Some(id);

  public void SetDocLinkAndName(string docLink, string name)
  {
    CurrentActionDocLink = Option.Some(docLink);
    CurrentActionName = Option.Some(name);
  }

  public void SetDescription(string description)
    => CurrentDescription = Option.Some(description);

  public void SetCurrentAccessLevel(ActionAccessLevel level)
    => CurrentAccessLevel = level;

  private void ResetForNextAction()
  {
    CurrentActionId = Option.None<string>();
    CurrentActionDocLink = Option.None<string>();
    CurrentActionName = Option.None<string>();
    CurrentDescription = Option.None<string>();
  }

  private void ResetForNextDataRow()
  {
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
  }

  private void ResetWipResourceType()
  {
    WipIsAllResourceTypes = Option.None<bool>();
    WipAssociatedDefinitionId = Option.None<string>();
    WipResourceTypeName = Option.None<string>();
    WipSpecificConditionKeyIds = Option.None<string>();
    WipDependentActionIds = Option.None<string>();
  }
}