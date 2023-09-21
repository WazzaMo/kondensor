/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Represents a fixed region specification
  /// using one of the <see cref="RegionId"/> enum
  /// values.
  /// </summary>
  public struct Region : IPrimitive
  {
    private RegionId _RegionId;

    public void Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: {RegionString()}", indent);

    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix}: {RegionString()}", indent);
    
    private string RegionString() => Regions.AsText(_RegionId).Value;

    public Region(RegionId region)
      => _RegionId = region;
  }


}