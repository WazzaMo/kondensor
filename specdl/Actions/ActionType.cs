/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Parser;

namespace Actions;


/// <summary>
/// An action type can be incrementally built within the context
/// and then added to the context's list of actions.
/// </summary>
public struct ActionType 
{
  private const string UNSET_STRING = "----UNSET";

  private string _ActionId;
  private string _AwsApiDocumentLink;
  private string _Name;
  private Dictionary<ActionAccessLevel,List<ActionResourceType>> _AccessLevelToResourceTypeMappings;

  public string ActionId => _ActionId;
  public string Name => _Name;
  public string ApiLink => _AwsApiDocumentLink;

  public ActionType()
  {
    _ActionId = UNSET_STRING;
    _AwsApiDocumentLink = UNSET_STRING;
    _Name = UNSET_STRING;
    _AccessLevelToResourceTypeMappings = new Dictionary<ActionAccessLevel, List<ActionResourceType>>();
  }

  public void SetActionId(string id)
    => _ActionId = id;
  
  public bool IsActionIdSet => _ActionId != UNSET_STRING;

  public void SetApiDocLink(string url)
    => _AwsApiDocumentLink = url;
  
  public void SetActionName(string name)
    => _Name = name;
    
  public bool IsApiDocLinkSet => _AwsApiDocumentLink != UNSET_STRING;

  public void MapAccessToResourceType(
    ActionAccessLevel access,
    ActionResourceType resourceType
  )
  {
    var list = _AccessLevelToResourceTypeMappings.ContainsKey(access)
      ? _AccessLevelToResourceTypeMappings[access]
      : new List<ActionResourceType>();
    list.Add(resourceType);

    _AccessLevelToResourceTypeMappings[access] = list;
  }

  public bool TryMapUpdateAccessAndResourceType(
    ActionAccessLevel access,
    ActionResourceType resourceType
  )
  {
    var list = _AccessLevelToResourceTypeMappings.ContainsKey(access)
      ? _AccessLevelToResourceTypeMappings[access]
      : new List<ActionResourceType>();

    

    int index = list.FindIndex( x => x.Description.Equals(resourceType.Description));
    if (index > -1)
    {
      return list.TryReplace(index, resourceType);
    }
    return false;
  }

  public IEnumerable<ActionAccessLevel> GetMappedAccessLevels()
  {
    return _AccessLevelToResourceTypeMappings.Keys.AsEnumerable();
  }

  public IEnumerable<ActionResourceType> GetResourceTypesForLevel(ActionAccessLevel level)
  {
    IEnumerable<ActionResourceType> result;

    if (_AccessLevelToResourceTypeMappings.ContainsKey(level))
    {
      result = _AccessLevelToResourceTypeMappings[level].AsEnumerable();
    }
    else
    {
      result = new List<ActionResourceType>().AsEnumerable();
    }
    return result;
  }

  public bool IsResourceMapListAvailableForLevel(ActionAccessLevel level)
    => _AccessLevelToResourceTypeMappings.ContainsKey(level)
      ? _AccessLevelToResourceTypeMappings[level].Count > 0
      : false;

  public ActionResourceType GetResourceFor(ActionAccessLevel level)
  {
    if (_AccessLevelToResourceTypeMappings.ContainsKey(level))
      return _AccessLevelToResourceTypeMappings[level].Last();
    else
      throw new InvalidOperationException( message: $"Expected access level {level} to be mapped already." );
  }
}