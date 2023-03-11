/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.IO;


namespace kondensor.cfgenlib
{

  public struct TextFileStream : ITextStream, IDisposable
  {
    private StreamWriter _File;

    public void Close()
      => _File.Close();

    public void Dispose()
      => _File.Dispose();

    public void Write(string format, params object[] values)
      => _File.Write(format, values);

    public void WriteLine(string format, params object[] values)
      => _File.WriteLine(format, values);

    /// <summary>
    /// Create a new file, overwriting existing files, with
    /// path and filename supplied in a string.
    /// </summary>
    /// <param name="filePathAndName">Path and filename.</param>
    public TextFileStream(string filePathAndName )
    {
      _File = File.CreateText(filePathAndName);
      _File.AutoFlush = true;
    }
  }
}