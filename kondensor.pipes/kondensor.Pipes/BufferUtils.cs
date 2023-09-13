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

  /// <summary>
  /// Searches for non-whitespace, returning the index
  /// where a non-whitespace character is found.
  /// </summary>
  /// <param name="buffer">Buffer to scan</param>
  /// <param name="startIndex">(optional) index to start scanning.</param>
  /// <returns>
  ///   0 to length when non-WS char found;
  ///   <see href="INDEX_END_BUFFER" /> if over the end of the buffer
  /// </returns>
  internal static int ScanForNextNonWhitespace(char[] buffer, int startIndex = 0)
  {
    int index = startIndex;
    while(IsValidIndex(buffer, index) && Char.IsWhiteSpace(buffer[index]))
    {
      index++;
    }
    if (index >= buffer.Length)
      index = INDEX_END_BUFFER;
    return index;
  }

  /// <summary>
  /// Searches for whitespace, returning the index
  /// where a whitespace character is found.
  /// </summary>
  /// <param name="buffer">Buffer to scan</param>
  /// <param name="startIndex">index to start scanning.</param>
  /// <returns>
  ///   0 to length when non-WS char found;
  ///   <see href="INDEX_END_BUFFER" /> if over the end of the buffer
  /// </returns>
  internal static int ScanForWhitespace(char[] buffer, int startIndex)
  {
    int index = startIndex;
    while(IsValidIndex(buffer, index) && ! Char.IsWhiteSpace(buffer[index]))
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
