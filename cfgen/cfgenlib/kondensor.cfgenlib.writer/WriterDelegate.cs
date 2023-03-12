/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.IO;
using System.Collections.Generic;
using Optional;

namespace kondensor.cfgenlib.writer
{

  /// <summary>
  /// Standard function for writing a type for registration.
  /// </summary>
  /// <param name="output">ITextStream to write into</param>
  /// <param name="value">Value to write</param>
  /// <param name="indent">Prefixed indent</param>
  /// <typeparam name="T">Type of value to write</typeparam>
  /// <returns>ITextStream object.</returns>
  public delegate ITextStream WriterDelegate<T>(
    ITextStream output, T value, string indent
  ) where T : struct;

  public delegate ITextStream ListWriterDelegate<T>(
    ITextStream output, List<T> list, string indent
  ) where T : struct;

  public delegate ITextStream Writer(
    ITextStream output, Option<object> value, Type type, string indent
  );

}