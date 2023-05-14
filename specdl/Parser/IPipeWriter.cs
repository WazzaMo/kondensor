/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace Parser
{

  /// <summary>
  /// A compiler is a pipe that reads from input and writes generated output.
  /// </summary>
  public interface IPipeWriter
  {
    /// <summary>
    /// Write string without any line ending.
    /// </summary>
    /// <param name="fragment">Text fragment to write.</param>
    void WriteFragment(string fragment);

    /// <summary>
    /// Write string fragment with line ending.
    /// </summary>
    /// <param name="fragment">Text fragment to write.</param>
    void WriteFragmentLine(string fragment);

  }

}