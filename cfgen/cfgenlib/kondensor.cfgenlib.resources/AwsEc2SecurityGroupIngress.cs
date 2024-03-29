/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2SecurityGroupIngress : IResourceType, IProtocolAndPortRange<AwsEc2SecurityGroupIngress>
  {
    public readonly string
      INGRESS_CIDRIP = "CidrIp", //: String
      INGRESS_CIDRIPV6 = "CidrIpv6", //: String
      INGRESS_DESCRIPTION = "Description", //: String
      INGRESS_FROM_PORT = "FromPort", //: Integer
      INGRESS_GROUP_ID = "GroupId", //: String
      INGRESS_GROUP_NAME = "GroupName", //: String
      INGRESS_IP_PROTOCOL = "IpProtocol", //: String
      INGRESS_SOURCE_PREFIX_LIST_ID = "SourcePrefixListId", //: String
      INGRESS_SOURCE_SECURITY_GROUP_ID = "SourceSecurityGroupId", //: String
      INGRESS_SOURCE_SECURITY_GROUP_NAME = "SourceSecurityGroupName", //: String
      INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID = "SourceSecurityGroupOwnerId", //: String
      INGRESS_TO_PORT = "ToPort"; //: Integer

    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::SecurityGroupIngress";

    public string Id {get; private set;}

    public void setId(string id) => Id = id;


    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public AwsEc2SecurityGroupIngress SetCidrIp(IpCidrAddress address)
    {
      _Properties.SetProp(INGRESS_CIDRIP, address);
      return this;
    }
    
    public bool HasCidrIp()
      => _Properties.HasValue<IpCidrAddress>(INGRESS_CIDRIP);
    
    public AwsEc2SecurityGroupIngress SetDescription(string description)
    {
      _Properties.SetProp(INGRESS_DESCRIPTION, new Text( description ));
      return this;
    }
    
    public bool HasDescription()
      => _Properties.HasValue<Text>(INGRESS_DESCRIPTION);
    
    public AwsEc2SecurityGroupIngress SetFromPort(int port)
    {
      _Properties.SetProp<IntNumber>(INGRESS_FROM_PORT, new IntNumber(port));
      return this;
    }
    
    public bool HasFromPort()
      => _Properties.HasValue<IntNumber>(INGRESS_FROM_PORT);
    
    public AwsEc2SecurityGroupIngress SetGroupId(string id)
    {
      _Properties.SetProp<Text>(INGRESS_GROUP_ID, new Text(id) );
      return this;
    }
    
    public bool HasGroupId()
      => _Properties.HasValue<Text>(INGRESS_GROUP_ID);
    
    public AwsEc2SecurityGroupIngress SetGroupName(string name)
    {
      _Properties.SetProp<Text>(INGRESS_GROUP_NAME, new Text(name) );
      return this;
    }
    
    public bool HasGroupName()
      => _Properties.HasValue<Text>(INGRESS_GROUP_NAME);
    
    public AwsEc2SecurityGroupIngress SetIpProtocol(IpProtocol protocol)
    {
      _Properties.SetProp<IpProtocol>(INGRESS_IP_PROTOCOL, protocol);
      return this;
    }
    
    public bool HasIpProtocol()
      => _Properties.HasValue<IpProtocol>(INGRESS_IP_PROTOCOL);
    
    public AwsEc2SecurityGroupIngress SetSourcePrefixListId(string id)
    {
      _Properties.SetProp<Text>(INGRESS_SOURCE_PREFIX_LIST_ID,  new Text(id) );
      return this;
    }
    
    public bool HasSourcePrefixListId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_PREFIX_LIST_ID);
    
    public AwsEc2SecurityGroupIngress SetSourceSecurityGroupId(string id)
    {
      _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_ID, new Text(id) );
      return this;
    }
    
    public bool HasSourceSecurityGroupId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_ID);
    
    public AwsEc2SecurityGroupIngress SetSourceSecurityGroupName(string name)
    {
      _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_NAME, new Text(name) );
      return this;
    }
    
    public bool HasSourceSecurityGroupName()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_NAME);
    
    public AwsEc2SecurityGroupIngress SetSourceSecurityGroupOwnerId(string id)
    {
      _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID, new Text(id) );
      return this;
    }
    
    public bool HasSourceSecurityGroupOwnerId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID);
    
    public AwsEc2SecurityGroupIngress SetToPort(int port)
    {
      _Properties.SetProp<IntNumber>(INGRESS_TO_PORT, new IntNumber(port));
      return this;
    }
    
    public bool HasToPort()
      => _Properties.HasValue<IntNumber>(INGRESS_TO_PORT);

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      throw new NotImplementedException(message:"Add tags to the security group, instead.");
    }

    public void AssertRequiredPropertiesSet()
    {
      // only SourceSecurityGroupOwnerId is conditionally required.
    }

    public AwsEc2SecurityGroupIngress()
    {
      _Properties = new ResourceProperties(
        INGRESS_CIDRIP,
        INGRESS_CIDRIPV6,
        INGRESS_DESCRIPTION,
        INGRESS_FROM_PORT,
        INGRESS_GROUP_ID,
        INGRESS_GROUP_NAME,
        INGRESS_IP_PROTOCOL,
        INGRESS_SOURCE_PREFIX_LIST_ID,
        INGRESS_SOURCE_SECURITY_GROUP_ID,
        INGRESS_SOURCE_SECURITY_GROUP_NAME,
        INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID,
        INGRESS_TO_PORT
      );
      Id = Resource.DEFAULT_ID;
    }
  }

}