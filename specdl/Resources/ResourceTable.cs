/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

namespace Resources;

public struct ResourceTable
{
  private class InternalData
  {
    internal List<string> _Headings = new List<string>();
    internal List<ResourceDefinition> _Resources = new List<ResourceDefinition>();
  }
  
  private InternalData _Data;

  public ResourceTable()
  {
    _Data = new InternalData();
  }

  public void AddHeading(string heading)
    => _Data._Headings.Add(heading);
  public IEnumerable<string> GetHeadings()
    => _Data._Headings.AsEnumerable();
  
  public void AddResourceDefinition(ResourceDefinition definition)
    => _Data._Resources.Add(definition);
  
  public IEnumerable<ResourceDefinition> GetResourceDefinitions()
    => _Data._Resources.AsEnumerable();
}