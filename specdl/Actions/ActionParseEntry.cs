/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
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