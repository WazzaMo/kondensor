
using Optional;
using System;
using System.IO;
using System.Collections.Generic;

using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.writer
{
  
  public struct ResourceWriter
  {
    public static StreamWriter Write(
      StreamWriter output,
      List<Resource> resources,
      string indent
    )
    {
      string _0_indent = indent;
      string _1_indent = _0_indent + YamlWriter.INDENT;
      string _2_indent = _1_indent + YamlWriter.INDENT;
      string _3_indent = _2_indent + YamlWriter.INDENT;

      if (resources.Count > 0)
      {
        YamlWriter.Write(output, $"Resources:", _0_indent);
        foreach(Resource resource in resources)
        {
          string type = resource.ResourceType.Type;

          YamlWriter.Write(output, $"{resource.ResourceId}:", _1_indent);
          YamlWriter.Write(output, $"Type: {type}", _2_indent);
          if (resource.Properties.Count > 0) {

            YamlWriter.Write(output, $"Properties:", _2_indent);
            foreach(var propKey in resource.Properties.Keys)
            {
              ResourceProperty resProp = resource.Properties[propKey];
              string name = resProp.Name;
              Option<IPrimitive> propValue = resProp.GetValue();
              propValue.MatchSome(
                primitive => {
                  primitive.Write(output, name, _3_indent);
                }
              );
            }
          }
        }

      }
      

      return output;
    }
  }

}