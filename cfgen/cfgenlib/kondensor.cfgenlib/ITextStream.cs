/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

namespace kondensor.cfgenlib
{

  /// <summary>
  /// Dependency injectable text stream protocol.
  /// Allows different ways to write text to be supported.
  /// </summary>
  public interface ITextStream : IDisposable
  {
    public void Close();

    /// <summary>
    /// Write text out without ending the line. Allows for append operations.
    /// </summary>
    /// <param name="format">
    ///   Format specification that allows raw text or format, such as "{0} {1}" value positionals.
    ///   <see href="https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-7.0#system-string-format(system-string-system-object)"/>
    /// </param>
    /// <param name="values">Optional value positionals for format use.</param>
    public void Write(string format, params object[] values);

    /// <summary>
    /// Write line of text with line ending character (typically newline).
    /// Allows format string usage as per
    /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-7.0#system-string-format(system-string-system-object)"/>
    /// </summary>
    /// <param name="format">Format string or raw text to write out with newline added to the end.</param>
    /// <param name="values">Values for positional format injection.</param>
    public void WriteLine(string format, params object[] values);
  }

}