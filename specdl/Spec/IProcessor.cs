/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.IO;

using Parser;

namespace Spec;


/// <summary>
/// A processor reads each line of the input document, and optionally, writes
/// to the output document. This interface allows processors to be plugged-in
/// depending on context or situation.
/// </summary>
public interface IProcessor
{
  /// <summary>
  /// Process all input lines of text. Write output lines as needed,
  /// determined by the process method.
  /// </summary>
  /// <param name="countHandled">Number of lines processed.</param>
  /// <param name="input">source of text to process.</param>
  /// <param name="output">destination to write any processed data, if needed.</param>
  void ProcessAllLines(ReplayWrapPipe pipe);
}