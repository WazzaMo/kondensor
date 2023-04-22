/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

public record struct ConditionKeyType (
  string ConditionKeyId,
  string Description,
  Type ValueType
);