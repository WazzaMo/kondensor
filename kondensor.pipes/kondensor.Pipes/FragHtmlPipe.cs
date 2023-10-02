/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace kondensor.Pipes;


public struct FragHtmlPipe : IPipe, IPipeWriter
{
  internal struct FragData
  {
    internal TextReader _Input;
    internal TextPipeWriter _Output;
    internal char[] _Buffer;
    internal int _BufferIndex;
    internal bool _EoInput;
    internal int _NumTagStart;
    internal int _NumTagEnd;
    internal List<IPreprocessor> _Preprocessors;
  }

  private FragData _Data;

  public FragHtmlPipe(
    TextReader input,
    TextPipeWriter output
  )
  {
    _Data = new FragData() {
      _Input = input,
      _Output = output,
      _Buffer = BufferUtils.EmptyBuffer,
      _BufferIndex = 0,
      _EoInput = false,
      _NumTagStart = 0,
      _NumTagEnd = 0,
      _Preprocessors = new List<IPreprocessor>()
    };
  }

  public bool IsInFlowEnded => throw new NotImplementedException();

  public void AddPreprocessor(IPreprocessor processor)
  {
    if (processor != null)
      _Data._Preprocessors.Add(processor);
  }

  public void ClosePipe()
  {
    _Data._Output.ClosePipe();
    _Data._Input.Close();
  }

  public bool IsLineTerminated()
    => _Data._Output.IsLineTerminated();

  public bool IsPipeOpen()
    => ! _Data._EoInput && _Data._Output.IsPipeOpen();

  public bool ReadToken(out string token)
    => IsTokenFound(out token);

  public IPipeWriter WriteFragment(string fragment)
    => _Data._Output.WriteFragment(fragment);

  public IPipeWriter WriteFragmentLine(string fragment)
    => _Data._Output.WriteFragmentLine(fragment);
  
  internal bool IsInTag => FragDataOps.IsInTag(ref _Data);
  internal bool IsBetweenTags => FragDataOps.IsBetweenTags(ref _Data);

  private const string EMPTY = "";

  private bool IsTokenFound(out string token)
  {
    bool isFound = false;

    int tokenEnd;
    int tokenStart = GetTokenStart();
    if (! _Data._EoInput && tokenStart >= 0)
    {
      tokenEnd = FragDataOps.IsSingleCharFragment(ref _Data)
        ? tokenStart + 1
        : BufferUtils.ScanForEndOfSymbol(
            IsFragment, IsFragmentEnd, _Data._Buffer, tokenStart
          );
      if ( BufferUtils.IsValidIndex(_Data._Buffer, tokenEnd-1) )
      {
        int length = tokenEnd - tokenStart;
        _Data._BufferIndex = tokenEnd;
        Span<char> buffer = new Span<char>(_Data._Buffer);
        Span<char> tokenBuffer = buffer.Slice(tokenStart, length);
        token = tokenBuffer.ToString();
        isFound = true;
      }
      else
        token = EMPTY;
    }
    else
      token = EMPTY;
    return isFound;
  }

  private int GetTokenStart()
  {
    int index;
    if (IsBetweenTags)
      index = _Data._BufferIndex;
    else
    {
      index = BufferUtils.ScanForSymbolStart(
        IsFragmentSpace,
        _Data._Buffer,
        _Data._BufferIndex
      );
    }

    while (
        FragDataOps.NeedNewBuffer(ref _Data)
        || ! BufferUtils.IsValidIndex(_Data._Buffer, index)
      )
      {
        FragDataOps.GetNewBuffer(ref _Data);
        index = BufferUtils.ScanForSymbolStart(
          IsFragmentSpace,
          _Data._Buffer,
          _Data._BufferIndex
        );
        _Data._BufferIndex = index;
      }

    return index;
  }

  private bool IsFragmentSpace(char _char)
    => FragDataOps.IsFragmentSpace(ref _Data, _char);

  private bool IsFragment(char _char)
    => FragDataOps.IsFragment(ref _Data, _char);
  
  private bool IsFragmentEnd(char _char)
    => FragDataOps.IsFragmentEnd(ref _Data, _char);
}
