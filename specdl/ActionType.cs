/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

public record struct ActionType (
  string ActionId,
  string AwsApiDocumentLink,
  string ActionName,
  string Description,
  Dictionary<ActionAccessLevel,List<ActionResourceType>> AccessLevelToResourceTypeMappings
);