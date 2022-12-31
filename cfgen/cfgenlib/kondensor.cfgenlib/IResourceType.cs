
using Optional;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib
{
  public interface IResourceType
  {
    /// <summary>
    /// Resource type string for example, AWS::EC2::VPC
    /// This is used to generate the line.
    /// "Type" : "AWS::EC2::VPC"
    /// </summary>
    /// <value>Fixed string value</value>
    string Type { get; }

    /// <summary>
    /// Accessor to the stored resource types.
    /// </summary>
    /// <value></value>
    Dictionary<string, ResourceProperty> Properties { get; }
  }
}