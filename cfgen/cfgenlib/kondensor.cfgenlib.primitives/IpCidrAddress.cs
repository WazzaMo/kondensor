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
    {
      if (Octets.Length < 4)
        throw new OverflowException($"{nameof(IpCidrAddress)} write expects 4 octets but has {Octets.Length}");
      
      YamlWriter.Write(output, $"{name}: {Octets[0]}.{Octets[1]}.{Octets[2]}.{Octets[3]}/{Cidr}", indent);
    }

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

    public IpCidrAddress(int cidr, params byte[] octets)
    {
      Cidr = cidr;
      Octets = OctetArray(octets);
    }

    /// <summary>
    /// Copy and modify <see cref="IpCidrAddress"/> octets
    /// according to the patches given, if at all.
    /// Patching is optional.
    /// </summary>
    /// <param name="source">Source to copy from.</param>
    /// <param name="index">octet index to modify</param>
    /// <param name="patches">new octet value at index offset</param>
    /// <returns>copied value with any patches applied.</returns>
    public static IpCidrAddress CopyAndMod(IpCidrAddress source, params (int index, byte octet)[] patches)
    {
      byte[] octets = CopyOctets(source.Octets);
      for(int pNum = 0; pNum < patches.Length; pNum++)
      {
        (int index, byte modOctet) = patches[pNum];
        if (index >= octets.Length) throw new ArgumentException($"Patch index {index} too large.");
        octets[index] = modOctet;
      }
      return new IpCidrAddress(source.Cidr, octets);
    }

    const int EXPECTED_OCTET_LEN = 4;

    /// <summary>
    /// Copy array of octets in preparaton for modification.
    /// </summary>
    /// <param name="octets">Source octets to duplicate</param>
    /// <returns>New array with same values</returns>
    public static byte[] CopyOctets(byte[] octets)
    {
      byte[] result = new byte[EXPECTED_OCTET_LEN];
      octets.CopyTo(result, 0);
      return result;
    }

    /// <summary>
    /// Array created from parameter list of values.
    /// </summary>
    /// <param name="octets">parameter list to use for values</param>
    /// <returns>New octet array</returns>
    public static byte[] OctetArray(params byte[] octets)
      => CopyOctets(octets);
  }

}