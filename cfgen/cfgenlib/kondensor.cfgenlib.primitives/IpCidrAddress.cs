/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Represents an IPv4 IP address or CIDR range.
  /// </summary>
  public struct IpCidrAddress : IPrimitive
  {
    public byte[] Octets { get; private set; }
    public int Cidr { get; private set; }

    public void Write(StreamWriter output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: {Octets[0]}.{Octets[1]}.{Octets[2]}.{Octets[3]}/{Cidr}", indent);

    public void WritePrefixed(StreamWriter output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix}: {Octets[0]}.{Octets[1]}.{Octets[2]}.{Octets[3]}/{Cidr}", indent);

    public void SetCidrAndAddress(int cidr, params byte[] octets)
    {
      Cidr = cidr;
      Octets = OctetArray(octets);
    }

    public static IpCidrAddress AnyAddress()
    {
      IpCidrAddress address = new IpCidrAddress()
      {
        Octets = OctetArray(),
        Cidr = 0
      };
      return address;
    }

    public static byte[] OctetArray(params byte[] octets)
    {
      byte[] value = new byte[4];
      for(int index = 0; index < octets.Length && index < 4; index++)
        value[index] = octets.Length >= index ? octets[index] : (byte) 0;
      
      return value;
    }
  }

}