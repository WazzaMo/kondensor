/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;



namespace ConditionKeys;

public struct ConditionKeyEntry
{
  const string __EMPTY = "--EMPTY--";

  private string _Id;
  private string _DocLink;
  private string _Name;
  private string _Description;
  private ConditionKeyType _ValueType;

  public string Id => _Id;
  public bool IsIdSet => _Id != __EMPTY;

  public string DocLink => _DocLink;
  public bool IsDocLinkSet => _DocLink != __EMPTY;

  public string Name => _Name;
  public bool IsNameSet => _Name != __EMPTY;

  public string Description => _Description;
  public bool IsDescriptionSet => _Description != __EMPTY;

  public ConditionKeyType CkType => _ValueType;
  public bool IsCkTypeSet => _ValueType != ConditionKeyType._Empty;

  public ConditionKeyEntry()
  {
    _Id = __EMPTY;
    _DocLink = __EMPTY;
    _Name = __EMPTY;
    _Description = __EMPTY;
    _ValueType = ConditionKeyType._Empty;
  }

  public void SetId(string id) => _Id = id;

  public void SetDocLink(string link) => _DocLink = link;

  public void SetName(string name) => _Name = name;

  public void SetDescription(string description) => _Description = description;

  public void SetCkType(ConditionKeyType ckt) => _ValueType = ckt;
}