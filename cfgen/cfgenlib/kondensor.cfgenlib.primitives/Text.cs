/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Collections.Concurrent;

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Represents a YAML string value.
  /// Will format it as a single short key: value
  /// or for a longer value as
  ///   key:
  ///     value.....
  ///     ....
  ///     value.
  /// </summary>
  public struct Text : IPrimitive
  {
    public string Value;

    public void Write(ITextStream output, string name, string indent)
    {
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;
      string prefix = $"{name}: ";

      if (Value.Length > MAX_LEN)
      {
        LongLineWrite(output, prefix, indent);
      }
      else
      {
        ShortLineWrite(output, prefix, indent);
      }
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      if (Value.Length > MAX_LEN)
      {
        LongLineWrite(output, prefix, indent);
      }
      else
      {
        ShortLineWrite(output, prefix, indent);
      }
    }

    private void LongLineWrite(ITextStream output, string prefix, string indent)
    {
      // Format longer lines over multiple YAML outputs.
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;

      string[] parts = SplitTextByLengthAndLastSpace(Value);

      YamlWriter.Write(output, prefix, _0_indent);
      
      for(int index = 0; index < parts.Length; index++)
      {
        YamlWriter.Write(output, message: parts[index], _1_indent);
      }
    }

    private void ShortLineWrite(ITextStream output, string prefix, string indent)
    {
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;

        YamlWriter.Write(output, message: $"{prefix} {Value}", _0_indent);
    }

    public Text(string text)
    {
      Value = text;
    }

    /// <summary>
    /// Maximum length used by <see cref="SplitTextByLengthAndLastSpace(string)"/>
    /// </summary>
    public const int MAX_LEN = 40;

    /// <summary>
    /// Utility for splitting text by length, into regular chunk sizes.
    /// </summary>
    /// <param name="original">Original string to break up.</param>
    /// <param name="maxLen">Max length to apply.</param>
    /// <returns></returns>
    public static string[] SplitTextByLengthAndLastSpace(string original)
    {

      int remaining = original.Length;
      int position = 0;

      List<string> chunks = new List<string>();

      while(remaining > 0)
      {
        int spaceIndex = LastNonSpaceIndexOf(original, position, remaining);

        int len = remaining > spaceIndex ? spaceIndex : remaining;
        
        var chunk = original.Substring(position, len).Trim();
        chunks.Add(chunk);
        position += chunk.Length;
        remaining -= chunk.Length;
      }
      return chunks.ToArray();
    }

    /// <summary>
    /// Finds last index of either space or remaining text.
    /// </summary>
    /// <param name="searchText">String to search for SPACE</param>
    /// <param name="position">Position to start search</param>
    /// <param name="remaining">Length of text remaining to search.</param>
    /// <returns>Index after space or the remaining value if not found.</returns>
    private static int LastNonSpaceIndexOf(string searchText, int position, int remaining)
    {
      const char SPACE = ' ';

      int searchLen = remaining > MAX_LEN ? MAX_LEN : remaining;
      string searchString = searchText.Substring(position, searchLen);
      int spaceIndex = searchText.LastIndexOf(SPACE);
      return searchText[spaceIndex] == ' '
        ? spaceIndex + 1
        : spaceIndex;
    }
  } // -- Text


}
