
using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Vpc : IResourceType, IHasTags
  {
    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::VPC";


    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void SetProp<T>(string name, T value) where T : IPrimitive
    => _Properties.SetProp<T>(name, value);
    
    public void SetCidrBlock(string value)
      => _Properties.SetProp<Text>("CidrBlock", new Text(value));

    public void SetEnableDnsHostnames(bool isEnable)
      => _Properties.SetProp<Bool>("EnableDnsHostnames", new Bool(isEnable));
    
    public void SetEnableDnsSupport(bool isEnable)
      => _Properties.SetProp<Bool>("EnableDnsSupport", new Bool(isEnable));
      // => _Properties["EnableDnsSupport"] = _Properties["EnableDnsSupport"].SetValue<Bool>( new Bool(isEnable) );

    public void SetInstanceTenancy(string tenancy)
      => _Properties.SetProp<Text>("InstanceTenancy", new Text(tenancy));
      // => _Properties["InstanceTenancy"] = _Properties["InstanceTenancy"].SetValue( new Text(tenancy) );

    public void SetIpv4IpamPoolId(IpamPoolIdValues poolId)
      => _Properties.SetProp<Text>("Ipv4IpamPoolId", new Text(poolId.ToString("F")));
      // => _Properties["Ipv4IpamPoolId"] = _Properties["Ipv4IpamPoolId"].SetValue( new Text(poolId) );
    
    public void SetIpv4NetmaskLength(int length)
      => _Properties.SetProp<IntNumber>("Ipv4NetmaskLength", new IntNumber(length));
      // => _Properties["Ipv4NetmaskLength"] = _Properties["Ipv4NetmaskLength"].SetValue( new IntNumber(length) );
    
    public void SetTags(Tags tags)
      => _Properties.SetProp<Tags>("Tags", tags);
      // => _Properties["Tags"] = _Properties["Tags"].SetValue(tags);

    public void AddTag(string key, string value)
    {
      Tag tag = new Tag(key, value);
      if (! _Properties.HasValue<Tags>("Tags"))
      {
        Tags tags = new Tags(tag);
        _Properties.SetProp<Tags>("Tags", tags);
      }
      else
      {
        _Properties.Access<Tags>("Tags", tags=> tags.TagList.Add(tag));
      }
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
    }
  }
  
}