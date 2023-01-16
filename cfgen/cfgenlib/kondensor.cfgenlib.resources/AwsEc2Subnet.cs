/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// Defines a VPC subnet and how the subnet impacts
  /// EC2 instances launched in that subnet.
  /// <see cref="https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-ec2-subnet.html"/>
  /// </summary>
  public struct AwsEc2Subnet : IOutputResourceType, IHasTags
  {
    private ResourceProperties _Properties;


    public static readonly string 
      ASSIGN_IPV6_ADDRESS_ON_CREATION = "AssignIpv6AddressOnCreation", // Bool - unused until ipV6 supported
      AVAILABILITY_ZONE = "AvailabilityZone", //: String
      AZ_ID = "AvailabilityZoneId", //: String
      CIDR_BLOCK = "CidrBlock", //: String
      ENABLE_DNS_64 = "EnableDns64", //: Boolean - unused until IpV6 supports
      IPV6_CIDR_BLOCK = "Ipv6CidrBlock", //: String
      IPV6_NATIVE = "Ipv6Native", //: Boolean - unused until IpV6 supported
      MAP_PUBLIC_IP_ON_LAUNCH = "MapPublicIpOnLaunch", //: Boolean
      OUTPOST_ARN = "OutpostArn", //: String
      PRIVATE_DNS_NAME_OPTIONS_ON_LAUNCH = "PrivateDnsNameOptionsOnLaunch", //: 
      TAGS = "Tags", //: 
      VPC_ID = "VpcId";

    public string Type => "AWS::EC2::Subnet";

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    // public void SetAssignIpv6AddressOnCreation(bool isEnabled)
    //   => _Properties.SetProp<Bool>(ASSIGN_IPV6_ADDRESS_ON_CREATION, new Bool(isEnabled));
    
    /// <summary>
    /// The AZ name and CIDR block must be set together.
    /// </summary>
    /// <param name="az">Availability Zone of the subnet</param>
    /// <param name="cidr">The IPv4 CIDR block assigned to the subnet.</param>
    public void SetAvailabilityZoneAndCidrBlock(string az, IpCidrAddress cidr)
    {
      _Properties.SetProp<Text>(AVAILABILITY_ZONE, new Text(az));
      _Properties.SetProp<IpCidrAddress>(CIDR_BLOCK, cidr);
    }

    public void SetAvailabilityZoneId( string azId )
      => _Properties.SetProp<Text>(AZ_ID, new Text(azId));
    
    /// <summary>
    /// Indicates whether instances launched in this subnet receive a public IPv4 address. The default value is false
    /// Opional.
    /// </summary>
    /// <param name="isEnabled">True causes EC2 intances to get public IP on launch.</param>
    public void SetMapPublicIpOnLaunch(bool isEnabled)
      => _Properties.SetProp<Bool>(MAP_PUBLIC_IP_ON_LAUNCH, new Bool(isEnabled));

    /// <summary>
    /// The Amazon Resource Name (ARN) of the Outpost. (Optional)
    /// </summary>
    /// <param name="arn">ARN for Outpost.</param>
    public void SetOutpostArn(string arn)
      => _Properties.SetProp<Text>(OUTPOST_ARN, new Text(arn));
    
    //     Available options:
    // EnableResourceNameDnsAAAARecord (true | false)
    // EnableResourceNameDnsARecord (true | false)
    // HostnameType (ip-name | resource-name)
    // public void SetPrivateDnsNameOptionsOnLaunch()

    public void AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      throw new NotImplementedException();
    }

    public void AddTag(string key, string value)
    {
      throw new NotImplementedException();
    }

    public AwsEc2Subnet()
    {
      _Properties = new ResourceProperties(
        ASSIGN_IPV6_ADDRESS_ON_CREATION,
        AVAILABILITY_ZONE,
        AZ_ID,
        CIDR_BLOCK,
        ENABLE_DNS_64,
        IPV6_CIDR_BLOCK,
        IPV6_NATIVE,
        MAP_PUBLIC_IP_ON_LAUNCH,
        OUTPOST_ARN,
        PRIVATE_DNS_NAME_OPTIONS_ON_LAUNCH,
        TAGS,
        VPC_ID
      );
    }
  }

}