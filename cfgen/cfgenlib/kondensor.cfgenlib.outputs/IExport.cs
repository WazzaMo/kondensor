/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.outputs
{

  public interface IExport
  {
    string ExportValue { get; }

    void Write( ITextStream output, string indent);
  }
}