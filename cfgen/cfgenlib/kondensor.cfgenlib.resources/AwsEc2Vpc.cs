
using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Vpc : IResourceType, IHasTags
  {
    public string Name => "AWS::EC2::VPC";

    public Dictionary<string, ResourceProperty> Properties => _Properties;

    private Dictionary<string, ResourceProperty> _Properties;
    
    public void SetProp(ResourceProperty prop) => BaseResourceType.SetProp(prop, _Properties);

    public void SetCidrBlock(string value)
      => _Properties["CidrBlock"].SetValue<Text>( new Text(value) );

    public void SetEnableDnsHostnames(bool isEnable)
      => _Properties["EnableDnsHostnames"].SetValue<Bool>( new Bool(isEnable) );
    
    public void SetEnableDnsSupport(bool isEnable)
      => _Properties["EnableDnsSupport"].SetValue<Bool>( new Bool(isEnable) );

    public void SetInstanceTenancy(string tenancy)
      => _Properties["InstanceTenancy"].SetValue( new Text(tenancy) );

    public void SetIpv4IpamPoolId(string poolId)
      => _Properties["Ipv4IpamPoolId"].SetValue( new Text(poolId) );
    
    public void SetIpv4NetmaskLength(int length)
      => _Properties["Ipv4NetmaskLength"].SetValue( new IntNumber(length) );
    
    public void SetTags(Tags tags)
      => _Properties["Tags"].SetValue(tags);

    public void AddTag(string key, string value)
    {
      Tag tag = new Tag(key, value);
      _Properties["Tags"].GetValue<Tags>().MatchSome( tags => tags.TagList.Add(tag));
    }

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