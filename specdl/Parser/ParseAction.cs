/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System;
using System.Collections.Generic;

namespace Parser
{

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

    public ParseAction Expect(Production production)
      => production(this);

    /// <summary>
    /// All expects were matched and need processing, advancing the rollback point.
    /// </summary>
    /// <param name="handler">Handler to be called if all expects matched</param>
    /// <returns>Same fluid object.</returns>
    public ParseAction Then(Action<LinkedList<Matching>, IPipeWriter> handler )
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
    /// Rollback to try a different set of expects and optionally report errors.
    /// </summary>
    /// <param name="errHandler">Optional error handler</param>
    /// <returns>Same fluid object</returns>
    public ParseAction Else(Action<LinkedList<Matching>, IPipeWriter>? errHandler = null)
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
  }

}