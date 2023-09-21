/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Text;

namespace kondensor.cfgenlib
{

  /// <summary>
  /// A text buffer implementing the <see cref="ITextStream"/> protocol.
  /// </summary>
  public struct TextBufferStream : ITextStream
  {
    private StringBuilder _Builder;

    public void Close()
    {}

    public void Dispose()
      => _Builder.Clear();

    public void Write(string format, params object[] values)
      => _Builder.AppendFormat(format, values).AppendLine();

    public void WriteLine(string format, params object[] values)
      => _Builder.AppendFormat(format, values).AppendLine();

    /// <summary>
    /// Takes all the built content and clears the buffer
    /// for reuse.
    /// </summary>
    /// <returns>String content of the buffer.</returns>
    public string TakeContentAndClear()
    {
      string value = _Builder.ToString();
      _Builder.Clear();
      return value;
    }

    public TextBufferStream()
    {
      _Builder = new StringBuilder();
    }
  }
}