
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
    void Write(ITextStream output, string name, string indent);

    /// <summary>
    /// Write a prefixed value to the output stream.
    /// This was added to support <see href="PrimitiveList"/> originally
    /// and allows values to be prefixed with any given string.
    /// </summary>
    /// <param name="output">Output stream</param>
    /// <param name="prefix">Prefix to used - can be blank string.</param>
    /// <param name="indent">Indent to use before prefix.</param>
    void WritePrefixed(ITextStream output, string prefix, string indent);

  }

}