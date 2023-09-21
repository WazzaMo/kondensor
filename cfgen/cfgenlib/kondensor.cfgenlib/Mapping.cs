/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using Optional;

using System;
using System.Collections.Generic;

namespace kondensor.cfgenlib
{
  public struct Mapping
  {
    public LinkedList<MappingDef> Mappings;

    public Mapping(params MappingDef[] defs)
    {
      Mappings = new ();
      if (defs != null && defs.Length > 0)
      {
        foreach(var _def in defs)
          Mappings.AddLast(_def);
      }
    }

    public struct MappingDef
    {
      public string Name;
      public Dictionary<string, string> KeyValues;

      public MappingDef(string name)
      // public MappingDef(string name, Action<Dictionary<string,string>> init = null)
      {
        Name = name;
        KeyValues = new ();
        // if (init != null)
        //   init(KeyValues);
      }
    }
  }

}