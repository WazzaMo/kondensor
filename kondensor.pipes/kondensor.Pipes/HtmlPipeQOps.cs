/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System.Collections.Generic;

namespace kondensor.Pipes;

internal static class HtmlPipeQOps
{
  internal static bool IsQueueEmpty(ref HtmlContext _Context)
    => _Context._InputQueue.Count == 0
      || _Context._QueueIndex >= _Context._InputQueue.Count;

  /// <summary>Get next item in queue and advance the queue index.</summary>
  /// <param name="_Context">Context holding queue state.</param>
  /// <param name="value">Value (out) to return.</param>
  /// <returns>True if fetched, false otherwise.</returns>
  internal static bool TryDequeue(ref HtmlContext _Context, out string value)
  {
    bool isOk = ! IsQueueEmpty(ref _Context);
    if (isOk)
    {
      value = _Context._InputQueue[_Context._QueueIndex];
      _Context._QueueIndex = _Context._QueueIndex + 1;
    }
    else
      value = "";
    return isOk;
  }

  internal static bool TryDequeueUntilMatch(
    ref HtmlContext _Context,
    ScanRule rule,
    out string value
  )
  {
    ScanResult scan = new ScanResult();
    bool isNotExhausted = true;
    string token = "";

    while(! scan.IsMatched && isNotExhausted)
    {
      isNotExhausted = TryDequeue(ref _Context, out token);
      scan = rule(token);
    }
    value = token;
    if (scan.IsMatched)
      UndoDequeue(ref _Context);
    return isNotExhausted;
  }

  internal static void Enqueue(ref HtmlContext _Context, string value)
    => _Context._InputQueue.Add(value);
  
  private static void UndoDequeue(ref HtmlContext _Context)
    => _Context._QueueIndex = _Context._QueueIndex -1 ;
}
