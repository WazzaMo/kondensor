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
    private Text _LogId;
    private Option<Text> _Description;
    private Option<IExport> _Export;
    private Option<Text> _Condition;
    private Option<Text> _Value;

    public Text LogicalId => _LogId;

    public Option<Text> Description => throw new NotImplementedException();

    public Option<Text> Condition => _Condition;

    public Text Value => _Value.Match<Text>
    (
      some: (value) => value,
      none: () => new Text("")
    );

    public Option<IExport> Export => _Export;

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
      _Value.MatchSome( value =>
        YamlWriter.Write(output, message: $"Value: {value}", _1_indent)
      );
      _Export.MatchSome( exp => exp.Write(output, _1_indent));
    }

    public void SetDescription(string description)
      => _Description = Option.Some<Text>(new Text(description));
    
    public void SetCondition(string condition)
      => _Condition = Option.Some<Text>(value: new Text(condition));
    
    public void SetValue(string value)
      => _Value = Option.Some<Text>(new Text(value));
    
    public void SetExport(IExport export)
      => _Export = Option.Some<IExport>(export);

    public OutputData(string type, string id)
    {
      _LogId = new Text(text:$"{type}{id}");
      _Description = Option.None<Text>();
      _Condition = Option.None<Text>();
      _Export = Option.None<IExport>();
      _Value = Option.None<Text>();
    }
  }

}