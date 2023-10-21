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

  public bool IsCheckPointingSupported
    => _BasePipe.IsCheckPointingSupported;

  public ReplayWrapPipe(IPipe mainPipe)
  {
    _BasePipe = mainPipe;
    _TokenHistory = new List<string>();
    _TokenHistoryIndexRaw = new int[]{0};
  }

  public void AddPreprocessor(IPreprocessor processor)
    => _BasePipe.AddPreprocessor(processor);

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
      // throw new InvalidOperationException("Re-reading data.");
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


  public IPipeCheckPoint GetCheckPoint()
    => new ReplayWrapCheckPoint(TokenHistoryIndex, _BasePipe);

  private ScanResult ReplayTokenHistoryToMatchBase(ScanRule rule)
  {
    ScanResult seek = new ScanResult();
    bool isMoreData = true;
    int desiredIndex = 0;
    string value;

    while(! seek.IsMatched && isMoreData )
    {
      desiredIndex = TokenHistoryIndex;
      isMoreData = ReadToken(out value);
      seek = rule(value);
    }
    if (seek.IsMatched)
      TokenHistoryIndex = desiredIndex;
    RemoveHistoryBeyond(desiredIndex);
    return seek;
  }

  private void RemoveHistoryBeyond(int index)
  {
    for(
      int current = _TokenHistory.Count - 1;
      current > index;
      current--
    )
    {
      _TokenHistory.RemoveAt(current);
    }
  }

  public void RestoreToCheckPoint(IPipeCheckPoint checkpointWrap)
  {
    if (checkpointWrap is ReplayWrapCheckPoint rwCp)
    {
      int checkPoint = rwCp.TokenHistoryIndex;

      _BasePipe.RestoreToCheckPoint(rwCp._BasePipeCheckPoint);
      if (checkPoint <= _TokenHistory.Count)
      {
        TokenHistoryIndex = checkPoint;
      }
      else
        throw new ArgumentException(
          message: $"Illegal checkpoint value given {checkPoint}, history is only {_TokenHistory.Count} long"
        );
    }
    else
      throw new ArgumentException(
        message: $"Checkpoint type {checkpointWrap.GetType().Name} is invalid to use with {nameof(ReplayWrapPipe)}"
      );
  }
}
