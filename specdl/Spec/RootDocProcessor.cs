/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;

using kondensor.Pipes;
using kondensor.Parser;

using RootDoc;

namespace Spec;

public struct RootDocProcessor : IProcessor
{
  private RootDocList _RootList;

  public RootDocProcessor()
  {
    _RootList = new RootDocList();
  }

  public IEnumerator<SubDoc> GetEnumerator() => _RootList.EnumerateDocs();

  public void ProcessAllLines(string url, ReplayWrapPipe pipe)
  {
    var parser = Parsing.Group(pipe)
      .Expect(_RootList.ParseRootDoc)
      ;
  }

  public void WriteOutput(ReplayWrapPipe pipe)
  {}
}