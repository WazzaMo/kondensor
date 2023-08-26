/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System.Collections.Generic;

using kondensor.Pipes;

namespace kondensor.Parser;


public static class Parsing
{
  public static ParseAction Group(ReplayWrapPipe pipe)
  {
    var action = new ParseAction(pipe);
    return action;
  }
}
