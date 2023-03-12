/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.outputs;
using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{


  public struct Import : IReference
  {
    private IExport _Exported;

    public string Imported => _Exported.ExportValue;

    private readonly string FULL_IMPORT_FUNCTION = "Fn::ImportValue:";
    public void Write(ITextStream output, string name, string indent)
    {
      string _0_indent = indent,
             _1_indent = _0_indent + YamlWriter.INDENT,
             _2_indent = _1_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, $"{name}:", _0_indent);
      YamlWriter.Write(output, FULL_IMPORT_FUNCTION, _1_indent);
      YamlWriter.Write(output, Imported, _2_indent);
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      string _0_indent = indent,
             _1_indent = _0_indent + YamlWriter.INDENT,
             _2_indent = _1_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, prefix, _0_indent);
      YamlWriter.Write(output, FULL_IMPORT_FUNCTION, _1_indent);
      YamlWriter.Write(output, Imported, _2_indent);
    }

    public Import(IExport exported)
    {
      _Exported = exported;
    }
  }


}