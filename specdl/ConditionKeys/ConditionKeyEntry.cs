/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;



namespace ConditionKeys;

public struct ConditionKeyEntry
{
  const string __EMPTY = "--EMPTY--";

  private string _Id;
  private string _DocLink;
  private ConditionKeyType _ValueType;

  public string Id => _Id;
  public bool IsIdSet => _Id != __EMPTY;

  public string DocLink => _DocLink;
  public bool IsDocLinkSet => _DocLink != __EMPTY;

  public ConditionKeyType CkType => _ValueType;
  public bool IsCkTypeSet => _ValueType != ConditionKeyType._Empty;

  public ConditionKeyEntry()
  {
    _Id = __EMPTY;
    _DocLink = __EMPTY;
    _ValueType = ConditionKeyType._Empty;
  }

  public void SetId(string id) => _Id = id;

  public void SetDocLink(string link) => _DocLink = link;

  public void SetCkType(ConditionKeyType ckt) => _ValueType = ckt;
}