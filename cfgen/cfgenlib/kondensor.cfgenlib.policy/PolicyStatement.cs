/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using Optional;

namespace kondensor.cfgenlib.policy
{

  /// <summary>
  /// IAM policy document type.
  /// 
  /// <see href="https://docs.aws.amazon.com/IAM/latest/UserGuide/access_policies.html#policies_session"/>
  /// </summary>
  public struct PolicyStatement
  {
    public Option<string> Sid;
    public EffectValue Effect;
    public Option<string> Principle;

    public List<string> Actions;

    public List<string> Resources;
  }

}