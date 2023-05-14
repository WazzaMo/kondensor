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
    private int _CountMatched;
    private int _RollbackPoint;

    internal ParseAction(ReplayWrapPipe pipe)
    {
      _Pipe = pipe;
      _CountMatched = 0;
      _RollbackPoint = pipe.GetCheckPoint();
      _MatchHistory = new LinkedList<Matching>();
    }

    public ParseAction Expect(Matcher nextRule)
    {
      bool hasToken = _Pipe.ReadToken(out string token);
      if (hasToken)
      {
        var matching = nextRule.Invoke(token);
        if (matching.IsMatch)
        {
          matching.MatcherName = nextRule.Method.Name.Some();
          _CountMatched++;
        }
        _MatchHistory.AddLast(matching);
      }
      return this;
    }

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
        _MatchHistory = new LinkedList<Matching>();
    }
  }

}