using Optional;

namespace kondensor.cfgenlib
{
  public struct Description
  {
    public Option<string> Text;

    public Description(Option<string> description)
    {
      Text = description;
    }
  }
}