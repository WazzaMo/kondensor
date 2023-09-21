/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2IpamPoolCidr : IResourceType
  {
    private ResourceProperties _Properties;
    private const string
      TYPE = "AWS::EC2::IPAMPoolCidr",
      CIDR = "Cidr", // String
      IPAM_POOL_ID = "IpamPoolId", // String
      NETMASK_LEN = "NetmaskLength"; //: Integer


    public string Type => throw new NotImplementedException();

    public string Id {get; private set; }

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      OutputData declaration = new OutputData(environment, this);
      ExportData export = new ExportData(environment, this);
      Outputs.AddOutput(document, declaration, optionalText);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public void AssertRequiredPropertiesSet()
    {
      _Properties.AssertHasValue<Text>(IPAM_POOL_ID);
    }

    public void setId(string id)
      => Id = id;

    public AwsEc2IpamPoolCidr SetCidr(IpCidrAddress cidr)
    {
      _Properties.SetProp<IpCidrAddress>(CIDR, cidr);
      return this;
    }

    public AwsEc2IpamPoolCidr SetIpamPoolId(string poolId)
    {
      _Properties.SetProp<Text>(IPAM_POOL_ID, new Text(poolId));
      return this;
    }

    public AwsEc2IpamPoolCidr SetNetmaskLength(int len)
    {
      _Properties.SetProp<IntNumber>(NETMASK_LEN, new IntNumber(len));
      return this;
    }

    public AwsEc2IpamPoolCidr()
    {
      _Properties = new(
        CIDR,
        IPAM_POOL_ID,
        NETMASK_LEN
      );
      Id = Resource.DEFAULT_ID;
    }
  }


}