/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct VpcIngress : IResourceType
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

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void SetCidrIp(IpCidrAddress address)
      => _Properties.SetProp(INGRESS_CIDRIP, address);
    
    public bool HasCidrIp()
      => _Properties.HasValue<IpCidrAddress>(INGRESS_CIDRIP);
    
    public void SetDescription(string description)
      => _Properties.SetProp(INGRESS_DESCRIPTION, new Text( description ));
    
    public bool HasDescription()
      => _Properties.HasValue<Text>(INGRESS_DESCRIPTION);
    
    public void SetFromPort(int port)
      => _Properties.SetProp<IntNumber>(INGRESS_FROM_PORT, new IntNumber(port));
    
    public bool HasFromPort()
      => _Properties.HasValue<IntNumber>(INGRESS_FROM_PORT);
    
    public void SetGroupId(string id)
      => _Properties.SetProp<Text>(INGRESS_GROUP_ID, new Text(id) );
    
    public bool HasGroupId()
      => _Properties.HasValue<Text>(INGRESS_GROUP_ID);
    
    public void SetGroupName(string name)
      => _Properties.SetProp<Text>(INGRESS_GROUP_NAME, new Text(name) );
    
    public bool HasGroupName()
      => _Properties.HasValue<Text>(INGRESS_GROUP_NAME);
    
    public void SetIpProtocol(IpProtocol protocol)
      => _Properties.SetProp<IpProtocol>(INGRESS_IP_PROTOCOL, protocol);
    
    public bool HasIpProtocol()
      => _Properties.HasValue<IpProtocol>(INGRESS_IP_PROTOCOL);
    
    public void SetSourcePrefixListId(string id)
      => _Properties.SetProp<Text>(INGRESS_SOURCE_PREFIX_LIST_ID,  new Text(id) );
    
    public bool HasSourcePrefixListId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_PREFIX_LIST_ID);
    
    public void SetSourceSecurityGroupId(string id)
      => _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_ID, new Text(id) );
    
    public bool HasSourceSecurityGroupId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_ID);
    
    public void SetSourceSecurityGroupName(string name)
      => _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_NAME, new Text(name) );
    
    public bool HasSourceSecurityGroupName()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_NAME);
    
    public void SetSourceSecurityGroupOwnerId(string id)
      => _Properties.SetProp<Text>(INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID, new Text(id) );
    
    public bool HasSourceSecurityGroupOwnerId()
      => _Properties.HasValue<Text>(INGRESS_SOURCE_SECURITY_GROUP_OWNER_ID);
    
    public void SetToPort(int port)
      => _Properties.SetProp<IntNumber>(INGRESS_TO_PORT, new IntNumber(port));
    
    public bool HasToPort()
      => _Properties.HasValue<IntNumber>(INGRESS_TO_PORT);
    
    public VpcIngress()
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
    }
  }

}