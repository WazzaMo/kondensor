/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.IO;

using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.writer
{

  public struct OutputsWriter 
  {
    public static ITextStream Write(
      ITextStream output,
      Outputs outputsList,
      string indent
    )
    {
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;
      
      if (outputsList.HasOutputs)
      {
        YamlWriter.Write(output, "Outputs:", indent);
        outputsList.ForEachOutput( entry => entry.Write(output, _1_indent));
      }
      return output;
    }

  }

}