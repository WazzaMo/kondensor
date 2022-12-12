
using Optional;
using System.IO;

using kondensor.cfgenlib;

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
      var child = indent + YamlWriter.INDENT;

      IResourceType resourceType = value.ResourceType;

      YamlWriter.Write(output, "Resources:", indent);
      YamlWriter.Write(output, $"Type: {resourceType.Name}", child);

      if (value.Properties.Count > 0) {
        string propChild = child + YamlWriter.INDENT;

        YamlWriter.Write(output, $"Properties:", child);
        foreach(var propKey in value.Properties.Keys)
        {
          ResourceProperty resProp = value.Properties[propKey];
          string name = resProp.Name;
          Option<string> propValue = resProp.GetValue<string>();
          propValue.MatchSome(
            valueString => YamlWriter.Write(output, $"{name}: {valueString}", propChild)
          );
        }
      }
      return output;
    }
  }

}