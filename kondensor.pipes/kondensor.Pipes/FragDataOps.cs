/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Text;

namespace kondensor.Pipes;

internal static class FragDataOps
{
  internal static bool NeedNewBuffer(ref FragHtmlPipe.FragData _Data)
    => !_Data._EoInput
    && !BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex);

  internal static void GetNewBuffer(ref FragHtmlPipe.FragData _Data)
  {
    var input = _Data._Input.ReadLine();
    if (input == null)
    {
      _Data._EoInput = true;
      _Data._Buffer = BufferUtils.EmptyBuffer;
    }
    else
    {
      char[] buffer = BufferUtils.GetWhitespaceTerminatedBufferFromString(input);
      PreprocessPipeUtils.TryApplyPreprocessors(
        _Data._Preprocessors,
        buffer,
        out _Data._Buffer
      );
    }
    _Data._BufferIndex = 0;
  }

  internal static bool TryScan(
    ref FragHtmlPipe.FragData _Data,
    char[] search,
    out int matchIndex
  )
  {
    bool isFound = false;
    Span<char> scanFor = new Span<char>(search);
    Span<char> buffer;

    matchIndex = 0;
    while(! isFound && ! _Data._EoInput)
    {
      if (NeedNewBuffer(ref _Data))
      {
        GetNewBuffer(ref _Data);
      }
      if (! _Data._EoInput && _Data._Buffer.Length > 0)
      {
        buffer = new Span<char>(_Data._Buffer);
        isFound = PreprocessorUtils.FindNextMatch(
          buffer,
          scanFor,
          _Data._BufferIndex,
          out matchIndex
        );
        if (! isFound)
          _Data._BufferIndex = _Data._Buffer.Length;
      }
    }
    if (isFound)
      _Data._BufferIndex = matchIndex;
    return isFound;
  }

  internal static bool IsFragmentSpace(ref FragHtmlPipe.FragData _Data, char _char)
  {
    bool result = Char.IsWhiteSpace(_char);
    DoStartEndTracking(ref _Data, _char);

    return result;
  }

  internal static bool IsFragmentLone(ref FragHtmlPipe.FragData _Data, char _char)
  {
    bool result = _char == '>';
    DoStartEndTracking(ref _Data, _char);
    return result;
  }

  internal static bool IsFragment(ref FragHtmlPipe.FragData _Data, char _char)
  {
    bool result;
    
    if (IsInTag(ref _Data))
    {
      result = Char.IsPunctuation(_char)
        || Char.IsLetterOrDigit(_char)
        || _char == '<'
        || _char == '=';
    }
    else
    {
      result = _char != '<';
    }
    DoStartEndTracking(ref _Data, _char);
    return result;
  }

  internal static bool IsFragmentEnd(ref FragHtmlPipe.FragData _Data, char _char)
  {
    bool result = ( IsInTag(ref _Data) && Char.IsWhiteSpace(_char) )
      || _char == '>'
      || _char == '<';
    
    DoStartEndTracking(ref _Data, _char);

    return result;
  }

  internal static void DoStartEndTracking(ref FragHtmlPipe.FragData _Data, char _char)
  {
    if (_char == '<')
      _Data._NumTagStart += 1;
    else if (_char == '>')
      _Data._NumTagEnd += 1;
  }

  internal static bool IsSingleCharFragment(ref FragHtmlPipe.FragData _Data)
  => BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex)
    && IsFragmentLone(ref _Data, _Data._Buffer[_Data._BufferIndex]);

  internal static bool IsInTag(ref FragHtmlPipe.FragData _Data)
    => _Data._NumTagStart == (_Data._NumTagEnd + 1);

  internal static bool IsBetweenTags(ref FragHtmlPipe.FragData _Data)
    => _Data._NumTagStart == _Data._NumTagEnd;
}
