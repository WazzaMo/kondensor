
using System;
using System.IO;

namespace kondensor.cfgenlib.primitives
{

  public interface IPrimitive
  {
    /// <summary>
    /// Write the primitive's name (from resource property) and value.
    /// </summary>
    /// <param name="output">Output to write into</param>
    /// <param name="name">Name to write</param>
    /// <param name="indent">Base indent to use</param>
    void Write(StreamWriter output, string name, string indent);
  }

}