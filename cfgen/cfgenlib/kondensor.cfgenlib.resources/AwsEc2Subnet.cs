/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.outputs;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// Defines a VPC subnet and how the subnet impacts
  /// EC2 instances launched in that subnet.
  /// <see cref="https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-ec2-subnet.html"/>
  /// </summary>
  public struct AwsEc2Subnet : IResourceType
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
    public AwsEc2Subnet SetAvailabilityZoneAndCidrBlock(AvailabilityZone az, IpCidrAddress cidr)
    {
      _Properties.SetProp<AvailabilityZone>(AVAILABILITY_ZONE, az);
      _Properties.SetProp<IpCidrAddress>(CIDR_BLOCK, cidr);
      return this;
    }

    public AwsEc2Subnet SetAvailabilityZoneId( string azId )
    {
      _Properties.SetProp<Text>(AZ_ID, new Text(azId));
      return this;
    }
    
    /// <summary>
    /// Indicates whether instances launched in this subnet receive a public IPv4 address. The default value is false
    /// Opional.
    /// </summary>
    /// <param name="isEnabled">True causes EC2 intances to get public IP on launch.</param>
    public AwsEc2Subnet SetMapPublicIpOnLaunch(bool isEnabled)
    {
      _Properties.SetProp<Bool>(MAP_PUBLIC_IP_ON_LAUNCH, new Bool(isEnabled));
      return this;
    }

    /// <summary>
    /// The Amazon Resource Name (ARN) of the Outpost. (Optional)
    /// </summary>
    /// <param name="arn">ARN for Outpost.</param>
    public AwsEc2Subnet SetOutpostArn(string arn)
    {
      _Properties.SetProp<Text>(OUTPOST_ARN, new Text(arn));
      return this;
    }

    /// <summary>
    /// Set the private DNS on-launch options.
    /// <see cref="VpcSubnetDnsNameOptions"/>
    /// </summary>
    /// <param name="options">The options resource primitive <see cref="VpcSubnetDnsNameOptions" /></param>    
    public AwsEc2Subnet SetPrivateDnsNameOptionsOnLaunch(VpcSubnetDnsNameOptions options)
    {
      _Properties.SetProp<VpcSubnetDnsNameOptions>(PRIVATE_DNS_NAME_OPTIONS_ON_LAUNCH, options);
      return this;
    }

    /// <summary>
    /// Set the VPC ID from an Import that will
    /// contain the subnet for use in security groups and for
    /// EC2 instances to use.
    /// </summary>
    /// <param name="vpcId">Ref or Import of VPC ID</param>
    public AwsEc2Subnet SetVpcId(Import vpcId)
    {
      _Properties.SetProp<Import>(VPC_ID, vpcId);
      return this;
    }
    
    /// <summary>
    /// Set the VPC ID from a Ref that will identify the VPC
    /// declared in the same template, that is to
    /// contain the subnet for use in security groups and for
    /// EC2 instances to use.
    /// </summary>
    /// <param name="reference">Ref value to use.</param>
    public AwsEc2Subnet SetVpcId(Ref reference)
    {
      _Properties.SetProp<Ref>(VPC_ID, reference);
      return this;
    }

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      SubnetOutput subnetOut = new SubnetOutput(environment, name);
      Outputs.AddOutput(document, subnetOut, optionalText);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
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