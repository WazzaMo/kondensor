/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace kondensor.Pipes;


public struct FragHtmlPipe : IPipe, IPipeWriter
{
  internal struct Data
  {
    internal TextReader _Input;
    internal TextPipeWriter _Output;
    internal char[] _Buffer;
    internal int _BufferIndex;
    internal bool _EoInput;
  }

  private Data _Data;

  public FragHtmlPipe(
    TextReader input,
    TextPipeWriter output
  )
  {
    _Data = new Data() {
      _Input = input,
      _Output = output,
      _Buffer = BufferUtils.EmptyBuffer,
      _BufferIndex = 0,
      _EoInput = false
    };
  }

  public bool IsInFlowEnded => throw new NotImplementedException();

  public void AddPreprocessor(IPreprocessor processor)
  {
    throw new NotImplementedException();
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
  
  private const string EMPTY = "";

  private bool IsTokenFound(out string token)
  {
    bool isFound = false;

    int tokenEnd;
    int tokenStart = GetTokenStart();
    if (! _Data._EoInput && tokenStart >= 0)
    {
      tokenEnd = IsSingleCharFragment()
        ? tokenStart + 1
        : BufferUtils.ScanForEndOfSymbol(IsFragment, _Data._Buffer, tokenStart);
      if ( BufferUtils.IsValidIndex(_Data._Buffer, tokenEnd) )
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
    int index = BufferUtils.ScanForSymbolStart(
      IsFragmentSpace,
      _Data._Buffer,
      _Data._BufferIndex
    );

   while (
      NeedNewBuffer()
      || ! BufferUtils.IsValidIndex(_Data._Buffer, index)
    )
    {
      GetNewBuffer();
      index = BufferUtils.ScanForSymbolStart(
        IsFragmentSpace,
        _Data._Buffer,
        _Data._BufferIndex
      );
      _Data._BufferIndex = index;
    }

    return index;
  }

  private bool NeedNewBuffer()
    => ! _Data._EoInput
    && ! BufferUtils.IsValidIndex( _Data._Buffer, _Data._BufferIndex );
  
  private void GetNewBuffer()
  {
    var input = _Data._Input.ReadLine();
    if (input == null)
    {
      _Data._EoInput = true;
    }
    _Data._Buffer = BufferUtils.GetWhitespaceTerminatedBufferFromString(input);
    _Data._BufferIndex = 0;
  }

  private bool IsFragmentSpace(char _char)
  => Char.IsWhiteSpace(_char);

  private bool IsFragmentLone(char _char)
  => _char == '>';

  private bool IsFragment(char _char)
  => Char.IsPunctuation(_char)
    || Char.IsLetterOrDigit(_char)
    || _char == '<'
    || _char == '=';

  private bool IsSingleCharFragment()
  => BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex)
    && IsFragmentLone(_Data._Buffer[_Data._BufferIndex]);
}
