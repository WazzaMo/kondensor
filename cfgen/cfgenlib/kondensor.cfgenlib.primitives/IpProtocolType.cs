/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */


namespace kondensor.cfgenlib.primitives
{

  public enum IpProtocolType
  {
    UKNOWN_TRANSPORT,
    ALL_PROTOCOLS,
    ICMPv4,
    ICMPv6,
    TCP,
    UDP,
    CIFS_TCP,
    CIFS_UDP,
    SSH,
    SMTP,
    DNS_UDP,
    DNS_TCP,
    HTTP,
    POP3,
    IMAP,
    LDAP,
    LDAPS,
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
    RPC,
    POSTGRESQL,
    ORACLE_RDS,
    WINRM_HTTP,
    WINRM_HTTPS,
    ELASTIC_GRAPHICS
  } // -- IpProtocolType


}