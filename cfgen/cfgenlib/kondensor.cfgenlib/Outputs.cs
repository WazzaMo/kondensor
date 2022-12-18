/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib
{

  public struct Outputs
  {
    private List<IOutput> _OutputList;

    public void AddOutput(IOutput value)
      => _OutputList.Add(value);
    
    public Outputs()
    {
      _OutputList = new List<IOutput>();
    }
  }

}