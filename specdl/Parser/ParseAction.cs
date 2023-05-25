/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System;
using System.Collections.Generic;

namespace Parser;

public struct ParseAction
{
  private ReplayWrapPipe _Pipe;
  private LinkedList<Matching> _MatchHistory;
  private int[] __CountMatched;
  private int[] __RollbackPoint;

  private int _CountMatched {
    get => __CountMatched[0];
    set => __CountMatched[0] = value;
  }
  private int _RollbackPoint {
    get => __RollbackPoint[0];
    set => __RollbackPoint[0] = value;
  }

  internal ParseAction(ReplayWrapPipe pipe)
  {
    _Pipe = pipe;
    __CountMatched = new int[1]{0};
    __RollbackPoint = new int[1]{pipe.GetCheckPoint()};
    _MatchHistory = new LinkedList<Matching>();
  }

  public ParseAction SkipUntil(Matcher rule)
  {
    int testCheckPoint = _Pipe.GetCheckPoint();
    bool canRead = true;
    string token;
    Matching status = Utils.NoMatch();
    
    while( canRead && !status.IsMatch)
    {
      testCheckPoint = _Pipe.GetCheckPoint();
      canRead = _Pipe.ReadToken(out token);
      if (canRead)
      {
        status = rule.Invoke(token);
      }
    }
    if (status.IsMatch)
    {
      _Pipe.ReturnToCheckPoint(testCheckPoint);
    }
    return this;
  }

  public ParseAction Expect(Matcher nextRule, string? annotation = null)
  {
    bool hasToken = _Pipe.ReadToken(out string token);
    if (hasToken)
    {
      var matching = nextRule.Invoke(token);
      
      if (matching.IsMatch)
      {
        if (! matching.HasName)
        {
          matching.MatcherName = nextRule.Method.Name;
        }
        if (annotation != null)
        {
          matching.Annotation = annotation;
        }
        _CountMatched++;
      }
      // Capture match history with modifications to the matching
      _MatchHistory.AddLast(matching);
    }
    return this;
  }

  // If /then
  public ParseAction If(Func<LinkedList<Matching>, bool> condition, Func<ParseAction, ParseAction> then)
  {
    ParseAction parser = condition(_MatchHistory)
      ? then(this)
      : this;
    return parser;
  }

  public ParseAction ExpectProductionUntil(Production production, Matcher endRule, string? endAnnodation = null)
  {
    bool isAllMatched = _CountMatched == _MatchHistory.Count;
    bool isProdMatch, isEndMatch;
    do {
      CheckEndRuleAndProduction(production, endRule, endAnnodation, out isAllMatched, out isProdMatch, out isEndMatch);
    } while (isAllMatched && isProdMatch && ! isEndMatch);
    return this;
  }

  public ParseAction Expect(Production production)
    => production(this);

  /// <summary>
  /// All expects were matched and need processing, advancing the rollback point.
  /// </summary>
  /// <param name="handler">Handler to be called if all expects matched</param>
  /// <returns>Same fluid object.</returns>
  public ParseAction AllMatchThen(Action<LinkedList<Matching>, IPipeWriter> handler )
  {
    if (_CountMatched == _MatchHistory.Count)
    {
      var writer = (IPipeWriter) _Pipe;
      handler(_MatchHistory, writer);
      _RollbackPoint = _Pipe.GetCheckPoint();
      ResetMatchHistory();
    }
    return this;
  }

  /// <summary>
  /// Expect the same production to parse and match N times over.
  /// </summary>
  /// <param name="numExpected">Number of matches expected</param>
  /// <param name="production">Production that should match</param>
  /// <returns>parser for fluid api</returns>
  public ParseAction ExpectProductionNTimes(int numExpected, Production production)
  {
    int runCount;
    bool isProductionMatch = true;

    for(runCount = 0; isProductionMatch && runCount < numExpected; runCount++)
    {
      CheckProduction(production, out isProductionMatch);
    }
    return this;
  }

  /// <summary>
  /// Rollback to try a different set of expects and optionally report errors.
  /// </summary>
  /// <param name="errHandler">Optional error handler</param>
  /// <returns>Same fluid object</returns>
  public ParseAction MismatchesThen(Action<LinkedList<Matching>, IPipeWriter>? errHandler = null)
  {
    if (_CountMatched < _MatchHistory.Count)
    {
      _Pipe.ReturnToCheckPoint(_RollbackPoint);
      if (errHandler != null)
      {
        errHandler.Invoke(_MatchHistory, (IPipeWriter) _Pipe);
      }
      ResetMatchHistory();
    }
    return this;
  }

  private void ResetMatchHistory()
  {
    _MatchHistory.Clear();
    _CountMatched = 0;
  }

  /// <summary>
  /// Supports ExpectProductionUntil
  /// </summary>
  /// <param name="production">production to process</param>
  /// <param name="endRule">rule that terminates production loop</param>
  /// <param name="annotation">annotation to apply to endRule result</param>
  /// <param name="isAllMatched">flag that all matches succeeded</param>
  /// <param name="isProdMatch">flag production match</param>
  /// <param name="isEndMatch">flag end rule match</param>
  private void CheckEndRuleAndProduction(
    Production production,
    Matcher endRule,
    string? annotation,
    out bool isAllMatched,
    out bool isProdMatch,
    out bool isEndMatch
  )
  {
    int checkpoint = _Pipe.GetCheckPoint();
    bool hasToken = _Pipe.ReadToken(out string token);
    if (hasToken)
    {
      var matching = endRule.Invoke(token);
      if (matching.IsMatch)
      {
        if (annotation != null)
        {
          matching.Annotation = annotation;
        }
        isEndMatch = true;
        isProdMatch = false;
        _CountMatched++;
        _MatchHistory.AddLast(matching);
      }
      else
      {
        _Pipe.ReturnToCheckPoint(checkpoint);
        Expect(production);
        isProdMatch = _CountMatched == _MatchHistory.Count;
        isEndMatch = false;
      }
    }
    else
    {
      isProdMatch = false;
      isEndMatch = false;
    }
    isAllMatched = _CountMatched == _MatchHistory.Count;
  }

  private void CheckProduction(
    Production production,
    out bool isProdMatch
  )
  {
    Expect(production);
    isProdMatch = _CountMatched == _MatchHistory.Count;
  }

}
