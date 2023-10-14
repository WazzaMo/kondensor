/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Optional;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using kondensor.Pipes;

namespace kondensor.Parser;

/// <summary>
/// This is the heart of the Parser library where
/// tokens and productions are matched, including
/// conditional matchings and annotating matched tokens.
/// </summary>
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
  ///   Number of mismatches during parsing, -ve means match history was lost;
  ///   +ve means history includes that many mismatches; 0 means all matched.
  /// </summary>
  public int NumberOfMismatches
    => _MatchHistory.Count - _CountMatched;

  /// <summary>
  /// Indicates if all tokens in the history were matched by rules.
  /// </summary>
  public bool IsAllMatched => 
    _CountMatched == _MatchHistory.Count;

  public IPipeWriter Writer => (IPipeWriter) _Pipe;

  /// <summary>
  /// Scan ahead looking for a match and making sure the
  /// next symbol to parse will be that symbol.
  /// Move forward throught the pipe scanning for the given
  /// matcher, leaving the parse action at the state where it
  /// will read the symbol that first matched the given Matcher.
  /// </summary>
  /// <param name="rule">Matcher to scan for</param>
  /// <returns>ParseAction at the point to read the expected token.</returns>
  public ParseAction SkipUntil(Matcher rule)
  {
    // int originalCheckPoint = _Pipe.GetCheckPoint();
    // int testCheckPoint = 0;
    // bool canRead = true;
    // string token;
    // Matching status = Utils.NoMatch();
    
    // while( canRead && !status.IsMatch)
    // {
    //   testCheckPoint = _Pipe.GetCheckPoint();
    //   canRead = _Pipe.ReadToken(out token);
    //   if (canRead)
    //   {
    //     status = rule.Invoke(token);
    //   }
    // }
    // if (status.IsMatch)
    // {
    //   if (testCheckPoint < originalCheckPoint)
    //     throw new Exception(message:$"\n## Checkpoint {testCheckPoint} before start point of {originalCheckPoint}");
    //   _Pipe.ReturnToCheckPoint(testCheckPoint);
    // }
    ScanRule scanner = (string token) => {
      var match = rule(token);
      return new ScanResult() {
        IsMatched = match.IsMatch,
        Index = match.MatchIndex
      };
    };
    var result = _Pipe.ScanAhead(scanner);
    return this;
  }

  /// <summary>Scans for and consumes a token that matches the given rule.</summary>
  /// <param name="matchRule">Rule for token scanning and then matching.</param>
  /// <param name="annotation">Annotation to apply to match.</param>
  /// <returns>ParseAction ready to parse token after matched token.</returns>
  public ParseAction ScanForAndExpect(
    Matcher matchRule, string? annotation = null
  )
    => SkipUntil(matchRule)
      .Expect(matchRule, annotation);

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
    else
    {
      Matching endInputMatching = new Matching()
      {
        Annotation = annotation != null ? annotation : "End of pipe",
        MatchResult = MatchKind.Mismatch,
        MismatchToken = "END-OF-FILE",
        Parts = Option.None<LinkedList<string>>()
      };
      _MatchHistory.AddLast(endInputMatching);
    }
    return this;
  }

  /// <summary>
  /// Check the next token to see if it matches the maybeRule and parse it if it does.
  /// </summary>
  /// <param name="maybeRule">Rule to check parsing with.</param>
  /// <param name="annotation">Mandatory annotation to apply if matched</param>
  /// <returns>same parser object.</returns>
  public ParseAction MayExpect(Matcher maybeRule, string annotation)
  {
    int checkpoint = _Pipe.GetCheckPoint();
    bool hasToken = _Pipe.ReadToken(out string token);

    if (hasToken)
    {
      Matching result = maybeRule.Invoke(token);
      if (result.IsMatch)
      {
        if (! result.HasName)
        {
          result.MatcherName = maybeRule.Method.Name;
        }
        result.Annotation = annotation;
        _CountMatched++;
        _MatchHistory.AddLast(result);
      }
      else
      {
        _Pipe.ReturnToCheckPoint(checkpoint);
      }
    }
    return this;
  }

  public ImmutableList<Matching> QueryHistory() => _MatchHistory.ToImmutableList();

  /// <summary>
  /// Attempts to match the production iff the condition is matched by > 0
  /// matches in the match history.
  /// </summary>
  /// <param name="condition">Condition to test for in match history.</param>
  /// <param name="production">Conditional production.</param>
  /// <returns>Same parseAction</returns>
  public ParseAction IfThenProduction(
    ParseCondition condition,
    Production production
  )
  {
    IEnumerable<Matching> matches = _MatchHistory.Where( node => condition(node));
    ParseAction resultParser = (matches.Count() > 0)
      ? Expect(production)
      : this;
    return resultParser;
  }

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
  /// IfElse that uses MatchCondition to search for the right Matching and then check for values in its fields.
  /// </summary>
  /// <param name="condition">Predicate conditions to apply to search and match fields (optional)</param>
  /// <param name="ifThen">Parsing branch for match with the matched Matching given.</param>
  /// <param name="elseThen">Parsing branch for a non-match (miss).</param>
  /// <returns></returns>
  public ParseAction IfElse(
    MatchCondition condition,
    Func<ParseAction, Matching, ParseAction> ifThen,
    Func<ParseAction, ParseAction> elseThen
  )
  {
    Matching lastMatch;
    ParseAction parser = condition.IsMostRecentFoundAndMatched(_MatchHistory, out lastMatch)
      ? ifThen(this,lastMatch)
      : elseThen(this)
      ;

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

  public ParseAction ProductionWhileMatch(Production production)
  {
    var savedHistory = SaveMatchHistory();
    int checkpoint = _Pipe.GetCheckPoint();

    while( IsAllMatched && IsProductionMatched(production))
    {
      savedHistory = SaveMatchHistory();
      checkpoint = _Pipe.GetCheckPoint();
    }
    _Pipe.ReturnToCheckPoint(checkpoint);
    RestoreMatchHistory(savedHistory);
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
  /// Attempts to parse by applying the first production and, should that fail,
  /// trying the second one, until either one has parsed or both failed.
  /// </summary>
  /// <param name="first">Primary production to attempt</param>
  /// <param name="second">Second production, if first fails.</param>
  /// <returns>ParseAction for fluid API.</returns>
  public ParseAction EitherProduction(
    Production first,
    Production second
  )
  {
    int checkpoint = _Pipe.GetCheckPoint();
    var savedHistory = SaveMatchHistory();
    if (! IsProductionMatched(first))
    {
      _Pipe.ReturnToCheckPoint(checkpoint);
      RestoreMatchHistory(savedHistory);
      Expect(second);
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

  private int SaveMatchHistory()
    => _MatchHistory.Count;
  
  private void RestoreMatchHistory(int savedHistory)
  {
    while(_MatchHistory.Count > savedHistory && savedHistory >= 0)
    {
      _MatchHistory.RemoveLast();
    }
    _CountMatched = _MatchHistory.Count;
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

  private bool IsProductionMatched(Production production)
  {
    CheckProduction(production, out bool productionMatched);
    return productionMatched;
  }
}
