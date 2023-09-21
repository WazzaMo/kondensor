/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System;
using kondensor.cfgenlib.writer;


namespace kondensor.cfgenlib.primitives
 {

  public struct IpProtocol : IPrimitive
  {
    public IpProtocolType Protocol;

    public void Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: {ProtocolToString()}", indent);


    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix}: {ProtocolToString()}", indent);

    public string ProtocolToString()
    {
      return Protocol switch 
      {
        IpProtocolType.TCP => "tcp",
        IpProtocolType.UDP => "udp",
        IpProtocolType.ICMPv4 => "icmp",
        IpProtocolType.ICMPv6 => "icmpv6",
        IpProtocolType.ALL_PROTOCOLS => "-1",
        _ => "ip"
      };
      // return Enum.Format(typeof(IpProtocolType), Protocol, "G");
    }

    public IpProtocol(IpProtocolType protocol)
    {
      Protocol = protocol;
    }

    public static IpProtocol TCP() => new IpProtocol(IpProtocolType.TCP);

    public static IpProtocol UDP() => new IpProtocol(IpProtocolType.UDP);

    public static IpProtocol ICMP() => new IpProtocol(IpProtocolType.ICMPv4);

    public static IpProtocol AllProtocols() => new IpProtocol(IpProtocolType.ALL_PROTOCOLS);
  } // -- end IpProtocol


 }