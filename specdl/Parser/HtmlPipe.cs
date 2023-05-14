/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;

using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Parser
{

  public struct HtmlPipe : IPipe
  {
    private static readonly Regex __LineSep = new Regex(pattern: @"\<");

    private TextReader _Input;
    private Queue<string> _InputQueue;
    private TextWriter _Output;
    private bool _IsOpen;
    private bool _EofInput;

    public HtmlPipe(TextReader input, TextWriter output)
    {
      _Input = input;
      _InputQueue = new Queue<string>();
      _Output = output;
      _IsOpen = true;
      _EofInput = false;
    }

    public void ClosePipe()
    {
      _Input.Close();
      _Output.Close();
      _IsOpen = false;
    }

    public bool IsPipeOpen()
      => _IsOpen;

    public bool IsInFlowEnded => _EofInput;

    public bool ReadToken(out string token)
    {
      bool isOk;
      if (_InputQueue.Count == 0)
      {
        isOk = GetTokenFromInput(out token);
      }
      else 
      {
        token = DequeueTokenOrEmpty();
        isOk = true;
      }
      return isOk;
    }

    public void WriteFragment(string fragment)
      => _Output.Write(fragment);

    public void WriteFragmentLine(string fragment)
      => _Output.WriteLine(fragment);

    private bool GetTokenFromInput(out string token)
    {
      bool isOk;
      string? line = _Input.ReadLine();

      if (line != null)
      {
        TokeniseLineParts(line);
        token = DequeueTokenOrEmpty();
        isOk = true;
      }
      else
      {
        _EofInput = true;
        isOk = false;
        token = "";
      }
      return isOk;
    }

    private void TokeniseLineParts(string line)
    {
      MatchCollection parts = __LineSep.Matches(line);

      for(int partIndex = 0; partIndex < parts.Count; partIndex++)
      {
        int index1, index2, length;

        index1 = parts[partIndex].Index;
        index2 = partIndex < (parts.Count - 1)
          ? parts[partIndex + 1].Index
          : line.Length;
        length = index2 - index1;

        string sub = line.Substring(index1, length);
        _InputQueue.Enqueue(sub);
      }
    }

    private string DequeueTokenOrEmpty()
    {
      string value = _InputQueue.Count > 0
        ? _InputQueue.Dequeue()
        : "";
      return value;
    }
  }

}