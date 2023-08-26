/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using kondensor.Parser;
using kondensor.Pipes;

using System;

namespace test.kondensor.Parser;


public class PipeFixture : IDisposable
{
  private HtmlPipe _RootPipe;
  private ReplayWrapPipe _Subject;

  public PipeFixture()
  {
    _RootPipe = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Subject = new ReplayWrapPipe(_RootPipe);
  }

  public HtmlPipe RootPipe => _RootPipe;

  public ReplayWrapPipe Subject => _Subject;

  public void Dispose()
  {
    _RootPipe.ClosePipe();
    _Subject.ClosePipe();
  }
}