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
    internal TextPipeWriter _Output;
    internal bool _IsOpen;
    internal bool _EofInput;
    internal char[] _UnprocessedText;
    internal int _UnprocessedIndex;
    internal List<IPreprocessor> _Preprocessors;

    internal bool IsLineTerminated() => _Output.IsLineTerminated();

    internal _InternalData( TextReader input, TextWriter output)
    {
      _Input = input;
      _InputQueue = new Queue<string>();
      _UnprocessedText = EmptyCharArray();
      _UnprocessedIndex = 0;
      _Output = new TextPipeWriter( output );
      _IsOpen = true;
      _EofInput = false;
      _Preprocessors = new List<IPreprocessor>();
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
    _Data._Output.ClosePipe();
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
        TryReadInputAndPreprocess();
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
    string input;

    if ( _Data._UnprocessedText != null
      && _Data._UnprocessedText.Length > 0
    )
    {
      input = _Data._UnprocessedText.ToString() ?? "";
      result = rule.Invoke(input);
    }
    while(! result.IsMatched && ! isEof)
    {
      isEof = ! GetTokenFromInput(out input);
      if (! isEof)
        result = rule.Invoke(input);
    }
    return result;
  }

  public void AddPreprocessor(IPreprocessor processor)
  {
    _Data._Preprocessors.Add(processor);
  }

  private int GetBufferEndIndex() => _Data._UnprocessedText.Length;

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
    string segment;

    Func<string> processText = () => {
      var value = builder.ToString();
      builder.Clear();
      isTextRead = true;
      return value;
    };

    if (! _Data._EofInput)
    {
      do
      {
        if ( _Data._UnprocessedIndex >= _Data._UnprocessedText.Length )
        {
          if (! TryReadInputAndPreprocess() )
          {
            _Data._UnprocessedText = EmptyCharArray();
            if (builder.Length > 0)
            {
              segment = processText();
              TokeniseLineParts(segment);
              tokenCount = 0;
            }
          }
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
            segment = processText();
            TokeniseLineParts(segment);
            tokenCount = charInput == '<' ? 1 : 0;
          }
        }

      } while(! _Data._EofInput && ! isTextRead);
    }

    return isTextRead;
  }

  private bool TryReadInputAndPreprocess()
  {
    bool isNotEof = TryReadInput(out string inputLine);

    if (isNotEof)
    {
      _Data._UnprocessedText = inputLine.ToCharArray();
      ApplyPreprocessors();
    }
    _Data._UnprocessedIndex = 0;
    return isNotEof;
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
