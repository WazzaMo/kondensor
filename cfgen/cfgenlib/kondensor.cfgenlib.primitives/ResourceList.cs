/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.IO;
using System.Collections.Generic;

using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.writer;


namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Generalised list of inlined resource definitions that
  /// can be used in a parent resource definition.
  /// Example is a Security Group having multiple VpcIngress rules.
  /// </summary>
  public struct ResourceList<T> : IPrimitive where T : IResourceType
  {
    private List<T> _Resources;

    public void Write(ITextStream output, string name, string indent)
    {
      string _0_indent = indent,
             _1_indent = _0_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, message: $"{name}:", indent);
      WriteEntries(output, _1_indent);
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      string _0_indent = indent,
             _1_indent = _0_indent + YamlWriter.INDENT;
      YamlWriter.Write(output, message: prefix, indent);
      WriteEntries(output, _1_indent);
    }

    private void WriteEntries(ITextStream output, string indent)
    {
      string  _0_indent = indent,
              _1_indent = _0_indent + YamlWriter.INDENT;
      
      foreach(var instance in _Resources)
      {
        int entryNum = 0;
        foreach(string name in instance.Properties.Keys)
        {
          var entry = instance.Properties[name];
          entry.GetValue().MatchSome( primitive => {
            if (entryNum == 0)
              primitive.WritePrefixed(output, prefix: $"- {name}", _0_indent);
            else
              primitive.Write(output, name, _1_indent);
          });
          entryNum++;
        }
      }
    }

    public void Add(T resource) => _Resources.Add(resource);

    public ResourceList(params T[] resources)
    {
      _Resources = new List<T>();
      foreach(var instance in resources)
        if (instance != null)
          _Resources.Add(instance);
    }
  }

}