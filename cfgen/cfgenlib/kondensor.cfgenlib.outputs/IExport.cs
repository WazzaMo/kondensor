/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.outputs
{

  public interface IExport
  {
    string ExportValue { get; }

    void Write( StreamWriter output, string indent);
  }
}