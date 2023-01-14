/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct VpcEgress : IResourceType, IProtocolAndPortRange
  {
    public readonly string
        EGRESS_IP = "CidrIp", //: String
        EGRESS_IPV6 = "CidrIpv6", //: String
        EGRESS_DESC = "Description", //: String
        EGRESS_PREFIX_LIST_ID = "DestinationPrefixListId", //: String
        EGRESS_DESTINATION_GROUP_ID = "DestinationSecurityGroupId", //: String
        EGRESS_FROM_PORT = "FromPort", //: Integer
        EGRESS_GROUP_ID = "GroupId", //: String
        EGRESS_IP_PROTOCOL = "IpProtocol", //: String
        EGRESS_TO_PORT = "ToPort";

    private ResourceProperties _Properties;

    public string Type => "AWS::EC2::SecurityGroupEgress";

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void SetIpProtocol(IpProtocol protocol)
      => _Properties.SetProp<IpProtocol>(EGRESS_IP_PROTOCOL, protocol);
    
    public bool HasIpProtocol()
      => _Properties.HasValue<IpProtocol>(EGRESS_IP_PROTOCOL);
    
    public void SetFromPort(int port)
      => _Properties.SetProp<IntNumber>(EGRESS_FROM_PORT, new IntNumber(port) );
    
    public bool HasFromPort()
      => _Properties.HasValue<IntNumber>(EGRESS_FROM_PORT);
    
    public void SetToPort(int port)
      => _Properties.SetProp<IntNumber>(EGRESS_TO_PORT, new IntNumber(port));
    
    public bool HasToPort()
      => _Properties.HasValue<IntNumber>(EGRESS_TO_PORT);
    
    public void SetCidrIp(IpCidrAddress ipCidr)
      => _Properties.SetProp<IpCidrAddress>(EGRESS_IP, ipCidr);
    
    public bool HasCidrIp()
      => _Properties.HasValue<IpCidrAddress>(EGRESS_IP);

    public VpcEgress()
    {
      _Properties = new ResourceProperties(
        "CidrIp", //: String
        "CidrIpv6", //: String
        "Description", //: String
        "DestinationPrefixListId", //: String
        "DestinationSecurityGroupId", //: String
        "FromPort", //: Integer
        "GroupId", //: String
        "IpProtocol", //: String
        "ToPort" //: Integer
      );
    }
  }

}