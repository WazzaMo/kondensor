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

  public struct VpcOutput : IOutput
  {
    private Text _LogId;
    private Option<Text> _Description;
    private Option<Text> _Condition;
    private Ref _Value;
    private IExport _Export; 

    public struct VpcExport : IExport
    {
      private string _EnvName;
      private string _VpcName;

      public string ExportValue => $"{_EnvName}-{_VpcName}";

      public VpcExport(string environment, string vpcName)
      {
        _EnvName = environment;
        _VpcName = vpcName;
      }

      public void Write(StreamWriter output, string indent)
      {
        string _1_indent = indent + YamlWriter.INDENT;
        YamlWriter.Write(output, message: "Export:", indent);
        YamlWriter.Write(output, message: $"Name: {ExportValue}", _1_indent);
      }
    }// vpcExport

    public Text LogicalId => _LogId;

    public Option<Text> Description => _Description;

    public Option<Text> Condition => _Condition;

    public Ref Value => _Value;

    public IExport Export => _Export;

    public void SetDescription(string description)
      => _Description = Option.Some(new Text(description));
    
    public void SetCondition(string condition)
      => _Condition = Option.Some(new Text(condition));

    public VpcOutput(string environment, string vpcName)
    {
      _LogId = new Text($"Vpc{vpcName}");
      _Description = Option.None<Text>();
      _Condition = Option.None<Text>();
      _Value = new Ref(vpcName);
      _Export = new VpcExport(environment, vpcName);
    }

    public void Write(StreamWriter output, string indent)
    {
      string  _0_ident = indent,
              _1_indent = _0_ident + YamlWriter.INDENT;
      YamlWriter.Write(output, message: $"{_LogId.Value}:", _0_ident);
      _Description.MatchSome( desc => 
        YamlWriter.Write(output, message: $"Description: {desc.Value}", _1_indent)
      );
      _Condition.MatchSome( cond => 
        YamlWriter.Write(output, message: $"Condition: {cond.Value}", _1_indent)
      );
      _Value.WritePrefixed(output, "Value:", _1_indent);
      // YamlWriter.Write(output, message: $"Value: {_Value.Value}", _1_indent);
      _Export.Write(output, _1_indent);
    }
  }

}