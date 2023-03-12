/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /*
  Get an array of AZs from region.

  Fn::GetAZs: us-east-1

  Use Select with an index to pick one.

  AvailabilityZone: 
        Fn::Select: 
          - 0
          - Fn::GetAZs: $region

  -- Reference to Region
  AvailabilityZone: !Select 
    - 0
    - !GetAZs 
      Ref: 'AWS::Region'
  */

  /// <summary>
  /// Generates template for availability zone function
  /// lookups by region based on.
  /// <see href="https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/intrinsic-function-reference-getavailabilityzones.html"/>
  /// </summary>
  public struct AvailabilityZone : IPrimitive
  {
    private const string
      SELECT = "Fn::Select",
      GETAZS = "Fn::GetAZs";
    private IPrimitive _Region;
    private int _AzIndex;

    public void Write(ITextStream output, string name, string indent)
    {
      string _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT,
        _2_indent = _1_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, $"{name}:", _0_indent);
      YamlWriter.Write(output, $"{SELECT}:", _1_indent);
      YamlWriter.Write(output, $"- {_AzIndex}", _2_indent);
      _Region.WritePrefixed(output, $"- {GETAZS}:", _2_indent);
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      string _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;
      
      YamlWriter.Write(output, $"{prefix} {SELECT}:", _0_indent);
      YamlWriter.Write(output, $"- {_AzIndex}", _1_indent);
      _Region.WritePrefixed(output, $"- {GETAZS}:", _1_indent);
    }

    /// <summary>
    /// Use region as reference <see cref="Ref"/> or <see cref="Region" />
    /// and AZ index (0 to 3) to identify an availability zone for the
    /// region of operation.
    /// </summary>
    /// <param name="index">Integer between 0 and 3 (actual number may vary by region)</param>
    /// <param name="region">Ref or Region primitive type.</param>
    public AvailabilityZone(int index, IPrimitive region)
    {
      _AzIndex = index;
      _Region = region;
    }
  }


}