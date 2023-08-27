/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;

namespace Resources;

public struct ResourceConditionKey
{
  public const string EMPTY_STRING = "--EMPTY--";

  public string Id;
  public string Template;

  public ResourceConditionKey()
  {
    Id = EMPTY_STRING;
    Template = EMPTY_STRING;
  }
}