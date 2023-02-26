/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using kondensor.cfgenlib.policy;

namespace kondensor.cfgenlib.primitives
{

  public struct PolicyDocValue : IPrimitive
  {
    private PolicyDocument _Policy;
    
    public void Write(StreamWriter output, string name, string indent)
    {
      throw new NotImplementedException();
    }

    public void WritePrefixed(StreamWriter output, string prefix, string indent)
    {
      throw new NotImplementedException();
    }
  }

}