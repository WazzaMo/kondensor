/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


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
  }

}