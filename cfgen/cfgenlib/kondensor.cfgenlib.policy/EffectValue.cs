/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

namespace kondensor.cfgenlib.policy
{

  /// <summary>
  /// Indicate whether policy allows or denies access.
  /// </summary>
  public enum EffectValue
  {
    /// <summary>Unused. Illegal value.</summary>
    Empty,
    Allow,
    Deny
  }

}
