/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.policy;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Represents an IAM policy document as a cloudformation
  /// template primitive value to be written as YAML.
  /// </summary>
  public struct PolicyDocValue : IPrimitive
  {
    const string POLICY_KEY = "PolicyDocument",
        VERSION_KEY = "Version",
        VERSION_VALUE = PolicyDocument.IAM_POLICY_VERSION,
        STATEMENT_KEY = "Statement",
        SID_KEY = "Sid",
        ACTION_KEY = "Action",
        EFFECT_KEY = "Effect",
        PRINCIPAL_KEY = "Principal",
        RESOURCE_KEY = "Resource",
        CONDITION_KEY = "Condition";

    private PolicyDocument _Policy;
    
    public void Write(StreamWriter output, string name, string indent)
    {
      string
        indent_0 = indent,
        indent_1 = YamlWriter.INDENT + indent_0,
        indent_2 = YamlWriter.INDENT + indent_1;

      YamlWriter.Write(output, POLICY_KEY+":", indent_0);
      YamlWriter.Write(output, $"{VERSION_KEY}: {VERSION_VALUE}", indent_1);
      if (_Policy.Statements == null || _Policy.Statements.Count == 0)
      {
        YamlWriter.WriteComment(output, comment: "No policy statements to write.", indent_1);
      }
      else
      {

      }
    }

    public void WritePrefixed(StreamWriter output, string prefix, string indent)
    {
      throw new NotImplementedException();
    }

    public PolicyDocValue(PolicyDocument document)
    {
      _Policy = document;
    }

    private void WriteStatement(StreamWriter output, PolicyStatement statement, string indent)
    {
      string
        _indent_0 = indent,
        _indent_1 = _indent_0 + YamlWriter.INDENT,
        _indent_2 = _indent_1 + YamlWriter.INDENT,
        _indent_3 = _indent_2 + YamlWriter.INDENT;
      
      YamlWriter.Write(output, STATEMENT_KEY + ":", _indent_0);
      if (statement.Sid.HasValue )
      {
        statement.Sid.MatchSome( sid => YamlWriter.Write(output, message: $"- {SID_KEY}: {sid}", _indent_1));
        YamlWriter.Write(output, message: $"{ACTION_KEY}:", _indent_2);
      }
      else
        YamlWriter.Write(output, message: $"- {ACTION_KEY}:", _indent_1);
      statement.Actions.ForEach(
        actionTxt => YamlWriter.Write(output, message: $"- {actionTxt}", _indent_3)
      );
    }
  }

}