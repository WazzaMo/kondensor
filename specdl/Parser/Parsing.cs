/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.Collections.Generic;

namespace Parser
{

  public static class Parsing
  {
    public static ParseAction Group(ReplayWrapPipe pipe)
    {
      var action = new ParseAction(pipe);
      return action;
    }
  }

}