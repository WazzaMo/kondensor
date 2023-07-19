/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using Parser;

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