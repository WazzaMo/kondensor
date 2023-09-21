/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2VpcInternetGatewayAttachment : IResourceType
  {
    private const string 
      INTERNET_GW_ID = "InternetGatewayId", // String
      VPC_ID = "VpcId", // String
      VPN_GW_ID = "VpnGatewayId"; // String

    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::VPCGatewayAttachment";

    public string Id { get; private set; }

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public AwsEc2VpcInternetGatewayAttachment SetVpcId(IReference vpcId)
    {
      _Properties.SetProp<IReference>(VPC_ID, vpcId);
      return this;
    }

    public AwsEc2VpcInternetGatewayAttachment SetInternetGatewayId(string gwId)
    {
      _Properties.SetProp<Text>(INTERNET_GW_ID, new Text(gwId));
      return this;
    }

    public AwsEc2VpcInternetGatewayAttachment SetVpnGatewayId(string vpnGwId)
    {
      _Properties.SetProp<Text>(VPN_GW_ID, new Text(vpnGwId));
      return this;
    }

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      throw new NotImplementedException();
    }

    public IResourceType AddTag(string key, string value)
    {
      throw new NotImplementedException();
    }

    public void AssertRequiredPropertiesSet()
    {
      _Properties.AssertHasValue<IReference>(VPC_ID);
      _Properties.AssertHasOnlyOneOf(INTERNET_GW_ID, VPN_GW_ID);
    }

    public void setId(string id) => Id = id;

    public AwsEc2VpcInternetGatewayAttachment()
    {
      _Properties = new ResourceProperties(INTERNET_GW_ID, VPC_ID, VPN_GW_ID);
      Id = Resource.DEFAULT_ID;
    }
  }

}