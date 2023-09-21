/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
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
