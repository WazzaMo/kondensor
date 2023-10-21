/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


namespace kondensor.Pipes;

internal struct HtmlPipeCheckPoint : IPipeCheckPoint
{
  internal int _QueueIndex;

  internal HtmlPipeCheckPoint(ref HtmlContext context)
  {
    _QueueIndex = context._QueueIndex;
  }

  internal void restoreTo(ref HtmlContext context)
  {
    context._QueueIndex = _QueueIndex;
  }
}
