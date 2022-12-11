
using kondensor.cfgenlib;

namespace kondensor.cfgenlib.resources {

  public struct BaseResourceType
  {
    public static void SetProp(ResourceProperty prop, Dictionary<string, ResourceProperty> _Properties)
    {
      if (_Properties.ContainsKey(prop.Name))
        _Properties[prop.Name].Assign(prop);
    }

    public static Dictionary<string, ResourceProperty> DeclareProperties(params string[] props)
    {
      Dictionary<string, ResourceProperty> _Properties;

      _Properties = new Dictionary<string, ResourceProperty>();
      foreach(string name in props) {
        _Properties.Add(name, new ResourceProperty(name));
      }
      return _Properties;
    }

  }
}