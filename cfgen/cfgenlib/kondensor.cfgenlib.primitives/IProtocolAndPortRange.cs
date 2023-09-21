/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
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