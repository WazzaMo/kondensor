
using System.IO;

using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.writer
{

  public struct OutputsWriter 
  {
    public static StreamWriter Write(
      StreamWriter output,
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