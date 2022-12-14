
using Optional;
using System;
using System.IO;

using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.writer
{
  
  public struct ResourceWriter
  {
    public static StreamWriter Write(
      StreamWriter output,
      Resource value,
      string indent
    )
    {
      string _1_indent = indent + YamlWriter.INDENT;

      IResourceType resourceType = value.ResourceType;

      YamlWriter.Write(output, "Resources:", indent);
      YamlWriter.Write(output, $"Type: {resourceType.Name}", _1_indent);

      if (value.Properties.Count > 0) {
        string _2_indent = _1_indent + YamlWriter.INDENT;

        YamlWriter.Write(output, $"Properties:", _1_indent);
        foreach(var propKey in value.Properties.Keys)
        {
          ResourceProperty resProp = value.Properties[propKey];
          string name = resProp.Name;
          Console.WriteLine($"Prop: {name} hasVal? {resProp.IsSet()}");
          Option<IPrimitive> propValue = resProp.GetValue();
          propValue.MatchSome(
            primitive => {
              Console.WriteLine($"Prop-- {name} = {primitive.GetType().Name}");
              primitive.Write(output, name, _2_indent);
            }
          );
        }
      }
      return output;
    }
  }

}