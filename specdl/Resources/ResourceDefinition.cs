/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;

using Optional;

namespace Resources;

public struct ResourceDefinition
{
  private const string EMPTY_STRING = "--EMPTY--";

  private class InternalData
  {
    internal string _Id = EMPTY_STRING;
    internal string _ApiDocLink = EMPTY_STRING;
    internal string _Name = EMPTY_STRING;
    internal string _Arn = EMPTY_STRING;
    internal List<ResourceConditionKey> _ConditionKey = new List<ResourceConditionKey>();
  }

  private InternalData _Definition;

  public ResourceDefinition()
  {
    _Definition = new InternalData();
  }

  public bool IsValid()
    => Id != EMPTY_STRING
    && ApiLink != EMPTY_STRING
    && Name != EMPTY_STRING;

  public string Id {
    get => _Definition._Id;
    set => _Definition._Id = value;
  }

  public string ApiLink {
    get => _Definition._ApiDocLink;
    set => _Definition._ApiDocLink = value;
  }

  public bool HasApiLink => _Definition._ApiDocLink != EMPTY_STRING;

  public string Name {
    get => _Definition._Name;
    set => _Definition._Name = value;
  }

  public bool HasName => _Definition._Name != EMPTY_STRING;

  public string Arn {
    get => _Definition._Arn;
    set => _Definition._Arn = value;
  }

  public List<ResourceConditionKey> ConditionKey {
    get => _Definition._ConditionKey;
  }

  public void AddConditionKey(ResourceConditionKey key)
    => _Definition._ConditionKey.Add(key);
}