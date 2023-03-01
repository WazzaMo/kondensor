/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

namespace kondensor.cfgenlib.policy
{

  public struct PolicyDocument
  {
    internal const string
      IAM_POLICY_VERSION = "2012-10-17",
      KW_SID = "Sid",
      KW_EFFECT = "Effect",
      KW_PRINCIPAL = "Principal",
      KW_ACTION = "Action",
      KW_CONDITION = "Condition";

    private List<PolicyStatement> _Statements;

    public List<PolicyStatement> Statements => _Statements;

    public PolicyDocument AddStatement(PolicyStatement statement)
    {
      _Statements.Add(statement);
      return this;
    }

    public PolicyDocument()
    {
      _Statements = new List<PolicyStatement>();
    }
  }

}