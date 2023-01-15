/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Subnet : IOutputResourceType, IHasTags
  {
    public static readonly string 
      ASSIGN_IPV6_ADDRESS_ON_CREATION = "AssignIpv6AddressOnCreation", //: Boolean
      AVAILABILITY_ZONE = "AvailabilityZone", //: String
      AZ_ID = "AvailabilityZoneId", //: String
      CIDR_BLOCK = "CidrBlock", //: String
      ENABLE_DNS_64 = "EnableDns64", //: Boolean
      IPV6_CIDR_BLOCK = "Ipv6CidrBlock", //: String
      IPVS_NATIVE = "Ipv6Native", //: Boolean
      MAP_PUBLIC_IP_ON_LAUNCH = "MapPublicIpOnLaunch", //: Boolean
      OUTPUT_ARN = "OutpostArn", //: String
      PRIVATE_DNS_NAME_OPTIONS_ON_LAUNCH = "PrivateDnsNameOptionsOnLaunch", //: 
      TAGS = "Tags", //: 
      VPC_ID = "VpcId";

    public string Type => throw new NotImplementedException();

    public Dictionary<string, ResourceProperty> Properties => throw new NotImplementedException();

    public void AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      throw new NotImplementedException();
    }

    public void AddTag(string key, string value)
    {
      throw new NotImplementedException();
    }
  }

}