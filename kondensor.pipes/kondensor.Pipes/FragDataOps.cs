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
    }
    char[] buffer = BufferUtils.GetWhitespaceTerminatedBufferFromString(input);
    PreprocessPipeUtils.TryApplyPreprocessors(
      _Data._Preprocessors,
      buffer,
      out _Data._Buffer
    );
    _Data._BufferIndex = 0;
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
