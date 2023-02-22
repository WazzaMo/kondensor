/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */



namespace kondensor.cfgenlib.primitives
{

  public interface IProtocolAndPortRange<Tr> where Tr : struct, IResourceType
  {
    Tr SetIpProtocol(IpProtocol protocol);

    Tr SetToPort(int port);
    Tr SetFromPort(int port);
  }

}