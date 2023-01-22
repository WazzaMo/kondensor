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

    public struct SubnetExport : IExport
    {
      private string _Environment;
      private string _SubnetName;

      public string ExportValue => $"{_Environment}-{_SubnetName}";

      public void Write(StreamWriter output, string indent)
      {
        throw new NotImplementedException();
      }

      public SubnetExport(string environment, string subnetName)
      {
        //
      }
    }

    public Text LogicalId => _Output.LogicalId;

    public Option<Text> Description => _Output.Description;

    public Option<Text> Condition => _Output.Condition;

    public Text Value => _Output.Value;

    public Option<IExport> Export => _Output.Export;

    public void Write(StreamWriter output, string indent)
      => _Output.Write(output, indent);

    private static readonly string TYPE = "Sub";

    public SubnetOutput(string environment, string subnetName)
    {
      _Output = new OutputData(TYPE, subnetName);
    }
  }

}