/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace kondensor.Pipes;


public struct FragHtmlPipe : IPipe, IPipeWriter
{
  private FragContext _Data;

  public FragHtmlPipe(
    TextReader input,
    TextPipeWriter output
  )
  {
    _Data = new FragContext(input, output);
  }

  public bool IsInFlowEnded => throw new NotImplementedException();

  public bool IsCheckPointingSupported => false;

  public void AddPreprocessor(IPreprocessor processor)
  {
    if (processor != null)
      _Data._Preprocessors.Add(processor);
  }

  public void ClosePipe()
  {
    _Data._Output.ClosePipe();
    _Data._Input.Close();
  }

  public bool IsLineTerminated()
    => _Data._Output.IsLineTerminated();

  public bool IsPipeOpen()
    => ! _Data._EoInput && _Data._Output.IsPipeOpen();

  public IPipeCheckPoint GetCheckPoint()
  {
    throw new NotImplementedException();
  }

  public bool ReadToken(out string token)
  {
    bool isFound = false;

    int tokenStart = FragDataOps.GetTokenStart(ref _Data);
    isFound = FragDataOps.GetTokenFromStartIndex(ref _Data, tokenStart, out token);
    return isFound;
  }

  public IPipeWriter WriteFragment(string fragment)
    => _Data._Output.WriteFragment(fragment);

  public IPipeWriter WriteFragmentLine(string fragment)
    => _Data._Output.WriteFragmentLine(fragment);

  public bool TryScanAheadFor(char[] search, out int matchIndex)
    => FragDataOps.TryScan(ref _Data, search, out matchIndex);


  public ScanResult ScanAhead(ScanRule rule)
  {
    ScanResult scan = new ScanResult();

    if (_Data._EoInput)
    {
      scan = new ScanResult() {
        IsMatched = false,
        Index = -1
      };
    }
    else if (! FragDataOps.NeedNewBuffer(ref _Data))
    {
      Span<char> totalBuffer = _Data._Buffer;
      Span<char> unprocessedBuffer = totalBuffer.Slice(_Data._BufferIndex);
      string text = unprocessedBuffer.ToString();
      scan = rule(text);
      if (scan.IsMatched)
        _Data._BufferIndex = _Data._BufferIndex + scan.Index;
      else
        scan = FragDataOps.ScanForNewBuffer(ref _Data, rule);
    }
    else
    {
      scan = FragDataOps.ScanForNewBuffer(ref _Data, rule);
    }
    return scan;
  }

  public void RestoreToCheckPoint(IPipeCheckPoint checkpoint)
  {
    throw new NotImplementedException();
  }

  internal const string EMPTY = "";
}
