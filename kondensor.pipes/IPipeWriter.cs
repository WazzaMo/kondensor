/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
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
    IPipeWriter WriteFragment(string fragment);

    /// <summary>
    /// Write string fragment with line ending.
    /// </summary>
    /// <param name="fragment">Text fragment to write.</param>
    IPipeWriter WriteFragmentLine(string fragment);

    /// <summary>
    /// Indicates if last operation (e.g. WriteFragmetnLine) terminated the line.
    /// </summary>
    /// <returns>True if line has been terminated.</returns>
    bool IsLineTerminated();
  }

}