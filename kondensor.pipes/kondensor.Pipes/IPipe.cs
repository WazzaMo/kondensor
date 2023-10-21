/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

namespace kondensor.Pipes;

/// <summary>
/// A compiler is a pipe that reads from input and writes generated output.
/// </summary>
public interface IPipe : IPipeWriter
{
  /// <summary>
  /// Register a preprocessor to perform text operations before lexical matching.
  /// </summary>
  /// <param name="processor">preprocessor to register</param>
  void AddPreprocessor(IPreprocessor processor);

  bool IsInFlowEnded { get; }

  /// <summary>Indicates if the pipe can give and restore context to a checkpoint.</summary>
  /// <value>True if supported</value>
  bool IsCheckPointingSupported { get; }

  /// <summary>
  /// Provides pipe context so the current stream position can be returned
  /// to later.
  /// Throws exception if checkpointing not supported.
  /// </summary>
  /// <exception cref="InvalidOperationException" />
  /// <returns>A checkpoint context.</returns>
  IPipeCheckPoint GetCheckPoint();

  /// <summary>
  /// Rapid scanning method for base pipe types that uses char arrays
  /// and accelerates base pipe processing for raw text.
  /// Wrap pipes will call directly into, bypassing token level operations.
  /// </summary>
  /// <param name="search">text to search for, in array form.</param>
  /// <param name="matchIndex">If match found, index to match, else -1</param>
  /// <returns>True if match found, False otherwise.</returns>
  bool TryScanAheadFor(
    char[] search,
    out int matchIndex
  );

  /// <summary>
  /// Is used to bring the pipe quickly to a point where the desired text
  /// will be the next token read. It returns an indication of success
  /// in the form of <see cref="ScanResult"/> providing a match success boolean and index.
  /// If result is a failure to match, likely cause is pipe exhausted
  /// input content.
  /// </summary>
  /// <param name="rule">Can be based on Regex or other matching method.</param>
  /// <returns><see cref="ScanResult"/> to indicate success or failure.</returns>
  ScanResult ScanAhead(ScanRule rule);

  /// <summary>
  /// Read a token and indicate if value is valid.
  /// </summary>
  /// <param name="token">token to return</param>
  /// <returns>True if valid, false if stream ended.</returns>
  bool ReadToken(out string token);

  /// <summary>
  /// For pipes that support checkpointing (confirm before calling)
  /// this returns the pipe's internal stream to the position
  /// captured when the checkpoint was taken.
  /// </summary>
  /// <param name="checkpoint"></param>
  void RestoreToCheckPoint(IPipeCheckPoint checkpoint);
}
