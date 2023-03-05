/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Vpc : IResourceType
  {
    const int
      IPV4_MIN_CIDR_SUBNET_SIZE = 16,
      IPV4_MAX_CIDR_SUBNET_SIZE = 28;
    private ResourceProperties _Properties;

    const string
      ENABLE_DNS_HOSTNAMES = "EnableDnsHostnames",
      ENABLE_DNS_SUPPORT = "EnableDnsSupport",
      INSTANCE_TENANCY = "InstanceTenancy",
      IPV4_IPAM_POOL_ID = "Ipv4IpamPoolId",
      IPV4_NETMASK_LEN = "Ipv4NetmaskLength",
      TAGS = "Tags";

    public string Type => "AWS::EC2::VPC";


    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public string Id { get; private set; }

    public AwsEc2Vpc SetCidrBlock(IpCidrAddress cidr)
    {
      const string CIDR_WARNING = "VPC CIDR subnet size must be between 16 and 28";

      if (cidr.Cidr < IPV4_MIN_CIDR_SUBNET_SIZE)
        throw new ArgumentException(message: $"{CIDR_WARNING} and {cidr.Cidr} is too small.");
      if (cidr.Cidr > IPV4_MAX_CIDR_SUBNET_SIZE)
        throw new ArgumentException(message: $"{CIDR_WARNING} and {cidr.Cidr} is too large.");
      _Properties.SetProp<IpCidrAddress>("CidrBlock", cidr);
      return this;
    }

    public AwsEc2Vpc SetEnableDnsHostnames(bool isEnable)
    {
      _Properties.SetProp<Bool>(ENABLE_DNS_HOSTNAMES, new Bool(isEnable));
      return this;
    }

    public AwsEc2Vpc SetEnableDnsSupport(bool isEnable)
    {
      _Properties.SetProp<Bool>(ENABLE_DNS_SUPPORT, new Bool(isEnable));
      return this;
    }

    public AwsEc2Vpc SetInstanceTenancy(string tenancy)
    {
      _Properties.SetProp<Text>(INSTANCE_TENANCY, new Text(tenancy));
      return this;
    }

    // TODO
    public AwsEc2Vpc SetIpv4IpamPoolId(IpamPoolIdValues poolId)
    {
      _Properties.SetProp<Text>(IPV4_IPAM_POOL_ID, new Text(poolId.ToString("F")));
      return this;
    }

    public AwsEc2Vpc SetIpv4NetmaskLength(int length)
    {
      _Properties.SetProp<IntNumber>(IPV4_NETMASK_LEN, new IntNumber(length));
      return this;
    }

    public AwsEc2Vpc SetTags(Tags tags)
    {
      _Properties.SetProp<Tags>(TAGS, tags);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public IResourceType AddOutput(
      TemplateDocument document,
      string environment,
      string name,
      params string[] optionalText
    )
    {
      // var (description, condition) = Outputs.GetOutputOptionsFrom(optionalText);
      OutputData outputVpc = new OutputData(environment, this);
      Outputs.AddOutput(document, outputVpc, optionalText);
      return this;
    }

    public void setId(string id) => Id = id;

    private string UniqueExportName(string environment, string name)
      => $"{environment}:{name}";

    public void AssertRequiredPropertiesSet()
    {
      // conditionally required only: 
      // - CidrBlock
      // - Ipv4IpamPoolId
      // conditions not yet clear.
    }

    public AwsEc2Vpc()
    {
      _Properties = new ResourceProperties(
        "CidrBlock",
        "EnableDnsHostnames", //: Boolean
        "EnableDnsSupport", //: Boolean
        "InstanceTenancy", //: String
        "Ipv4IpamPoolId", //: String
        "Ipv4NetmaskLength", //: Integer
        "Tags" //: 
      );
      Id = "empty";
    }

  }// end struct
  
}