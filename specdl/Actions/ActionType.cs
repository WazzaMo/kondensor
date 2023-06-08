/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

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
  private string _Description;
  private Dictionary<ActionAccessLevel,List<ActionResourceType>> _AccessLevelToResourceTypeMappings;

  public string ActionId => _ActionId;
  public string Name => _Name;
  public string ApiLink => _AwsApiDocumentLink;
  public string Description => _Description;

  public ActionType()
  {
    _ActionId = UNSET_STRING;
    _AwsApiDocumentLink = UNSET_STRING;
    _Name = UNSET_STRING;
    _Description = UNSET_STRING;
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

  public void SetDescription(string desc)
    => _Description = desc;
  
  public bool IsDescriptionSet => _Description != UNSET_STRING;

  public void MapAccessToResourceType(ActionAccessLevel access, ActionResourceType resourceType)
  {
    var list = _AccessLevelToResourceTypeMappings.ContainsKey(access)
      ? _AccessLevelToResourceTypeMappings[access]
      : new List<ActionResourceType>();
    list.Add(resourceType);

    _AccessLevelToResourceTypeMappings[access] = list;
  }
}