/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Collections.Generic;

using Spec;

namespace Actions;

public struct ActionsTableContext : IContext
{
  private const int
    UNSET_NUM = -1,
    DEFAULT_ACTION_COUNT = 1;

  private List<ActionType> _Actions;

  private ActionType _CurrentAction;
  private int _ExpectedNumResourceTypeRows;

  private ActionResourceType _CurrentResourceType;

  private ActionAccessLevel _CurrentAccessLevel;

  public Option<string> ConditionKeyId;
  public Option<string> DependentActionId;
  private Option<bool> WipIsAllResourceTypes;

  public ActionsTableContext()
  {
    _Actions = new List<ActionType>();
    _CurrentAction = new ActionType();
    _ExpectedNumResourceTypeRows = DEFAULT_ACTION_COUNT;

    _CurrentResourceType = new ActionResourceType();
    _CurrentAccessLevel = ActionAccessLevel.Unknown;
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
    WipIsAllResourceTypes = Option.None<bool>();
  }

  public int NumResourceRowsExpected => _ExpectedNumResourceTypeRows;
  
  public void SetActionId(string id)
    => _CurrentAction.SetActionId(id);

  public void SetDocLinkAndName(string docLink, string name)
  {
    _CurrentAction.SetApiDocLink(docLink);
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

  public List<ActionType> GetDeclaredActions()
    => _Actions;

  /// <summary>
  /// Set the expected number of rows for resource type information
  /// from a td rowspan="5" attribute.
  /// </summary>
  /// <param name="rowSpan">String value from the HTML attribute to parse.</param>
  public void SetResourceTypeExpectedNumRows(string rowSpan)
  {
    try
    {
      int value = Int32.Parse(rowSpan);
      _ExpectedNumResourceTypeRows = value;
    }
    catch( ArgumentException ae)
    {
      throw new Exception("Bug: <td rowspan value provided but was NULL", ae);
    }
    catch( FormatException fe)
    {
      throw new Exception(message: $"Bug: expected number of rows for resource types but was given : {rowSpan}", fe);
    }
    catch( Exception e)
    {
      throw new Exception(message: "Bug: value overflowed or was illegal, not an integer: " + rowSpan, e);
    }
  }

  public void NextActionDefinition()
  {
    _CurrentAction = new ActionType();
    ResetForNextAccessLevel();
  }

  public void CollectActionTypeAndReset()
  {
    _Actions.Add(_CurrentAction);
  }

  public void CollectResourceTypeAndReset()
  {
    _CurrentAction.MapAccessToResourceType(_CurrentAccessLevel, _CurrentResourceType);
    _CurrentResourceType = new ActionResourceType();
    if (_ExpectedNumResourceTypeRows > 0)
      _ExpectedNumResourceTypeRows = _ExpectedNumResourceTypeRows - 1;
    ResetForNextAccessLevel();
  }

  private void ResetForNextAccessLevel()
  {
    _CurrentAccessLevel = ActionAccessLevel.Unknown;
    ConditionKeyId = Option.None<string>();
    DependentActionId = Option.None<string>();
  }

  private void ResetWipResourceType()
  {
    WipIsAllResourceTypes = Option.None<bool>();
  }
}