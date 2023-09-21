/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// IPAM Pool ID is an optional parameter for creating
  /// a VPC.
  /// </summary>
  public enum IpamPoolIdValues
  {
    CidrBlock,
    Ipv4IpamPoolId
  }

}