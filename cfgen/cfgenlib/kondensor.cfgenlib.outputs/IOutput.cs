/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.IO;

using Optional;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.outputs
{

  public interface IOutput
  {
    Text LogicalId { get; }
    Option<Text> Description { get; }
    Option<Text> Condition { get; }
    Text Value { get; }
    IExport Export { get; }

    void Write( StreamWriter output, string indent);

    void SetDescription(string description);
    void SetCondition(string condition);
  }

}