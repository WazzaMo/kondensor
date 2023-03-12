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

  public struct OutputData : IOutput
  {
    private Option<Text> _Description;
    private IExport _Export;
    private Option<Text> _Condition;
    private Option<Ref> _Value;

    private string _Environment;
    private IResourceType _Resource;

    public Text LogicalId => new Text(ExportUtils.OutputIdFor(_Environment, _Resource) );

    public Option<Text> Description => _Description;

    public Option<Text> Condition => _Condition;

    public Ref Value => _Value.Match<Ref>
    (
      some: (value) => value,
      none: () => new Ref("")
    );

    public IExport Export => _Export;

    public void Write(ITextStream output, string indent)
    {
      string  _0_ident = indent,
              _1_indent = _0_ident + YamlWriter.INDENT;
      
      YamlWriter.Write(
        output,
        message: $"{ExportUtils.OutputIdFor(_Environment, _Resource)}:", _0_ident
      );
      _Description.MatchSome( desc => 
        YamlWriter.Write(output, message: $"Description: {desc.Value}", _1_indent)
      );
      _Condition.MatchSome( cond => 
        YamlWriter.Write(output, message: $"Condition: {cond.Value}", _1_indent)
      );
      _Value.MatchSome( value =>
        value.WritePrefixed(output, prefix: "Value:", _1_indent)
      );
      _Export.Write(output, _1_indent);
    }

    public void SetDescription(string description)
      => _Description = Option.Some<Text>(new Text(description));
    
    public void SetCondition(string condition)
      => _Condition = Option.Some<Text>(value: new Text(condition));
        
    public OutputData(string environment, IResourceType resource)
    {
      _Resource = resource;
      _Environment = environment;
      _Export = new ExportData(environment, resource);
      _Value = Option.Some( new Ref(resource.Id) );
      _Description = Option.None<Text>();
      _Condition = Option.None<Text>();
    }
  }

}