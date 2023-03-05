/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.patterns
{

  /// <summary>
  /// Patterns of use for AWS VPC
  /// </summary>
  public static class VpcPatterns
  {

    /// <summary>
    ///   Define a VPC with a declaration ID, a description
    ///   and key parameters, such as the base IP range CIDR
    ///   and the type of tenancy model for any EC2 instances
    ///   launched in the VPC.
    /// </summary>
    /// <param name="stack">Stack to populate</param>
    /// <param name="vpcDecl">VPC declaration ID</param>
    /// <param name="vpcDescription">Description</param>
    /// <param name="ipv4Range">CIDR IP v4 range</param>
    /// <param name="isUseDnsHostnames">Should DNS hostnames be enabled</param>
    /// <param name="isUseSharedEc2Hardware">Do you want EC2s to be on dedicated hardware or shared.</param>
    /// <param name="isUseDedicatedHost">Do you want the option to define dedicated or not at launch.</param>
    /// <param name="tags">Tags to add to the VPC</param>
    /// <returns></returns>
    public static Stack DefineVpc(
      Stack stack,
      string vpcDecl,
      string vpcDescription,
      IpCidrAddress ipv4Range,
      bool isUseDnsHostnames,
      bool isUseSharedEc2Hardware,
      bool isUseDedicatedHost,
      (string key, string value)[] tags
    )
    {
      string tenancy;
      if (isUseSharedEc2Hardware)
        tenancy = "default";
      else if (isUseDedicatedHost)
        tenancy = "host";
      else
        tenancy = "dedicated";
      
      stack.AddResource<AwsEc2Vpc>(
        vpcDecl,
        vpc => 
        {
          vpc
          .SetCidrBlock(ipv4Range)
          .SetEnableDnsSupport(isUseDnsHostnames)
          .SetEnableDnsHostnames(isUseDnsHostnames)
          .SetInstanceTenancy(tenancy);
          for(int index = 0; index < tags.Length; index++)
            vpc.AddTag(tags[index].key, tags[index].value);
          return vpc;
        },
        vpcDescription
      );
      return stack;
    }

    public static Stack DefineSubnetsAcrossAzs(
      Stack stack,
      string vpcDecl,
      IpCidrAddress ipv4Range,
      int numberOfSubnets
    )
    {
      Ref vpc;

      stack.Ref(vpcDecl, out vpc);
      byte[] octets = ipv4Range.Octets;
      int variationIndex = ipv4Range.Cidr >= 24 ? 2
        : ipv4Range.Cidr >= 16 ? 1 : 0;
      
      byte startVariation = octets[variationIndex];
      
      for(int index = 0; index < numberOfSubnets; index++)
      {
        AvailabilityZone az = new AvailabilityZone(index, Regions.CurrentRegion());
        byte[] address = CopyOctets(octets);
        address[variationIndex] = (byte) (startVariation + (byte)index);
        IpCidrAddress block = new IpCidrAddress(ipv4Range.Cidr, address);
        stack.AddResource<AwsEc2Subnet>(
        id: "InnerSubnet",
          (subnetProps) => subnetProps
            .SetVpcId( vpc)
            .SetAvailabilityZoneAndCidrBlock(az, block),
          optText: $"Internal subnet in AZ{index}"
        );
      }
      return stack;
    }

    private static byte[] CopyOctets(byte[] octets)
    {
      byte[] result = new byte[octets.Length];
      octets.CopyTo(result, 0);
      return result;
    }

  } // VPC

}