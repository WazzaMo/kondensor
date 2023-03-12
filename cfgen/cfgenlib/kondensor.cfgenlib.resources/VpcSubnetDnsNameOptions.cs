/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// Used to describe <see cref="AwsEc2Subnet"/> DNS options.
  /// Implemented like a primitive but is actually a component
  /// of a resource description.
  /// </summary>
  public struct VpcSubnetDnsNameOptions : IPrimitive
  {
    public static readonly string
      ENABLE_RESOURCE_NAME_IPV6_AAAA_RECORD = "EnableResourceNameDnsAAAARecord", // (true | false)
      ENABLE_RESOURCE_NAME_A_RECORD = "EnableResourceNameDnsARecord", // (true | false)
      HOSTNAME_TYPE = "HostnameType"; // (ip-name | resource-name)

    public enum HostnameType {
      IP_NAME,
      RESOURCE_NAME
    }

    private ResourceProperties _Properties;

    public void SetEnableResourceNameARecord(bool isEnable)
      => _Properties.SetProp<Bool>(ENABLE_RESOURCE_NAME_A_RECORD, new Bool(isEnable));

    public void SetHostnameType( HostnameType nameType )
    {
      EnumVal<HostnameType> value = new EnumVal<HostnameType>(nameType, (HostnameType t) => t switch {
        HostnameType.IP_NAME => "ip-name",
        HostnameType.RESOURCE_NAME => "resource-name",
        _ => "ip-name"
      });

      _Properties.SetProp<EnumVal<HostnameType>>(HOSTNAME_TYPE, value);
    }

    public void Write(ITextStream output, string name, string indent)
    {
      string _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT;
      
      YamlWriter.Write(output, $"{name}:", _0_indent);
      foreach( var propKey in _Properties.Properties.Keys)
      {
        ResourceProperty prop = _Properties.Properties[propKey];
        Option<IPrimitive> propVal = prop.GetValue();
        propVal.MatchSome( primitive => primitive.Write(output, prop.Name, _1_indent));
      }
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      // Not needed and should not be used.
      throw new NotImplementedException();
    }

    public VpcSubnetDnsNameOptions()
    {
      _Properties = new ResourceProperties(
        ENABLE_RESOURCE_NAME_IPV6_AAAA_RECORD,
        ENABLE_RESOURCE_NAME_A_RECORD,
        HOSTNAME_TYPE
      );
    }
  }

}