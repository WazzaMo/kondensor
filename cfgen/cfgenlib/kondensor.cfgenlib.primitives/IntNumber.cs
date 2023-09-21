/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  public struct IntNumber : IPrimitive
  {
    public int Number;

    public void Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, message: $"{name}: {Number}", indent);

    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, message: $"{prefix} {Number}", indent);

    public IntNumber(int value)
    {
      Number = value;
    }
  }

}