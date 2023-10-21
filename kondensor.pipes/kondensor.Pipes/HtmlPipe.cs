/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace kondensor.Pipes;


public struct HtmlPipe : IPipe
{

  private HtmlContext _Data;

  public HtmlPipe(TextReader input, TextWriter output)
  {
    _Data = new HtmlContext(input, output);
  }

  public void ClosePipe()
  {
    _Data._Input.Close();
    _Data._Output.ClosePipe();
    _Data._IsOpen = false;
  }

  public bool IsPipeOpen()
    => _Data._IsOpen;

  public bool IsInFlowEnded => _Data._EofInput;

  public bool IsCheckPointingSupported => true;

  public bool ReadToken(out string token)
  {
    bool isOk;
    if (HtmlPipeQOps.IsQueueEmpty(ref _Data))
    {
      do
      {
        isOk = HtmlTokenOps.GetTokenFromInput(ref _Data, out token);
      } while( isOk && ! _Data._EofInput && token.Length == 0);
    }
    else 
    {
      token = HtmlTokenOps.DequeueTokenOrEmpty(ref _Data);
      isOk = true;
    }
    return isOk;
  }

  public IPipeWriter WriteFragment(string fragment)
    => _Data._Output.WriteFragment(fragment);

  public IPipeWriter WriteFragmentLine(string fragment)
    => _Data._Output.WriteFragmentLine(fragment);

  public bool IsLineTerminated()
    => _Data.IsLineTerminated();

  public bool TryScanAheadFor(char[] searchArray, out int matchIndex)
  {
    bool isFound = false;
    bool isNotEof = ! _Data._EofInput;
    int internalStartIndex = _Data._UnprocessedIndex;
    Span<char> buffer = new Span<char>(_Data._UnprocessedText);
    Span<char> search = new Span<char>(searchArray);

    matchIndex = 0;

    while(! isFound && isNotEof)
    {
      if (internalStartIndex >= _Data._UnprocessedText.Length )
      {
        HtmlTokenOps.TryReadInputAndPreprocess(ref _Data);
        buffer = new Span<char>(_Data._UnprocessedText);
        isNotEof = ! _Data._EofInput;
        internalStartIndex = 0;
      }
      isFound = PreprocessorUtils.FindNextMatch(
        buffer,
        search,
        internalStartIndex,
        out int foundIndex
      );
      if (isFound)
      {
        _Data._UnprocessedIndex = foundIndex;
        matchIndex = foundIndex;
      }
      else
        internalStartIndex = GetBufferEndIndex();
    }
    return isFound;
  }

  public ScanResult ScanAhead(ScanRule rule)
  {
    ScanResult result = new ScanResult();
    bool isEof = _Data._EofInput;
    string input  = "";
    HtmlContext ScanData = _Data;

    if ( ScanData._UnprocessedText.Length > 0 )
    {
      input = ScanData._UnprocessedText.ToString() ?? "";
      result = rule(input);
    }
    while(! result.IsMatched
      && ! (isEof && HtmlPipeQOps.IsQueueEmpty(ref ScanData) )
    )
    {
      isEof = ! HtmlTokenOps.GetTokenFromInput(ref ScanData, out input, rule);
      if (! isEof)
        result = rule(input);
    }
    if (result.IsMatched)
    {
      _Data._InputQueue = ScanData._InputQueue;
      _Data._QueueIndex = ScanData._QueueIndex;
      _Data._UnprocessedText = ScanData._UnprocessedText;
      _Data._UnprocessedIndex = ScanData._UnprocessedIndex;
      _Data._EofInput = ScanData._EofInput;
    }
    return result;
  }

  public void AddPreprocessor(IPreprocessor processor)
  {
    _Data._Preprocessors.Add(processor);
  }

  private int GetBufferEndIndex() => _Data._UnprocessedText.Length;

  public IPipeCheckPoint GetCheckPoint()
  {
    var checkpoint = new HtmlPipeCheckPoint(ref _Data);
    return checkpoint;
  }

  public void RestoreToCheckPoint(IPipeCheckPoint checkpoint)
  {
    if (checkpoint is HtmlPipeCheckPoint htmlPoint)
    {
      htmlPoint.restoreTo(ref _Data);
    }
    else
      throw new InvalidEnumArgumentException(
        message: $"{checkpoint.GetType().Name} cannot be passed as a checkpoint."
      );
  }
}
