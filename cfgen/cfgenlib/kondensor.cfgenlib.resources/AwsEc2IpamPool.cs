/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;
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

    private const string
      ADDRESS_FAMILY = "AddressFamily",
      ALLOC_DEFAULT_NETMASK_LENGTH = "AllocationDefaultNetmaskLength",
      ALLOC_MAX_NETMASK_LEN = "AllocationMaxNetmaskLength",
      ALLOC_MIN_NETMASK_LEN = "AllocationMinNetmaskLength",
      ALLOC_RESOURCE_TAGS = "AllocationResourceTags",
      AUTO_IMPORT = "AutoImport",
      AWS_SERVICE = "AwsService",
      DESCRIPTION = "Description",
      IPAM_SCOPE_ID = "IpamScopeId",
      LOCALE = "Locale",
      PROVISIONED_CIDRS = "ProvisionedCidrs",
      PUBLICLY_ADVERTISABLE = "PubliclyAdvertisable",
      SOURCE_IPAM_POOL_ID = "SourceIpamPoolId",
      TAGS = "Tags";

    public string Id {get; private set;}

    public void setId(string id) => Id = id;

    public AwsEc2IpamPool SetProp<T>(string name, T value) where T : IPrimitive
    {
      _Properties.SetProp<T>(name, value);
      return this;
    }
    
    public AwsEc2IpamPool SetAddressFamily(IpAddressFamily family)
    {
      _Properties.SetProp<Text>(ADDRESS_FAMILY, value: new Text(family.ToString("F")));
      return this;
    }

    public AwsEc2IpamPool SetAllocationDefaultNetmaskLength(int defaultLength)
    {
      _Properties.SetProp<IntNumber>(ALLOC_DEFAULT_NETMASK_LENGTH, value: new IntNumber(defaultLength));
      return this;
    }
    
    public AwsEc2IpamPool SetAllocationMaxNetmaskLength(int maxLength)
    {
      _Properties.SetProp<IntNumber>(ALLOC_MAX_NETMASK_LEN, value: new IntNumber(maxLength));
      return this;
    }
    
    public AwsEc2IpamPool SetAllocationMinNetmaskLength(int minLength)
    {
      _Properties.SetProp<IntNumber>(ALLOC_MIN_NETMASK_LEN, value: new IntNumber(minLength));
      return this;
    }

    public AwsEc2IpamPool SetAllocationResourceTags(Tags tags)
    {
      _Properties.SetProp<Tags>(ALLOC_RESOURCE_TAGS, value: tags);
      return this;
    }
    
    public AwsEc2IpamPool SetAutoImport(bool isAutoImport)
    {
      _Properties.SetProp<Bool>(AUTO_IMPORT, value: new Bool(isAutoImport));
      return this;
    }

    public AwsEc2IpamPool SetAwsService(AwsService awsService)
    {
      _Properties.SetProp<Text>(AWS_SERVICE, value: new Text(awsService.ToString("F")));
      return this;
    }
    
    public AwsEc2IpamPool SetDescription(string description)
    {
      _Properties.SetProp<Text>(DESCRIPTION, value: new Text(description));
      return this;
    }
    
    public AwsEc2IpamPool SetIpamScopeId(string scopeId)
    {
      _Properties.SetProp<Text>(IPAM_SCOPE_ID, value: new Text(scopeId));
      return this;
    }

    public AwsEc2IpamPool SetLocale(Region region)
    {
      _Properties.SetProp<Region>(LOCALE, region);
      return this;
    }

    public AwsEc2IpamPool SetProvisionedCidrs(List<string> provisionedCidrs)
    {
      _Properties.SetProp<PrimitiveList<Text>>(
          PROVISIONED_CIDRS,
          value: new PrimitiveList<Text>( provisionedCidrs.ConvertAll<Text>(x => new Text(x)) )
        );
      return this;
    }

    public AwsEc2IpamPool SetPubliclyAdvertisable(bool isPubliclyAdvertisable)
    {
      _Properties.SetProp<Bool>(PUBLICLY_ADVERTISABLE, value: new Bool(isPubliclyAdvertisable));
      return this;
    }
    public AwsEc2IpamPool SetSourceIpamPoolId(string sourceIpamPoolId)
    {
      _Properties.SetProp<Text>(SOURCE_IPAM_POOL_ID, value: new Text(sourceIpamPoolId));
      return this;
    }

    public AwsEc2IpamPool SetTag(Tags tags)
    {
      _Properties.SetProp<Tags>(TAGS, value: tags);
      return this;
    }

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
      _Properties.AssertHasValue<Text>(ADDRESS_FAMILY);
      _Properties.AssertHasValue<Text>(IPAM_SCOPE_ID);
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
      Id = Resource.DEFAULT_ID;
    }
  }

}