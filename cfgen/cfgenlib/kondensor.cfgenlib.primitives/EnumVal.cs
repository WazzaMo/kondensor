/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System;

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Generic primitive for enum types.
  /// </summary>
  public struct EnumVal<Tenum> : IPrimitive where Tenum : struct, Enum
  {
    private Tenum _Value;
    private Func<Tenum, string> _WriteUtil;

    public void Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: '{_WriteUtil(_Value)}'", indent);

    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix} '{_WriteUtil(_Value)}'", indent);

    public EnumVal(Tenum value, Func<Tenum, string> toString )
    {
      _Value = value;
      _WriteUtil = toString;
    }
  }

}