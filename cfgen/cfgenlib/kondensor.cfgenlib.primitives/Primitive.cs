
using Optional;

namespace kondensor.cfgenlib.primitives
{

  public struct Values {
    public static string CidrBlock(int i1, int i2, int i3, int i4, int range)
      => $"{i1}.{i2}.{i3}.{i4}/{range}";
    
  }

}
