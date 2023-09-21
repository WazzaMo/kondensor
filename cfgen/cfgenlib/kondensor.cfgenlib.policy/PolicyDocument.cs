/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Collections.Generic;

namespace kondensor.cfgenlib.policy
{

  public struct PolicyDocument
  {
    internal const string IAM_POLICY_VERSION = "2012-10-17";

    public string PolicyName { get; private set; }
    private List<PolicyStatement> _Statements;
    public List<PolicyStatement> Statements => _Statements;

    public PolicyDocument SetPolicyName( string name )
    {
      PolicyName = name;
      return this;
    }

    public PolicyDocument AddStatement(PolicyStatement statement)
    {
      _Statements.Add(statement);
      return this;
    }

    const string NAME_DEFAULT = "defaultPolicyName";

    public PolicyDocument(string policyName = NAME_DEFAULT)
    {
      PolicyName = policyName;
      _Statements = new List<PolicyStatement>();
    }

  }

}