/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

namespace ConditionKeys;

public enum ConditionKeyType
{
  _Empty,
  _Unknown,
  ArrayOfString,
  String,
  Integer,
  Float,
  Null,
  Date,
  IpAddress,
  List,
  Object
}