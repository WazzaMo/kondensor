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
  internal static bool NeedNewBuffer(ref FragContext _Data)
    => !_Data._EoInput
    && !BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex);

  internal static void GetNewBuffer(ref FragContext _Data)
  {
    var input = _Data._Input.ReadLine();
    if (input == null)
    {
      _Data._EoInput = true;
      _Data._Buffer = BufferUtils.EmptyBuffer;
    }
    else
    {
      char[] buffer = BufferUtils.GetBufferFromString(input);
      PreprocessPipeUtils.TryApplyPreprocessors(
        _Data._Preprocessors,
        buffer,
        out _Data._Buffer
      );
    }
    _Data._BufferIndex = 0;
  }

  internal static bool TryScan(
    ref FragContext _Data,
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

  internal static int GetTokenStart(ref FragContext _Data)
  {
    int index;

    if (FragmentHtml.IsBetweenTags(ref _Data))
      index = _Data._BufferIndex;
    else
    {
      index = BufferUtils.ScanForSymbolStart( ref _Data, FragmentHtml.IsFragmentSpace,
        _Data._Buffer, _Data._BufferIndex
      );
    }

    while (
      FragDataOps.NeedNewBuffer(ref _Data)
      || !BufferUtils.IsValidIndex(_Data._Buffer, index)
    )
    {
      FragDataOps.GetNewBuffer(ref _Data);
      index = BufferUtils.ScanForSymbolStart( ref _Data,
        FragmentHtml.IsFragmentSpace,
        _Data._Buffer, _Data._BufferIndex
      );
      _Data._BufferIndex = index;
    }

    return index;
  }

  internal static bool GetTokenFromStartIndex(
    ref FragContext _Data,
    int tokenStartIndex,
    out string token
  )
  {
    bool isFound = false;

    int tokenEnd;
    if (!_Data._EoInput && tokenStartIndex >= 0)
    {
      tokenEnd = FragmentHtml.IsSingleCharFragment(ref _Data)
        ? tokenStartIndex + 1
        : BufferUtils.ScanForEndOfSymbol(
            ref _Data,
            FragmentHtml.IsFragment, FragmentHtml.IsFragmentEnd,
            _Data._Buffer, tokenStartIndex
          );
      if (BufferUtils.IsValidIndex(_Data._Buffer, tokenEnd - 1))
      {
        int length = tokenEnd - tokenStartIndex;
        _Data._BufferIndex = tokenEnd;
        Span<char> buffer = new Span<char>(_Data._Buffer);
        Span<char> tokenBuffer = buffer.Slice(tokenStartIndex, length);
        token = tokenBuffer.ToString();
        isFound = true;
      }
      else
        token = FragHtmlPipe.EMPTY;
    }
    else
      token = FragHtmlPipe.EMPTY;
    
    return isFound;
  }
}
