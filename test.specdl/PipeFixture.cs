
using Parser;

using System;

namespace test;


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