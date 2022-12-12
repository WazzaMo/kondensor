
namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Common interface for primitives that can be
  /// tagged.
  /// </summary>
  public interface IHasTags
  {
    void AddTag(string key, string value);
  }

}