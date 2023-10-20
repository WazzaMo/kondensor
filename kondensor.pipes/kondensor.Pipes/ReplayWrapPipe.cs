/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System.Collections.Generic;

namespace kondensor.Pipes;


/// <summary>
/// Wrapper pipe that adds to replay ability to a simple pipe.
/// </summary>
public struct ReplayWrapPipe : IPipe
{
  private IPipe _BasePipe;
  private List<string> _TokenHistory;
  /// <summary>Token history index</summary>
  private int[] _TokenHistoryIndexRaw;

  private int TokenHistoryIndex {
    get { return _TokenHistoryIndexRaw[0]; }
    set { _TokenHistoryIndexRaw[0] = value; }
  }

  public bool IsInFlowEnded => _BasePipe.IsInFlowEnded;

  public ReplayWrapPipe(IPipe mainPipe)
  {
    _BasePipe = mainPipe;
    _TokenHistory = new List<string>();
    _TokenHistoryIndexRaw = new int[]{0};
  }

  public void ClosePipe()
    => _BasePipe.ClosePipe();

  public bool IsPipeOpen()
    => _BasePipe.IsPipeOpen();

  public bool ReadToken(out string token)
  {
    bool isOk;
    int readIndex = TokenHistoryIndex;

    if (readIndex == _TokenHistory.Count)
    {
      isOk = _BasePipe.ReadToken(out token);
      if (isOk)
      {
        _TokenHistory.Add(token);
        readIndex++;
      }
    }
    else
    {
      token = _TokenHistory[readIndex];
      readIndex++;
      isOk = true;
    }
    TokenHistoryIndex = readIndex;
    return isOk;
  }

  public bool TryScanAheadFor(char[] search, out int matchIndex)
    => _BasePipe.TryScanAheadFor(search, out matchIndex);

  public ScanResult ScanAhead(ScanRule rule)
  {
    var result = _BasePipe.ScanAhead(rule);
    if (result.IsMatched)
      return ReplayTokenHistoryToMatchBase(rule);
    return result;
  }

  public IPipeWriter WriteFragment(string fragment)
  {
    _BasePipe.WriteFragment(fragment);
    return (IPipeWriter) this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    _BasePipe.WriteFragmentLine(fragment);
    return (IPipeWriter) this;
  }

  public bool IsLineTerminated() => _BasePipe.IsLineTerminated();


  public int GetCheckPoint() => TokenHistoryIndex;

  public void ReturnToCheckPoint(int checkPoint)
  {
    if (checkPoint <= _TokenHistory.Count)
    {
      TokenHistoryIndex = checkPoint;
    }
    else
      throw new ArgumentException(message: $"Illegal checkpoint value given {checkPoint}, history is only {_TokenHistory.Count} long");
  }

  public void AddPreprocessor(IPreprocessor processor)
  {
    _BasePipe.AddPreprocessor(processor);
  }

  private ScanResult ReplayTokenHistoryToMatchBase(ScanRule rule)
  {
    ScanResult seek = new ScanResult();
    int desiredIndex = -1;
    string value;

    while(! seek.IsMatched && (desiredIndex+1) < _TokenHistory.Count)
    {
      desiredIndex++;
      value = _TokenHistory[desiredIndex];
      seek = rule(value);
    }
    if (seek.IsMatched)
      TokenHistoryIndex = desiredIndex;
    return seek;
  }
}
