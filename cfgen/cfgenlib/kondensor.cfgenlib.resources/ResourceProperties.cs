
using Optional;

using System;
using System.Collections.Generic;

using kondensor.cfgenlib;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources {

  public struct ResourceProperties
  {
    private Dictionary<string, ResourceProperty> _Properties;

    public Dictionary<string, ResourceProperty> Properties => _Properties;


    public ResourceProperties(params string[] props)
    {
      _Properties = DeclareProperties(props);
    }

    public void SetProp<T>(string name, T value) where T : IPrimitive
    {
      if (_Properties.ContainsKey(name))
      {
        _Properties[name] = _Properties[name].SetValue<T>(value);
      }
      else
      {
        throw new AssertionException(
          AssertionKind.ResourcePropertyNameMismatch, 
          message: $"Property of name {name} is not defined."
        );
      }
    }

    public void Access<T>(string name, Action<T> accessor) where T: IPrimitive
    {
      if (_Properties.ContainsKey(name) && _Properties[name].IsSet() )
      {
        String propName = name;

        _Properties[name].GetValue().Match(
          some: (IPrimitive primitive) => {
              if (typeof(T) != primitive.GetType())
              {
                throw new AssertionException(
                  AssertionKind.ResourcePropertyHeldTypeUnexpected,
                  message: $"ResourceProperty[{propName}] has type {primitive.GetType().Name} but {typeof(T).Name} requested."
                );
              }
              accessor( (T) primitive);
            },
          none: () => throw new AssertionException(
              AssertionKind.ResourcePropertyHeldTypeUnexpected,
              message: $"ResourceProperty [{propName}] not set to a value but access was attempted."
            )
        );
      }
    }

    public bool HasValue<T>(string name) where T : IPrimitive
    {
      if (_Properties.ContainsKey(name) && _Properties[name].IsSet()) {
        Type? heldType = null;
        _Properties[name].GetValue().MatchSome( primitives => heldType = primitives.GetType());
        return (heldType == typeof(T) );
      }
      return false;
    }

    /// <summary>
    /// Convenience method for asserting required properties.
    /// </summary>
    /// <param name="name">Name of property to assert existence.</param>
    /// <typeparam name="T">Expected primitive type of property.</typeparam>
    public void AssertHasValue<T>(string name) where T : IPrimitive
    {
      if (! HasValue<T>(name))
        throw new PropertyNeededException(name);
    }

    public void AssertHasOnlyOneOf(params string[] names)
    {
      List<string> matches = new();

      for(int index = 0; index < names.Length; index++)
      {
        if (_Properties.ContainsKey(names[index]))
          matches.Add(names[index]);
      }
      if (matches.Count > 1)
        throw new PropertyNeededException($"Only one of {matches}");
    }

    private static Dictionary<string, ResourceProperty> DeclareProperties(string[] props)
    {
      Dictionary<string, ResourceProperty> _Properties;

      _Properties = new Dictionary<string, ResourceProperty>();
      foreach(string name in props) {
        if (_Properties.ContainsKey(name))
          throw new ArgumentException($"Property named '{name}' already exists.");
        _Properties.Add(name, new ResourceProperty(name));
      }
      return _Properties;
    }

  }
}