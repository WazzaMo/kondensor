/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace kondensor.Pipes;

internal struct ReplayWrapCheckPoint : IPipeCheckPoint
{
  internal int TokenHistoryIndex;
  internal IPipeCheckPoint _BasePipeCheckPoint;

  internal ReplayWrapCheckPoint(int index, IPipe basePipe)
  {
    TokenHistoryIndex = index;
    if (basePipe.IsCheckPointingSupported)
      _BasePipeCheckPoint = basePipe.GetCheckPoint();
    else
      throw new ArgumentException("Base pipe does not support checkpointing!");
  }
}