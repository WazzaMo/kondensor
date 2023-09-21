/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
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
    Ref Value { get; }
    IExport Export { get; }

    void Write( ITextStream output, string indent);

    void SetDescription(string description);
    void SetCondition(string condition);
  }

}