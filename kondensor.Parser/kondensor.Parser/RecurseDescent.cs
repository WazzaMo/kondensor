/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;

using kondensor.Pipes;
using Optional;

namespace kondensor.Parser;

public struct RecurseDescent
{
  private Matcher _Rule;
  private Option<Matching> _Result;
  private IPipe _Pipe;

  public RecurseDescent(Matcher matcher, IPipe pipe)
  {
    _Rule = matcher;
    _Result = Option.None<Matching>();
    _Pipe = pipe;
  }

  public static RecurseDescent Root(Matcher matcher, IPipe pipe)
    =>  new RecurseDescent(matcher, pipe);
  
  public RecurseDescent Upon(Matcher rule, Action<Matching, RecurseDescent> task)
  {
    var rd = new RecurseDescent(rule, _Pipe);
    bool hasToken = _Pipe.ReadToken(out string token);
    if (hasToken)
    {
      Matching result;
      result = rule.Invoke(token);
      task(result, new RecurseDescent(rule, _Pipe));
    }
    
    return this;
  }
  
  public IPipeWriter Writer => (IPipeWriter) _Pipe;

  internal void WriteFragment(string fragment) => _Pipe.WriteFragment(fragment);
}

