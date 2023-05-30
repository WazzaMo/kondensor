/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

  /// <summary>
  /// Indicates if all tokens in the history were matched by rules.
  /// </summary>
  public bool IsAllMatched => 
    _CountMatched == _MatchHistory.Count;

  public ParseAction SkipUntil(Matcher rule)
  {
    int originalCheckPoint = _Pipe.GetCheckPoint();
    int testCheckPoint = 0;
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
      if (testCheckPoint < originalCheckPoint)
        throw new Exception(message:$"\n## Checkpoint {testCheckPoint} before start point of {originalCheckPoint}");
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
      else
      {
        matching.MismatchToken = token;
      }

      // Capture match history with modifications to the matching
      _MatchHistory.AddLast(matching);
    }
    return this;
  }

  public ImmutableList<Matching> QueryHistory() => _MatchHistory.ToImmutableList();

  /// <summary>
  /// Conditional parsering If() where condition can describe a pattern of Matching possibilities.
  /// </summary>
  /// <param name="condition">boolean test on Matching fields, typically based on annotations.</param>
  /// <param name="ifThen">Detailed analysis and extra parsing.</param>
  /// <returns>Same parser for fluid api</returns>
  public ParseAction If(
    ParseCondition condition,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> ifThen
  )
  {
    IEnumerable<Matching> results = _MatchHistory.Where(node => condition(node));
    ParseAction parser = (results.Count() > 0)
      ? ifThen(this, results)
      : this;
    return parser;
  }

  /// <summary>
  /// Conditional parsing based on Matching condition and number of matches.
  /// </summary>
  /// <param name="condition">Matching fields boolean expression, mostly for annotations</param>
  /// <param name="countMatchCondition">Count based condition, such as x => x > 3</param>
  /// <param name="ifThen">match results and parser for any further analysis and conditional parsing.</param>
  /// <returns>Same parser for fluid api</returns>
  public ParseAction If(
    ParseCondition condition,
    Func<int, bool> countMatchCondition,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> ifThen
  )
  {
    IEnumerable<Matching> results = _MatchHistory.Where(node => condition(node));
    ParseAction parser = countMatchCondition( results.Count() )
      ? ifThen(this, results)
      : this;
    return parser;
  }

  /// <summary>
  /// Conditional parsing in the match and non-match branches where each branch may need to analyse match results first.
  /// </summary>
  /// <param name="condition">Matching field boolean match, usually for annotations.</param>
  /// <param name="ifThen">Analysis and branched parsing where condition matched</param>
  /// <param name="elseThen">Analysis for no matching records and branched parsing where condition failed to match.</param>
  /// <returns>Same parser for fluid api</returns>
  public ParseAction IfElse(
    ParseCondition condition,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> ifThen,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> elseThen
  )
  {
    IEnumerable<Matching> results = _MatchHistory.Where(node => condition(node));
    ParseAction parser = (results.Count() > 0)
      ? ifThen(this, results)
      : elseThen(this, results);
    return parser;
  }

  /// <summary>
  /// Conditional parsing with count of matches condition for match and non-match branches.
  /// </summary>
  /// <param name="condition">boolean Matching field condition</param>
  /// <param name="countMatchCondition">count of matches needed, such as x => x > 2</param>
  /// <param name="ifThen">Parsing branch for field and count match</param>
  /// <param name="elseThen">Parsing branch where either fields or count did not match.</param>
  /// <returns>Same parser for fluid api</returns>
  public ParseAction IfElse(
    ParseCondition condition,
    Func<int, bool> countMatchCondition,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> ifThen,
    Func<ParseAction, IEnumerable<Matching>, ParseAction> elseThen
  )
  {
    IEnumerable<Matching> results = _MatchHistory.Where(node => condition(node));
    ParseAction parser = countMatchCondition(results.Count())
      ? ifThen(this, results)
      : elseThen(this, results);
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
