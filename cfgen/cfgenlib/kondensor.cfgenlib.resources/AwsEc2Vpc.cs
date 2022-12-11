
using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Vpc : IResourceType
  {
    public string Name => "AWS::EC2::VPC";

    public Dictionary<string, ResourceProperty> Properties => _Properties;

    private Dictionary<string, ResourceProperty> _Properties;
    
    public void SetProp(ResourceProperty prop) => BaseResourceType.SetProp(prop, _Properties);

    public void SetCidrBlock(string value)
      => _Properties["CidrBlock"].SetValue<string>(value);

    public void SetEnableDnsHostnames(bool isEnable)
      => _Properties["EnableDnsHostnames"].SetValue<bool>(isEnable);
    
    public void SetEnableDnsSupport(bool isEnable)
      => _Properties["EnableDnsSupport"].SetValue<bool>(isEnable);

    public void SetInstanceTenancy(string tenancy)
      => _Properties["InstanceTenancy"].SetValue(tenancy);

    public void SetIpv4IpamPoolId(string poolId)
      => _Properties["Ipv4IpamPoolId"].SetValue(poolId);
    
    public void SetIpv4NetmaskLength(int length)
      => _Properties["Ipv4NetmaskLength"].SetValue(length);
    
    public void SetTags(Tags tags)
      => _Properties["Tags"].SetValue(tags);

    public AwsEc2Vpc()
    {
      _Properties = BaseResourceType.DeclareProperties(
        "CidrBlock",
        "EnableDnsHostnames", //: Boolean
        "EnableDnsSupport", //: Boolean
        "InstanceTenancy", //: String
        "Ipv4IpamPoolId", //: String
        "Ipv4NetmaskLength", //: Integer
        "Tags" //: 
      );
    }
  }
  
}