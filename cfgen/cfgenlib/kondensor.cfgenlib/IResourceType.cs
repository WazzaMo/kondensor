
using Optional;
using System.Collections.Generic;


namespace kondensor.cfgenlib
{
  public interface IResourceType
  {
    string Name { get; }
    Dictionary<string, ResourceProperty> Properties { get; }
  }
}