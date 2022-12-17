using System.IO;
using System.Collections.Generic;
using Optional;

namespace kondensor.cfgenlib.writer
{

  /// <summary>
  /// Standard function for writing a type for registration.
  /// </summary>
  /// <param name="output">StreamWriter to write into</param>
  /// <param name="value">Value to write</param>
  /// <param name="indent">Prefixed indent</param>
  /// <typeparam name="T">Type of value to write</typeparam>
  /// <returns>StreamWriter object.</returns>
  public delegate StreamWriter WriterDelegate<T>(
    StreamWriter output, T value, string indent
  ) where T : struct;

  public delegate StreamWriter ListWriterDelegate<T>(
    StreamWriter output, List<T> list, string indent
  ) where T : struct;

  public delegate StreamWriter Writer(
    StreamWriter output, Option<object> value, Type type, string indent
  );

}