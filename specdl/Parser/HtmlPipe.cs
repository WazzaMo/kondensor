/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;

using System.IO;
using System.Collections.Generic;
using System.Text;
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
        do
        {
          isOk = GetTokenFromInput(out token);
        } while( isOk && ! _EofInput && token.Length == 0);
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

      if (_EofInput)
      {
        isOk = false;
        token = "";
      }
      else
      {
        string? line = GreedyRead();// _Input.ReadLine();
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
      }
      return isOk;
    }

    /// <summary>
    /// Read until next token starts.
    /// </summary>
    /// <returns>One or more lines of text</returns>
    private string? GreedyRead()
    {
      string? result = null;
      StringBuilder builder = new StringBuilder();

      int charInput;
      int tokenCount = 0;

      do
      {
        charInput = _Input.Peek();
        tokenCount = ((char)charInput) == '<' ? tokenCount + 1 : tokenCount;
        if ( charInput > 0 && tokenCount < 2)
        {
          builder.Append((char) _Input.Read());
        }
      } while( charInput > 0 && tokenCount < 2);

      if (builder.Length > 0)
      {
        result = builder.ToString();
      }
      return result;
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

        string sub = line.Substring(index1, length).Trim();
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