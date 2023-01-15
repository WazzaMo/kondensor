/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2Vpc : IOutputResourceType, IHasTags
  {
    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::VPC";


    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void SetCidrBlock(string value)
      => _Properties.SetProp<Text>("CidrBlock", new Text(value));

    public void SetEnableDnsHostnames(bool isEnable)
      => _Properties.SetProp<Bool>("EnableDnsHostnames", new Bool(isEnable));
    
    public void SetEnableDnsSupport(bool isEnable)
      => _Properties.SetProp<Bool>("EnableDnsSupport", new Bool(isEnable));

    public void SetInstanceTenancy(string tenancy)
      => _Properties.SetProp<Text>("InstanceTenancy", new Text(tenancy));

    // TODO
    public void SetIpv4IpamPoolId(IpamPoolIdValues poolId)
      => _Properties.SetProp<Text>("Ipv4IpamPoolId", new Text(poolId.ToString("F")));
    
    public void SetIpv4NetmaskLength(int length)
      => _Properties.SetProp<IntNumber>("Ipv4NetmaskLength", new IntNumber(length));
    
    public void SetTags(Tags tags)
      => _Properties.SetProp<Tags>("Tags", tags);

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

    public void AddOutput(
      TemplateDocument document,
      string environment,
      string name,
      params string[] optionalText
    )
    {
      var (description, condition) = Outputs.GetOutputOptionsFrom(optionalText);
      VpcOutput vpcOut = new VpcOutput(environment, name);
      description.MatchSome( desc => vpcOut.SetDescription(desc) );
      condition.MatchSome(cond => vpcOut.SetCondition(cond));
      document.Outputs.MatchSome(outputs => outputs.AddOutput(vpcOut));
    }

    private string UniqueExportName(string environment, string name)
      => $"{environment}:{name}";

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

  }// end struct
  
}