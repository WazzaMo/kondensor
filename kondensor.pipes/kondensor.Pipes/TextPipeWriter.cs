/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;

namespace kondensor.Pipes;

public struct TextPipeWriter : IPipeWriter
{
  private TextWriter _Writer;
  private bool _IsWriterOpen;
  private bool _LineTerminated;

  public TextPipeWriter(TextWriter writer)
  {
    _Writer = writer;
    _IsWriterOpen = true;
    _LineTerminated = false;
  }

  public void ClosePipe()
  {
    _Writer.Close();
    _IsWriterOpen = false;
  }

  public bool IsLineTerminated()
    => _LineTerminated;
  
  public bool IsPipeOpen()
    => _IsWriterOpen;

  public IPipeWriter WriteFragment(string fragment)
  {
    AssertOpenForWrite();
    _Writer.Write(fragment);
    UnterminatedLineWritten();
    return this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    AssertOpenForWrite();
    if (fragment.Length == 0)
    {
      if (!IsLineTerminated())
        _Writer.WriteLine("");
    }
    else
    {
      _Writer.WriteLine(fragment);
    }
    TerminateLine();
    return this;
  }

  private void AssertOpenForWrite()
  {
    if (! _IsWriterOpen)
      throw new InvalidOperationException(message: "Pipe has been closed and cannot be written into.");
  }

  private void TerminateLine() => _LineTerminated = true;
  private void UnterminatedLineWritten() => _LineTerminated = false;
}