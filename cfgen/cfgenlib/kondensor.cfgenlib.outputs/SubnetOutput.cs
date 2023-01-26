/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.writer;
using Optional;

namespace kondensor.cfgenlib.outputs
{

  public struct SubnetOutput : IOutput
  {
    private OutputData _Output;

    public Text LogicalId => _Output.LogicalId;

    public Option<Text> Description => _Output.Description;

    public Option<Text> Condition => _Output.Condition;

    public Ref Value => _Output.Value;

    public IExport Export => _Output.Export;

    public void SetDescription(string description)
      => _Output.SetDescription(description);
    
    public void SetCondition(string condition)
      => _Output.SetCondition(condition);

    public void Write(StreamWriter output, string indent)
      => _Output.Write(output, indent);

    private static readonly string TYPE = "Sub";

    public SubnetOutput(string environment, string subnetName)
    {
      var export = new ExportData(environment,TYPE, subnetName);
      _Output = new OutputData(TYPE, subnetName, export);
      _Output.SetValue(new Ref(subnetName));
    }
  }

}