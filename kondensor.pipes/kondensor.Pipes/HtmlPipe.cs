/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */



using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace kondensor.Pipes;


public struct HtmlPipe : IPipe
{
  private static readonly Regex __LineSep = new Regex(pattern: @"\<");

  private class _InternalData
  {
    internal TextReader _Input;
    internal Queue<string> _InputQueue;
    internal TextWriter _Output;
    internal bool _IsOpen;
    internal bool _EofInput;
    internal char[] _UnprocessedText;
    internal int _UnprocessedIndex;
    internal List<IPreprocessor> _Preprocessors;
    private bool _LineTerminated;

    internal void TerminateLine() => _LineTerminated = true;

    internal void UnterminatedLineWritten() => _LineTerminated = false;

    internal bool IsLineTerminated() => _LineTerminated;

    internal _InternalData( TextReader input, TextWriter output)
    {
      _Input = input;
      _InputQueue = new Queue<string>();
      _UnprocessedText = EmptyCharArray();
      _UnprocessedIndex = 0;
      _Output = output;
      _IsOpen = true;
      _EofInput = false;
      _Preprocessors = new List<IPreprocessor>();
      _LineTerminated = false;
    }
  }

  private _InternalData _Data;

  public HtmlPipe(TextReader input, TextWriter output)
  {
    _Data = new _InternalData(input, output);
  }

  public void ClosePipe()
  {
    _Data._Input.Close();
    _Data._Output.Close();
    _Data._IsOpen = false;
  }

  public bool IsPipeOpen()
    => _Data._IsOpen;

  public bool IsInFlowEnded => _Data._EofInput;

  public bool ReadToken(out string token)
  {
    bool isOk;
    if (_Data._InputQueue.Count == 0)
    {
      do
      {
        isOk = GetTokenFromInput(out token);
      } while( isOk && ! _Data._EofInput && token.Length == 0);
    }
    else 
    {
      token = DequeueTokenOrEmpty();
      isOk = true;
    }
    return isOk;
  }

  public IPipeWriter WriteFragment(string fragment)
  {
    _Data._Output.Write(fragment);
    _Data.UnterminatedLineWritten();
    return (IPipeWriter) this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    if ( fragment.Length == 0 )
    {
      if (! _Data.IsLineTerminated())
        _Data._Output.WriteLine("");
    }
    else
    {
      _Data._Output.WriteLine(fragment);
    }
    _Data.TerminateLine();
    return (IPipeWriter) this;
  }

  public bool IsLineTerminated()
    => _Data.IsLineTerminated();
  

  public void AddPreprocessor(IPreprocessor processor)
  {
    _Data._Preprocessors.Add(processor);
  }

  private bool GetTokenFromInput(out string token)
  {
    bool isOk;

    if (_Data._EofInput)
    {
      isOk = false;
      token = "";
    }
    else
    {
      isOk = GreedyRead();
      if (isOk)
      {
        token = DequeueTokenOrEmpty();
        isOk = true;
      }
      else
      {
        _Data._EofInput = true;
        isOk = false;
        token = "";
      }
    }
    return isOk;
  }

  private static char[] EmptyCharArray() => new char[] {};

  /// <summary>
  /// Read until next token starts.
  /// </summary>
  /// <returns>One or more lines of text</returns>
  private bool GreedyRead()
  {
    bool isTextRead = false;
    StringBuilder builder = new StringBuilder();

    char charInput;
    int tokenCount = 0;
    string inputLine;

    if (! _Data._EofInput)
    {
      do
      {
        if ( _Data._UnprocessedIndex >= _Data._UnprocessedText.Length )
        {
          if (builder.Length > 0)
          {
            string segment = builder.ToString();
            TokeniseLineParts(segment);
            isTextRead = true;
            builder.Clear();
            tokenCount = 0;
          }
          if (TryReadInput(out inputLine))
          {
            _Data._UnprocessedText = inputLine.ToCharArray();
            ApplyPreprocessors();
          }
          else
          {
            _Data._UnprocessedText = EmptyCharArray();
          }
          _Data._UnprocessedIndex = 0;
        }

        if (_Data._UnprocessedIndex < _Data._UnprocessedText.Length)
        {
          charInput = _Data._UnprocessedText[_Data._UnprocessedIndex];
          tokenCount = ((char)charInput) == '<' ? tokenCount + 1 : tokenCount;

          if (tokenCount < 2)
          {
            builder.Append(charInput);
            _Data._UnprocessedIndex++;
          }
          else
          {
            string segment = builder.ToString();
            TokeniseLineParts(segment);
            isTextRead = true;
            builder.Clear();
            tokenCount = charInput == '<' ? 1 : 0;
          }
        }

      } while(! _Data._EofInput && ! isTextRead);
    }

    return isTextRead;
  }

  private void ApplyPreprocessors()
  {
    char[] text = _Data._UnprocessedText;
    char[] nextText;

    bool isUpdated = false;
    _Data._Preprocessors.ForEach( preproc => {
      if (preproc.IsMatch(text))
      {
        isUpdated = preproc.ProcessText(text, out nextText);
        text = nextText;
      }
    });
    if (isUpdated)
      _Data._UnprocessedText = text;
  }

  private bool TryReadInput(out string textLine)
  {
    string? inputLine = _Data._Input.ReadLine();
    _Data._EofInput = inputLine == null ? true : _Data._EofInput;
    textLine = _Data._EofInput
      ? ""
      : inputLine + "\n";
    return inputLine != null;
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
      _Data._InputQueue.Enqueue(sub);
    }
  }

  private string DequeueTokenOrEmpty()
  {
    string value = _Data._InputQueue.Count > 0
      ? _Data._InputQueue.Dequeue()
      : "";
    return value;
  }
}
