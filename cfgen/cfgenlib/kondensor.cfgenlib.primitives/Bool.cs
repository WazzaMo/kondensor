
using kondensor.cfgenlib.writer;
using System;

namespace kondensor.cfgenlib.primitives
{
  public struct Bool : IPrimitive
  {
    public bool Value;

    void IPrimitive.Write(StreamWriter output, string name, string indent)
    {
      YamlWriter.Write(output, $"{name}: '{Value}'", indent);
    }

    public Bool(bool value)
    {
      Value = value;
    }
  }

}