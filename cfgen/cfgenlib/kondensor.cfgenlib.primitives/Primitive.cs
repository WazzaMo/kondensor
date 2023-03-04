
using Optional;
using System.Collections.Generic;


namespace kondensor.cfgenlib.primitives
{

  public struct Primitive {
    public static string CidrBlock(int i1, int i2, int i3, int i4, int range)
      => $"{i1}.{i2}.{i3}.{i4}/{range}";
    
    /// <summary>
    /// Utility that converts an array or variable length parameter list
    /// into a list<T> of primitives.
    /// </summary>
    /// <param name="convert">Converter or casting function</param>
    /// <param name="values">Values to convert into list</param>
    /// <typeparam name="Traw">Raw value type like int, string etc.</typeparam>
    /// <typeparam name="Tp">Primitive value type, like IntNumber, Text etc</typeparam>
    /// <returns>System.Collection.Generic.List<Tp> list of values</returns>
    public static List<Tp> ConvertToPrimitive<Traw, Tp>(Func<Traw,Tp> convert, params Traw[] values)
    where Tp : struct, IPrimitive
    {
      List<Tp> primitives = new List<Tp>();

      for(int index = 0; index < values.Length; index++)
      {
        primitives.Add(convert(values[index]));
      }
      return primitives;
    }
  }

}
