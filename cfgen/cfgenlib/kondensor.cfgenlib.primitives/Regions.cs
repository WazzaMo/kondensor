/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */


namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Utilities for working with Regions.
  /// </summary>
  public static class Regions
  {
    /// <summary>
    /// Give <see cref="Text"/> value of region.
    /// </summary>
    /// <param name="region">Region to convert</param>
    /// <returns>Text conversion.</returns>
    public static Text AsText(RegionId region)
    {
      const string FORMAT = "G";
      const char
        enumSep = '_',
        apiSep = '-';

      string apiRegion = Enum.Format(typeof(RegionId),region, FORMAT);
      apiRegion = apiRegion.Replace(enumSep,apiSep);
      return new Text(apiRegion);
    }

    /// <summary>
    /// Returns Ref to the region where the stack
    /// is used to provision infrastructure.
    /// </summary>
    /// <returns><see cref="Ref"/> to region</returns>
    public static Ref CurrentRegion()
    {
      const string PSEUDO_REF_REGION = "AWS::Region";
      return new Ref(PSEUDO_REF_REGION);
    }
  }

}