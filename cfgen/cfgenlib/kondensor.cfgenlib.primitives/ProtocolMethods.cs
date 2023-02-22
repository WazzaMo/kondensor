/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Support methods for working with IP protocols.
  /// </summary>
  public static class ProtocolMethods
  {
    /// <summary>
    /// For a given, known IP protocol, sets the transport (TCP/UDP) and port ranges.
    /// <paramref name="self"/>The base value on which to set protocol transport and port range</paramref>
    /// <paramref name="protocolType"/>Type of protocol to look up</paramref>
    /// <returns>
    ///   A protocol and range type with protocol, fromPort and toPort set if protocolType is valid.
    ///   If invalid, the IpProtocol, fromPort and toPort will not be set.
    /// </returns>
    /// </summary>
    public static T SetProtocolAndPortRange<T>(this T self, IpProtocolType protocolType) where T : struct, IResourceType, IProtocolAndPortRange<T>
    {
      var value = self;
      int fromPort = ProtocolMethods.MinPortForProtocol(protocolType);
      int ToPort = ProtocolMethods.MaxPortForProtocol(protocolType);
      if (fromPort > 0 || ToPort > 0)
      {
        value.SetIpProtocol(ProtocolMethods.TransportForType(protocolType) );
        value.SetFromPort(fromPort);
        value.SetToPort(ToPort);
      }
      return value;
    }

    public static int MinPortForProtocol( IpProtocolType protocol)
      => protocol switch {

        IpProtocolType.ALL_PROTOCOLS => 0,
        IpProtocolType.SSH => 22,
        IpProtocolType.CIFS_UDP => 137,
        IpProtocolType.CIFS_TCP => 139,
        IpProtocolType.DNS_TCP => 53,
        IpProtocolType.DNS_UDP => 53,
        IpProtocolType.HTTP => 80,
        IpProtocolType.HTTPS => 443,
        IpProtocolType.LDAP => 389,
        IpProtocolType.LDAPS => 636,
        IpProtocolType.MSSQL => 1433,
        IpProtocolType.MYSQL_AURORA => 3306,
        IpProtocolType.ELASTIC_GRAPHICS => 2007,
        IpProtocolType.NFS => 2049,
        IpProtocolType.ORACLE_RDS => 2483,
        IpProtocolType.IMAP => 143,
        IpProtocolType.IMAPS => 993,
        IpProtocolType.POP3 => 110,
        IpProtocolType.POP3S => 995,
        IpProtocolType.POSTGRESQL => 5432,
        IpProtocolType.RDP => 3389,
        IpProtocolType.REDSHIFT => 5439,
        IpProtocolType.RPC => 530,
        IpProtocolType.SMB => 137,
        IpProtocolType.SMTP => 25,
        IpProtocolType.SMTPS => 465,
        IpProtocolType.WINRM_HTTP => 5985,
        IpProtocolType.WINRM_HTTPS => 5986,
        _ => 0
      };
    
    public static int MaxPortForProtocol(IpProtocolType protocol)
      => protocol switch {
        IpProtocolType.ALL_PROTOCOLS => 65535,
        IpProtocolType.SSH => 22,
        IpProtocolType.CIFS_UDP => 138,
        IpProtocolType.CIFS_TCP => 445,
        IpProtocolType.DNS_TCP => 53,
        IpProtocolType.DNS_UDP => 53,
        IpProtocolType.HTTP => 80,
        IpProtocolType.HTTPS => 443,
        IpProtocolType.LDAP => 389,
        IpProtocolType.LDAPS => 636,
        IpProtocolType.MSSQL => 1433,
        IpProtocolType.MYSQL_AURORA => 3306,
        IpProtocolType.ELASTIC_GRAPHICS => 2007,
        IpProtocolType.NFS => 2049,
        IpProtocolType.ORACLE_RDS => 2483,
        IpProtocolType.IMAP => 143,
        IpProtocolType.IMAPS => 993,
        IpProtocolType.POP3 => 110,
        IpProtocolType.POP3S => 995,
        IpProtocolType.POSTGRESQL => 5432,
        IpProtocolType.RDP => 3389,
        IpProtocolType.REDSHIFT => 5439,
        IpProtocolType.RPC => 530,
        IpProtocolType.SMB => 445,
        IpProtocolType.SMTP => 25,
        IpProtocolType.SMTPS => 587,
        IpProtocolType.WINRM_HTTP => 5985,
        IpProtocolType.WINRM_HTTPS => 5986,
        _ => 0
      };

    public static IpProtocol TransportForType(IpProtocolType protocolType)
      => new IpProtocol(ProtocolMethods.TransportForProtocol(protocolType));

    public static IpProtocolType TransportForProtocol(IpProtocolType protocol)
      => protocol switch {
        IpProtocolType.ALL_PROTOCOLS => IpProtocolType.ALL_PROTOCOLS,
        IpProtocolType.SSH =>      IpProtocolType.TCP,
        IpProtocolType.CIFS_UDP => IpProtocolType.UDP,
        IpProtocolType.CIFS_TCP => IpProtocolType.TCP,
        IpProtocolType.DNS_TCP =>  IpProtocolType.TCP,
        IpProtocolType.DNS_UDP =>  IpProtocolType.UDP,
        IpProtocolType.HTTP =>     IpProtocolType.TCP,
        IpProtocolType.HTTPS =>    IpProtocolType.TCP,
        IpProtocolType.LDAP =>     IpProtocolType.TCP,
        IpProtocolType.LDAPS =>    IpProtocolType.TCP,
        IpProtocolType.MSSQL =>    IpProtocolType.TCP,
        IpProtocolType.MYSQL_AURORA => IpProtocolType.TCP,
        IpProtocolType.ELASTIC_GRAPHICS => IpProtocolType.TCP,
        IpProtocolType.NFS =>      IpProtocolType.UDP,
        IpProtocolType.ORACLE_RDS => IpProtocolType.TCP,
        IpProtocolType.IMAP =>     IpProtocolType.TCP,
        IpProtocolType.IMAPS =>    IpProtocolType.TCP,
        IpProtocolType.POP3 =>     IpProtocolType.TCP,
        IpProtocolType.POP3S => IpProtocolType.TCP,
        IpProtocolType.POSTGRESQL => IpProtocolType.TCP,
        IpProtocolType.RDP => IpProtocolType.TCP,
        IpProtocolType.REDSHIFT => IpProtocolType.TCP,
        IpProtocolType.RPC => IpProtocolType.UDP,
        IpProtocolType.SMB => IpProtocolType.TCP,
        IpProtocolType.SMTP => IpProtocolType.TCP,
        IpProtocolType.SMTPS => IpProtocolType.TCP,
        IpProtocolType.WINRM_HTTP => IpProtocolType.TCP,
        IpProtocolType.WINRM_HTTPS => IpProtocolType.TCP,
        _ => IpProtocolType.UKNOWN_TRANSPORT
      };
  }


}