
using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{
  public struct Bool : IPrimitive
  {
    public bool Value;

    public void Write(StreamWriter output, string name, string indent)
    {
      YamlWriter.Write(output, $"{name}: '{Value}'", indent);
    }

    public Bool(bool value)
    {
      Value = value;
    }
  }

}