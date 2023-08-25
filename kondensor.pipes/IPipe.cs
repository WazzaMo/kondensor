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
  public interface IPipe : IPipeWriter
  {
    /// <summary>
    /// Read a token and indicate if value is valid.
    /// </summary>
    /// <param name="token">token to return</param>
    /// <returns>True if valid, false if stream ended.</returns>
    bool ReadToken(out string token);

    bool IsInFlowEnded {get; }

    void ClosePipe();

    bool IsPipeOpen();

    void AddPreprocessor(IPreprocessor processor);
  }

}