/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using kondensor.cfgenlib.writer;


namespace kondensor.cfgenlib.primitives
 {

  public struct IpProtocol : IPrimitive
  {
    public IpProtocolType Protocol;

    public void Write(StreamWriter output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: {ProtocolToString()}", indent);


    public void WritePrefixed(StreamWriter output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix}: {ProtocolToString()}", indent);

    public string ProtocolToString()
    {
      return Enum.Format(typeof(IpProtocolType), Protocol, "G");
    }

    public IpProtocol(IpProtocolType protocol)
    {
      Protocol = protocol;
    }

    public enum IpProtocolType
    {
      ICMPv4,
      ICMPv6,
      TCP,
      UDP,
      SSH,
      SMTP,
      DNS_UDP,
      DNS_TCP,
      HTTP,
      POP3,
      IMAP,
      LDAP,
      HTTPS,
      SMB,
      SMTPS,
      IMAPS,
      POP3S,
      MSSQL,
      NFS,
      MYSQL_AURORA,
      RDP,
      REDSHIFT,
      POSTGRESQL,
      ORACLE_RDS,
      WINRM_HTTP,
      WINRM_HTTPS,
      ESASTIC_GRAPHICS
    } // -- IpProtocolType
  } // -- end IpProtocol


 }