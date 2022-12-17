using Optional;
using System;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib
{

  public struct ResourceProperty
  {
    private Option<IPrimitive> Inner;

    public string Name { get; }

    public Option<IPrimitive> GetValue()
    {
      return Inner;
    }

    public ResourceProperty SetValue<T>(T value) where T : IPrimitive
    {
      Inner = Option.Some( (IPrimitive) value);
      return this;
    }

    public bool IsSet() => Inner.HasValue;

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
      Inner = Option.None<IPrimitive>();
    }
  }

}