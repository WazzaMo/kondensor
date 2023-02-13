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
    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::VPC";


    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public string Id { get; private set; }

    public AwsEc2Vpc SetCidrBlock(IpCidrAddress cidr)
    {
      _Properties.SetProp<IpCidrAddress>("CidrBlock", cidr);
      return this;
    }

    public AwsEc2Vpc SetEnableDnsHostnames(bool isEnable)
    {
      _Properties.SetProp<Bool>("EnableDnsHostnames", new Bool(isEnable));
      return this;
    }

    public AwsEc2Vpc SetEnableDnsSupport(bool isEnable)
    {
      _Properties.SetProp<Bool>("EnableDnsSupport", new Bool(isEnable));
      return this;
    }

    public AwsEc2Vpc SetInstanceTenancy(string tenancy)
    {
      _Properties.SetProp<Text>("InstanceTenancy", new Text(tenancy));
      return this;
    }

    // TODO
    public AwsEc2Vpc SetIpv4IpamPoolId(IpamPoolIdValues poolId)
    {
      _Properties.SetProp<Text>("Ipv4IpamPoolId", new Text(poolId.ToString("F")));
      return this;
    }

    public AwsEc2Vpc SetIpv4NetmaskLength(int length)
    {
      _Properties.SetProp<IntNumber>("Ipv4NetmaskLength", new IntNumber(length));
      return this;
    }

    public AwsEc2Vpc SetTags(Tags tags)
    {
      _Properties.SetProp<Tags>("Tags", tags);
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
      var (description, condition) = Outputs.GetOutputOptionsFrom(optionalText);
      OutputData outputVpc = new OutputData(environment, this);
      Outputs.AddOutput(document, outputVpc, optionalText);
      return this;
    }

    public void setId(string id) => Id = id;

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
      Id = "empty";
    }

  }// end struct
  
}