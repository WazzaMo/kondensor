/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.policy;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.composites
{

  /// <summary>
  /// Represents an IAM policy document as a cloudformation
  /// template primitive value to be written as YAML.
  /// </summary>
  public struct PolicyDocValue : IComposite
  {
    const string
        POLICY_KEY = "PolicyDocument",
        POLICY_NAME = "PolicyName",
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
      Text policyName = new Text(_Policy.PolicyName);
      policyName.WritePrefixed(output, POLICY_NAME+":", indent);
      WriteDocumentDetails(output, indent);
    }

    /// <summary>
    /// For use where policy document is provided as a sub-value
    /// and so the first indent is to be prefixed by '-' and then
    /// follows with regular document.
    /// 
    /// - PolicyName: giveaccesstotopiconly
    ///   PolicyDocument:
    ///     Version: '2012-10-17'
    ///     Statement:
    ///     - Effect: Allow
    /// </summary>
    /// <param name="output">Object to write into</param>
    /// <param name="prefix">Prefix characters</param>
    /// <param name="indent">Indent required before the prefix</param>
    public void WritePrefixed(StreamWriter output, string prefix, string indent)
    {
      string formattedPrefix = $"{prefix} {POLICY_NAME}:";
      Text policyName = new Text(_Policy.PolicyName);

      policyName.WritePrefixed(output, formattedPrefix, indent);
      WriteDocumentDetails(output, indent);
    }

    public void SetPolicyName(string name)
      => _Policy.SetPolicyName(name);

    /// <summary>
    /// Best way to create the policy document.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="statements"></param>
    /// <returns></returns>
    public static PolicyDocValue Create(string name, params Func<PolicyStatement, PolicyStatement>[] statements)
    {
      PolicyDocument document = new PolicyDocument(name);
      for(int index = 0; index < statements.Length; index++) 
      {
        PolicyStatement policyStatement = new PolicyStatement();
        policyStatement = statements[index](policyStatement);
        document.AddStatement(policyStatement);
      }
      return new PolicyDocValue(document);
    }

    public PolicyDocValue(PolicyDocument document)
    {
      _Policy = document;
    }

    private void WriteDocumentDetails(StreamWriter output, string _0_indent)
    {
      string _1_indent = YamlWriter.INDENT + _0_indent;

      YamlWriter.Write(output, POLICY_KEY+":", _0_indent);
      YamlWriter.Write(output, $"{VERSION_KEY}: {VERSION_VALUE}", _1_indent);
      if (_Policy.Statements == null || _Policy.Statements.Count == 0)
      {
        YamlWriter.WriteComment(output, comment: "No policy statements to write.", _1_indent);
      }
      else
      {
        _Policy.Statements.ForEach( statement => PolicyDocValue.WriteStatement(output, statement, _1_indent));
      }
    }

    private static void WriteStatement(StreamWriter output, PolicyStatement statement, string indent)
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
        YamlWriter.Write(output, message: $"{EFFECT_KEY}: {Effect(statement.Effect)}", _indent_2);
      }
      else
      {
        YamlWriter.Write(output, message: $"- {EFFECT_KEY}: {Effect(statement.Effect)}", _indent_1);
      }

      statement.Principal.MatchSome( principal => YamlWriter.Write(output, message: $"{PRINCIPAL_KEY}: {principal}", _indent_1));
      YamlWriter.Write(output, message: $"{ACTION_KEY}:", _indent_2);
      statement.Actions.ForEach(
        actionTxt => YamlWriter.Write(output, message: $"- {actionTxt}", _indent_3)
      );
      YamlWriter.Write(output, message: $"{RESOURCE_KEY}:", _indent_2);
      statement.Resources.ForEach(
        resource => YamlWriter.Write(output, message: $"- {resource}", _indent_3)
      );
    }

    private static string Effect(EffectValue effect)
    {
      return  effect switch {
        EffectValue.Allow => "Allow",
        EffectValue.Deny => "Deny",
        _ => "Deny # Effect not initialised to correct value."
      };
    }

  }

}