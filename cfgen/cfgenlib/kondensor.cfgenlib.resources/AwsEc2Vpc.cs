
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
    {
      Bool _bool;
      _bool = new Bool(isEnable);
      _Properties["EnableDnsHostnames"] = _Properties["EnableDnsHostnames"].SetValue<Bool>(_bool);
    }
      // => _Properties["EnableDnsHostnames"].SetValue<Bool>( new Bool(isEnable) );
    
    public void SetEnableDnsSupport(bool isEnable)
      => _Properties["EnableDnsSupport"] = _Properties["EnableDnsSupport"].SetValue<Bool>( new Bool(isEnable) );

    public void SetInstanceTenancy(string tenancy)
      => _Properties["InstanceTenancy"] = _Properties["InstanceTenancy"].SetValue( new Text(tenancy) );

    public void SetIpv4IpamPoolId(string poolId)
      => _Properties["Ipv4IpamPoolId"] = _Properties["Ipv4IpamPoolId"].SetValue( new Text(poolId) );
    
    public void SetIpv4NetmaskLength(int length)
      => _Properties["Ipv4NetmaskLength"] = _Properties["Ipv4NetmaskLength"].SetValue( new IntNumber(length) );
    
    public void SetTags(Tags tags)
      => _Properties["Tags"] = _Properties["Tags"].SetValue(tags);

    public void AddTag(string key, string value)
    {
      Tag tag = new Tag(key, value);
      if (_Properties["Tags"].IsSet())
      {
        _Properties["Tags"].GetValue().MatchSome( _tags => {
          Tags tags = (Tags) _tags;
          tags.TagList.Add(tag);
        });
      }
      else
      {
        CreateTagsAndAddTag(tag);
      }
    }

    private void CreateTagsAndAddTag(Tag tag)
    {
      _Properties["Tags"].SetValue<Tags>(new Tags(tag));
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