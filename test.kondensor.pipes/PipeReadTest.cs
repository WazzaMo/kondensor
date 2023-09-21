/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using Xunit;

using System.IO;

using kondensor.Pipes;


namespace test.kondensor.YamlFormat;

public class PipeReadTest
{
  private IPipe _Pipe;
  private string _Input = @"
        Multi-line input for processing.
        Each new line is a terminator for a token.
    ";

  public PipeReadTest()
  {
    _Pipe = new TextPipe(writer: null, reader: new StringReader(_Input));
  }

  [Fact]
  public void reading_from_pipe_shows_4_tokens_then_eof()
  {
    string token;
    Assert.True(_Pipe.ReadToken(out token));
    Assert.Equal(expected: "", token);
    Assert.True(_Pipe.ReadToken(out token));
    Assert.Equal(expected: "        Multi-line input for processing.", token);
    Assert.True(_Pipe.ReadToken(out token));
    Assert.Equal(expected: "        Each new line is a terminator for a token.", token);
    Assert.True(_Pipe.ReadToken(out token));
    Assert.Equal(expected: "    ", token);
    Assert.False(_Pipe.ReadToken(out token));
    Assert.True(_Pipe.IsInFlowEnded);
  }
}