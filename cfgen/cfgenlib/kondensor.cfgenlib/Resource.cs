
using Optional;

using System.Collections.Generic;

namespace kondensor.cfgenlib
{

  public struct Resource
  {
    public string ResourceId;
    public IResourceType ResourceType;
    public List<ResourceProperty> Properties;

    public Resource(string id, IResourceType type, params ResourceProperty[] props)
    {
      ResourceId = id;
      ResourceType = type;
      Properties = new List<ResourceProperty>();

      foreach(var aProp in props){
        Properties.Add(aProp);
      }
    }
  }

}