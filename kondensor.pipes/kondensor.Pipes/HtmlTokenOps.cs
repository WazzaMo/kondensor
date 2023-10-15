/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.Text;
using System.Text.RegularExpressions;

namespace kondensor.Pipes;

internal static class HtmlTokenOps
{
  internal static char[] EmptyCharArray() => new char[] { };
  private static readonly Regex __LineSep = new Regex(pattern: @"\<");

  internal static bool GetTokenFromInput(
    ref HtmlContext _Data,
    out string token,
    ScanRule? rule = null
  )
  {
    bool isOk;

    if (_Data._EofInput)
    {
      if (HtmlPipeQOps.IsQueueEmpty(ref _Data))
      {
        isOk = false;
        token = "";
      }
      else
      {
        isOk = TryDequeueOptionalScanRule(ref _Data, out token, rule);
      }
    }
    else
    {
      isOk = GreedyRead(ref _Data, rule);
      if (isOk)
      {
        isOk = TryDequeueOptionalScanRule(ref _Data, out token, rule);
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

  /// <summary>
  /// Read until next token starts.
  /// </summary>
  /// <returns>One or more lines of text</returns>
  internal static bool GreedyRead(ref HtmlContext _Data, ScanRule? rule)
  {
    bool isTextRead = false;
    StringBuilder builder = new StringBuilder();

    char charInput;
    int tokenCount = 0;
    string segment;

    Func<string> processText = () =>
    {
      var value = builder.ToString();
      builder.Clear();
      isTextRead = true;
      return value;
    };

    if (!_Data._EofInput)
    {
      do
      {
        if (_Data._UnprocessedIndex >= _Data._UnprocessedText.Length)
        {
          if (!TryReadInputAndPreprocess(ref _Data))
          {
            _Data._UnprocessedText = EmptyCharArray();
            if (builder.Length > 0)
            {
              segment = processText();
              TokeniseLineParts( ref _Data, segment);
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
            TokeniseLineParts(ref _Data, segment);
            tokenCount = charInput == '<' ? 1 : 0;
          }
        }

      } while (!_Data._EofInput && !isTextRead);
    }

    return isTextRead;
  }

  private static void TokeniseLineParts(ref HtmlContext _Data, string line)
  {
    MatchCollection parts = __LineSep.Matches(line);

    string token;

    for (int partIndex = 0; partIndex < parts.Count; partIndex++)
    {
      int index1, index2, length;

      index1 = parts[partIndex].Index;
      index2 = partIndex < (parts.Count - 1)
        ? parts[partIndex + 1].Index
        : line.Length;
      length = index2 - index1;

      token = line.Substring(index1, length);

      EnqueueToken(ref _Data, token);
    }
  }

  private static void EnqueueToken( ref HtmlContext _Data, string segment)
  {
    string trimmedOfSpace = segment.Trim();
    HtmlPipeQOps.Enqueue(ref _Data, trimmedOfSpace);
  }

  internal static bool TryReadInputAndPreprocess(ref HtmlContext _Data)
  {
    bool isNotEof = TryReadInput(ref _Data, out string inputLine);

    if (isNotEof)
    {
      _Data._UnprocessedText = inputLine.ToCharArray();
      ApplyPreprocessors(ref _Data);
    }
    _Data._UnprocessedIndex = 0;
    return isNotEof;
  }

  internal static void ApplyPreprocessors(ref HtmlContext _Data)
  {
    char[] text = _Data._UnprocessedText;
    char[] nextText;

    bool isUpdated = false;
    _Data._Preprocessors.ForEach(preproc =>
    {
      if (preproc.IsMatch(text))
      {
        isUpdated = preproc.ProcessText(text, out nextText);
        text = nextText;
      }
    });
    if (isUpdated)
      _Data._UnprocessedText = text;
  }

  internal static bool TryReadInput(ref HtmlContext _Data, out string textLine)
  {
    string? inputLine = _Data._Input.ReadLine();
    _Data._EofInput = inputLine == null ? true : _Data._EofInput;
    textLine = _Data._EofInput
      ? ""
      : inputLine + "\n";
    return inputLine != null;
  }

  internal static bool TryDequeueOptionalScanRule(
    ref HtmlContext _Data,
    out string token,
    ScanRule? rule
  )
  {
    token = rule == null
      ? DequeueTokenOrEmpty(ref _Data)
      : DequeueTokenOrEmpty(ref _Data, rule);
    return true;
  }

  internal static string DequeueTokenOrEmpty(ref HtmlContext _Data)
  {
    string value =
      HtmlPipeQOps.TryDequeue(ref _Data, out string item)
      ? item : "";

    return value;
  }

  private static string DequeueTokenOrEmpty(ref HtmlContext _Data, ScanRule rule)
  {
    string value = "";

    bool isOk = HtmlPipeQOps.TryDequeueUntilMatch(ref _Data, rule, out value);
    return value;
  }

}
