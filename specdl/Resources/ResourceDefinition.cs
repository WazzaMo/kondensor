/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;

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
    internal Option<ResourceConditionKey> _ConditionKey = Option.None<ResourceConditionKey>();
  }

  private InternalData _Definition;

  public ResourceDefinition()
  {
    _Definition = new InternalData();
  }
}