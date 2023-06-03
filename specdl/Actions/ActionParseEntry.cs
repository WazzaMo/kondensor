/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;

namespace Actions;


public record struct ActionParseEntry (
  Option<string> Id,
  Option<string> SourceDocLink,
  Option<string> Name,
  Option<string> Description,
  Option<string> AccessLevel,
  Option<string> ResourceType,
  Option<string> ConditionKey
);