
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using Optional;

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// 
  /// <see
  ///   href="https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-ec2-ipampool.html"
  /// />
  /// </summary>
  public struct AwsEc2IpamPool : IResourceType
  {
    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::IPAMPool";

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public string Id {get; private set;}

    public void setId(string id) => Id = id;

    public void SetProp<T>(string name, T value) where T : IPrimitive
      => _Properties.SetProp<T>(name, value);
    
    public void SetAddressFamily(IpAddressFamily family)
      => _Properties.SetProp<Text>(name: "AddressFamily", value: new Text(family.ToString("F")));

    public void SetAllocationDefaultNetmaskLength(int defaultLength)
      => _Properties.SetProp<IntNumber>(name:"AllocationDefaultNetmaskLength", value: new IntNumber(defaultLength));
    
    public void SetAllocationMaxNetmaskLength(int maxLength)
      => _Properties.SetProp<IntNumber>(name: "AllocationMaxNetmaskLength", value: new IntNumber(maxLength));
    
    public void SetAllocationMinNetmaskLength(int minLength)
      => _Properties.SetProp<IntNumber>(name: "AllocationMinNetmaskLength", value: new IntNumber(minLength));

    public void SetAllocationResourceTags(Tags tags)
      => _Properties.SetProp<Tags>(name: "AllocationResourceTags", value: tags);
    
    public void SetAutoImport(bool isAutoImport)
      => _Properties.SetProp<Bool>(name: "AutoImport", value: new Bool(isAutoImport));

    public void SetAwsService(AwsService awsService)
      => _Properties.SetProp<Text>(name: "AwsService", value: new Text(awsService.ToString("F")));
    
    public void SetDescription(string description)
      => _Properties.SetProp<Text>(name: "Description", value: new Text(description));
    
    public void SetIpamScopeId(string scopeId)
      => _Properties.SetProp<Text>(name: "IpamScopeId", value: new Text(scopeId));

    public void SetLocale(string locale)
      => _Properties.SetProp<Text>(name: "Locale", value: new Text(locale));

    public void SetProvisionedCidrs(List<string> provisionedCidrs)
      => _Properties.SetProp<PrimitiveList<Text>>(
          name: "ProvisionedCidrs",
          value: new PrimitiveList<Text>( provisionedCidrs.ConvertAll<Text>(x => new Text(x)) )
        );
    public void SetPubliclyAdvertisable(bool isPubliclyAdvertisable)
      => _Properties.SetProp<Bool>(name: "PubliclyAdvertisable", value: new Bool(isPubliclyAdvertisable));
    public void SetSourceIpamPoolId(string sourceIpamPoolId)
      => _Properties.SetProp<Text>(name: "SourceIpamPoolId", value: new Text(sourceIpamPoolId));
    public void SetTag(Tags tags)
      => _Properties.SetProp<Tags>(name: "Tags", value: tags);

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public AwsEc2IpamPool()
    {
      _Properties = new ResourceProperties(
        "AddressFamily",
        "AllocationDefaultNetmaskLength",
        "AllocationMaxNetmaskLength",
        "AllocationMinNetmaskLength",
        "AllocationResourceTags", // Tags
        "AutoImport",
        "AwsService",
        "Description",
        "IpamScopeId",
        "Locale",
        "ProvisionedCidrs",
        "PubliclyAdvertisable",
        "SourceIpamPoolId",
        "Tags"  
      );
      Id = "empty";
    }
  }

}