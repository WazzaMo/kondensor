/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;

namespace kondensor.Pipes;

internal static class BufferUtils
{
  internal const int INDEX_END_BUFFER = -1;

  /// <summary>Creates an empty buffer array.</summary>
  /// <value>char[] of zero length.</value>
  internal static char[] EmptyBuffer => new char[0] { };

  /// <summary>Rule for scanning characters.</summary>
  /// <param name="_char">Character to check for a match</param>
  /// <returns>True when matched, False if not significant.</returns>
  internal delegate bool ScanRule(char _char);

  /// <summary>
  /// Searches through a collection of symbol boundary characters, looking
  /// for the next non-boundary, returning the index
  /// where a non-boundary character is found.
  /// </summary>
  /// <param name="wordSeparator">Scan for character that returns true as a match.</param>
  /// <param name="buffer">Buffer to scan</param>
  /// <param name="startIndex">(optional) index to start scanning.</param>
  /// <returns>
  ///   0 to length when non-WS char found;
  ///   <see href="INDEX_END_BUFFER" /> if over the end of the buffer
  /// </returns>
  internal static int ScanForSymbolStart(
    ScanRule wordSeparator,
    char[] buffer,
    int startIndex = 0
  )
  {
    int index = startIndex;
    while(IsValidIndex(buffer, index) && wordSeparator(buffer[index]))
    {
      index++;
    }
    if (index >= buffer.Length)
      index = INDEX_END_BUFFER;
    return index;
  }

  /// <summary>
  /// Searches for symbol characters, returning the index
  /// where at the edge of a symbol.
  /// </summary>
  /// <param name="symbolRule">Rule defining valid symbol characters.</param>
  /// <param name="buffer">Buffer to scan</param>
  /// <param name="startIndex">index to start scanning.</param>
  /// <returns>
  ///   0 to length when non-WS char found;
  ///   <see href="INDEX_END_BUFFER" /> if over the end of the buffer
  /// </returns>
  internal static int ScanForEndOfSymbol(
    ScanRule symbolRule,
    char[] buffer,
    int startIndex
  )
  {
    int index = startIndex;
    while(IsValidIndex(buffer, index) && symbolRule(buffer[index]) )
    {
      index++;
    }
    if (index >= buffer.Length)
      index = INDEX_END_BUFFER;
    return index;
  }

  internal static bool IsValidIndex(char[] buffer, int index)
    => index >= 0 && index < buffer.Length;
  
  internal static char[] GetWhitespaceTerminatedBufferFromString(string? input)
  {
    string temp = input != null ? WhitespaceTerminate(input) : "";
    return temp.ToCharArray();
  }

  internal static string WhitespaceTerminate(string invalue)
    => invalue + " ";
}
