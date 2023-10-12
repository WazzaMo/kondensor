/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using kondensor.Pipes;

using test.kondensor.fixtures;

using System;

namespace test.kondensor.pipes;


public class PipeFixture_FragHtmlPipe : IDisposable
{
  private FragHtmlPipe _RootPipe;
  private ReplayWrapPipe _Subject;

  public PipeFixture_FragHtmlPipe()
  {
    TextPipeWriter writer = new TextPipeWriter(Console.Out);
    _RootPipe = new FragHtmlPipe(PipeValues.HTML, writer);
    _Subject = new ReplayWrapPipe(_RootPipe);
  }

  public FragHtmlPipe RootPipe => _RootPipe;

  public ReplayWrapPipe Subject => _Subject;

  public void Dispose()
  {
    _RootPipe.ClosePipe();
    _Subject.ClosePipe();
  }
}