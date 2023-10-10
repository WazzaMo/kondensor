/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.Collections.Generic;

namespace kondensor.Pipes;

/// <summary>
/// Context used by <see cref="FragHtmlPipe"/> and its peer static class
/// functions, to handle HTML data in fragments.
/// </summary>
internal struct FragContext
{
  internal TextReader _Input;
  internal TextPipeWriter _Output;
  internal char[] _Buffer;
  internal int _BufferIndex;
  internal bool _EoInput;
  internal int _NumTagStart;
  internal int _NumTagEnd;
  internal List<IPreprocessor> _Preprocessors;
  internal Queue<string> _Tokens;

  internal FragContext(
    TextReader input,
    TextPipeWriter output
  )
  {
    _Input = input;
    _Output = output;
    _Buffer = BufferUtils.EmptyBuffer;
    _BufferIndex = 0;
    _EoInput = false;
    _NumTagStart = 0;
    _NumTagEnd = 0;
    _Preprocessors = new List<IPreprocessor>();
    _Tokens = new Queue<string>();
  }
}
