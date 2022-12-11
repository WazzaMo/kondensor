using Optional;
using System;

namespace kondensor.cfgenlib
{

  public struct ResourceProperty
  {
    private object? Inner;

    public string Name { get; }

    public Option<T> GetValue<T>()
    {
      Option<T> value;
      if (Inner != null && Inner is Option<T> )
      {
        value = (Option<T>) Inner;
      }
      else 
      {
        value = Option.None<T>();
      }
      return value;
    }

    public void SetValue<T>(T value)
    {
      Type type = typeof(T);
      Inner = type.IsValueType
        ? Option.Some(value)
        : value != null
          ? Option.Some(value)
          : (object) Option.None<T>;
    }

    public bool Assign(ResourceProperty other)
    {
      if (other.Name == Name) {
        Inner = other.Inner;
        return true;
      }
      return false;      
    }

    public ResourceProperty(string name)
    {
      Name = name;
      Inner = null;
    }
  }

}