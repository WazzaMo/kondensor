
using Optional;
using kondensor.cfgenlib;

namespace kondensor.cfgenlib.api
{

  public struct Primitive {
    public static Option<string> CidrBlock(int i1, int i2, int i3, int i4, int range)
      => CreateValue($"{i1}.{i2}.{i3}.{i4}/{range}");
    
    private static Option<string> CreateValue(string value)
    {
      return string.IsNullOrEmpty(value) ? Option.None<string>() : Option.Some(value);
    }

    private static Option<T> CreateValue<T>(T value) where T : struct
    {
      return Option.Some<T>(value);
    }
  }

}
