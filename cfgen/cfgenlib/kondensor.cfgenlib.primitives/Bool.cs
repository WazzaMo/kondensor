/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{
  public struct Bool : IPrimitive
  {
    public bool Value;

    void IPrimitive.Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: {Value}", indent);

    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix} {Value}", indent);

    public Bool(bool value)
      => Value = value;
  }

}