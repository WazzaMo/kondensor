/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Common interface for primitives that can be
  /// tagged.
  /// </summary>
  public interface IHasTags
  {
    void AddTag(string key, string value);
  }

}