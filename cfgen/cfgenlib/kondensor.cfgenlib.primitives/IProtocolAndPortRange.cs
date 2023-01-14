/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */



namespace kondensor.cfgenlib.primitives
{

  public interface IProtocolAndPortRange
  {
    void SetIpProtocol(IpProtocol protocol);

    void SetToPort(int port);
    void SetFromPort(int port);
  }

}