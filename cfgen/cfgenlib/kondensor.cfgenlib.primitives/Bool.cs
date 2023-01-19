/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{
  public struct Bool : IPrimitive
  {
    public bool Value;

    void IPrimitive.Write(StreamWriter output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: '{Value}'", indent);

    public void WritePrefixed(StreamWriter output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix} '{Value}'", indent);

    public Bool(bool value)
      => Value = value;
  }

}