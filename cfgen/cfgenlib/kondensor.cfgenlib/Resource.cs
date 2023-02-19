
using Optional;

using System.Collections.Generic;

namespace kondensor.cfgenlib
{

  public struct Resource
  {
    public string ResourceId;
    public IResourceType ResourceType;
    public Dictionary<string, ResourceProperty> Properties => ResourceType.Properties;

    public Resource(string id, IResourceType type)
    {
      ResourceId = id;
      ResourceType = type;
    }

    public static readonly string DEFAULT_ID = "needs to be set";
  }

}