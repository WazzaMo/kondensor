

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