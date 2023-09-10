/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Text;

namespace kondensor.Pipes;


public struct FragHtmlPipe : IPipe, IPipeWriter
{
  internal struct Data
  {
    internal TextReader _Input;
    internal TextPipeWriter _Output;
    internal Queue<string> _Pending;
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
      _Pending = new Queue<string>(),
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
  {
    throw new NotImplementedException();
  }

  public IPipeWriter WriteFragment(string fragment)
    => _Data._Output.WriteFragment(fragment);

  public IPipeWriter WriteFragmentLine(string fragment)
    => _Data._Output.WriteFragmentLine(fragment);
}