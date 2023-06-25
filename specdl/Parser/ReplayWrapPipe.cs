/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;
using System.Collections.Generic;

namespace Parser
{

  /// <summary>
  /// Wrapper pipe that adds to replay ability to a simple pipe.
  /// </summary>
  public struct ReplayWrapPipe : IPipe
  {
    private IPipe _BasePipe;
    private List<string> _TokenHistory;
    private int[] _ReadIndex;

    private int ReadIndex {
      get { return _ReadIndex[0]; }
      set { _ReadIndex[0] = value; }
    }

    public bool IsInFlowEnded => _BasePipe.IsInFlowEnded;

    public ReplayWrapPipe(IPipe mainPipe)
    {
      _BasePipe = mainPipe;
      _TokenHistory = new List<string>();
      _ReadIndex = new int[]{0};
    }

    public void ClosePipe()
      => _BasePipe.ClosePipe();

    public bool IsPipeOpen()
      => _BasePipe.IsPipeOpen();

    public bool ReadToken(out string token)
    {
      bool isOk;
      int readIndex = ReadIndex;

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
      ReadIndex = readIndex;
      return isOk;
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

    public int GetCheckPoint() => ReadIndex;

    public void ReturnToCheckPoint(int checkPoint)
    {
      if (checkPoint < _TokenHistory.Count)
      {
        ReadIndex = checkPoint;
      }
      else
        throw new ArgumentException(message: $"Illegal checkpoint value given {checkPoint}, history is only {_TokenHistory.Count} long");
    }

    public void AddPreprocessor(IPreprocessor processor)
    {
      _BasePipe.AddPreprocessor(processor);
    }
  }
}