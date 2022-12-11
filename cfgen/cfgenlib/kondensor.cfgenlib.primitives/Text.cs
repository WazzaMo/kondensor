
using System.Collections.Concurrent;

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Represents a YAML string value.
  /// </summary>
  public struct Text : IPrimitive
  {
    public string Value;

    public void Write(StreamWriter output, string name, string indent)
    {
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;
      string[] parts = SplitTextByLength(Value, MAX_LEN);

      YamlWriter.Write(output, $"{name}: {parts[0]}", _0_indent);
      for(int index = 1; index < parts.Length; index++)
      {
        YamlWriter.Write(output, parts[index], _1_indent);
      }
    }

    public Text(string text)
    {
      Value = text;
    }

    public const int MAX_LEN = 40;

    /// <summary>
    /// Utility for splitting text by length, into regular chunk sizes.
    /// </summary>
    /// <param name="original">Original string to break up.</param>
    /// <param name="maxLen">Max length to apply.</param>
    /// <returns></returns>
    public static string[] SplitTextByLength(string original, int maxLen)
    {
      int remaining = original.Length;
      int position = 0;

      List<string> chunks = new List<string>();

      while(remaining > 0)
      {
        int len = remaining > maxLen ? maxLen : remaining;
        var chunk = position > 0 && original.Length >= maxLen
          ? original.Substring(position, len)
          : original;
        chunks.Add(chunk);
        position += len;
        remaining -= len;
      }
      return chunks.ToArray();
    }
  }

}
