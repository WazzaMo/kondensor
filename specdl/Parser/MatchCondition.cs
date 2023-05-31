/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser;

public struct MatchCondition
{
  private Func<Matching, bool> _SearchMatchResults;
  private Option<Func<Matching, bool>> _ConditionOnMostRecent;

  public MatchCondition(Func<Matching,bool> searchMethod)
  {
    _SearchMatchResults = searchMethod;
    _ConditionOnMostRecent = Option.None<Func<Matching,bool>>();
  }

  public MatchCondition(Func<Matching,bool> searchMethod, Func<Matching,bool> conditionOnRecent)
  {
    _SearchMatchResults = searchMethod;
    _ConditionOnMostRecent = Option.Some(conditionOnRecent);
  }

  internal bool IsMostRecentFoundAndMatched(LinkedList<Matching> matchHistory, out Matching lastMatching)
  {
    Func<Matching, bool> search = _SearchMatchResults;
    bool isMatch = false;
    Matching lastMatch;
    var matches =
      from node in matchHistory
      where search(node)
      select node;

    if (matches.Count() > 0)
    {
      lastMatch  = matches.Last();
      isMatch = _ConditionOnMostRecent.Exists(condition => condition(lastMatch));
      lastMatching = lastMatch;
    }
    else
      lastMatching = Utils.NoMatch();
    return isMatch;
  }
}