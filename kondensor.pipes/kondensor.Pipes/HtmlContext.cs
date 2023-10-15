/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */



namespace kondensor.Pipes;

/// <summary>
/// Context for <see cref="HtmlPipe"/>
/// </summary>
internal struct HtmlContext
{
  internal TextReader _Input;
  internal List<string> _InputQueue;
  internal int _QueueIndex;
  internal TextPipeWriter _Output;
  internal bool _IsOpen;
  internal bool _EofInput;
  internal char[] _UnprocessedText;
  internal int _UnprocessedIndex;
  internal List<IPreprocessor> _Preprocessors;

  internal bool IsLineTerminated() => _Output.IsLineTerminated();

  internal HtmlContext(TextReader input, TextWriter output)
  {
    _Input = input;
    _InputQueue = new List<string>();
    _QueueIndex = 0;
    _UnprocessedText = BufferUtils.EmptyBuffer;
    _UnprocessedIndex = 0;
    _Output = new TextPipeWriter(output);
    _IsOpen = true;
    _EofInput = false;
    _Preprocessors = new List<IPreprocessor>();
  }
}
