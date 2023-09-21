/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */


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