/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;

namespace kondensor.Pipes;

/// <summary>
/// An input/output pipe for text, divided by lines. A simple example of a pipe
/// implementation that also supports text preprocessing (search/replace).
/// This type is passed by value, so use ref in function parameters where
/// the data must be kept consistent across call boundaries.
/// </summary>
public struct TextPipe : IPipe
{
  private TextWriter _Writer;
  private TextReader _Reader;

  private bool _IsEofHit;
  private List<IPreprocessor> _Preprocessors;

  public TextPipe(TextWriter? writer = null, TextReader? reader = null)
  {
    _Writer = writer == null
      ? Console.Out
      : writer;
    _Reader = reader == null
      ? Console.In
      : reader;

    _IsEofHit = false;
    _Preprocessors = new List<IPreprocessor>();
  }

  public bool IsInFlowEnded => _IsEofHit;

  public void AddPreprocessor(IPreprocessor processor)
    => _Preprocessors.Add(processor);

  public void ClosePipe()
  {
    _IsEofHit = true;
    _Writer.Close();
  }

  public bool IsLineTerminated()
    => true;

  public bool IsPipeOpen()
    => _IsEofHit;

  public bool ReadToken(out string token)
  {
    var line = _Reader.ReadLine();
    bool isEof = line == null;

    _IsEofHit = isEof;
    token = line == null ? "" : PreprocessLine(line);
    return ! isEof;
  }

  public IPipeWriter WriteFragment(string fragment)
  {
    _Writer.Write(fragment);
    return this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    _Writer.WriteLine(fragment);
    return this;
  }

  private string PreprocessLine(string line)
  {
    string finalLine;

    char[] recentText = line.ToArray();
    char[] workInProcess;
    bool isProcessed;
    IPreprocessor processor;

    workInProcess = new char[0];

    for(int idx = 0; idx < _Preprocessors.Count; idx++)
    {
      processor = _Preprocessors.ElementAt(idx);
      isProcessed = processor.ProcessText(recentText, out workInProcess);
      if (isProcessed)
        recentText = workInProcess;
    }
      
    finalLine = (recentText != null && recentText.Length > 0)
      ? new string(recentText)
      : "";

    return finalLine;
  }
}