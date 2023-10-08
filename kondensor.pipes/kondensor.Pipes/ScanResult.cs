/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

namespace kondensor.Pipes;

public struct ScanResult
{
  public const int IDX_NO_MATCH = -1;

  public bool IsMatched;
  public int Index;

  public ScanResult()
  {
    IsMatched = false;
    Index = IDX_NO_MATCH;
  }
}
