

using Optional;


namespace kondensor.cfgenlib
{

  /// <summary>
  /// Represents the CloudFormation header
  /// sequence and the description of the template.
  /// </summary>
  public struct Header
  {
    public Option<string> Description;

    public Header(string description)
    {
      if (string.IsNullOrEmpty(description))
      {
        Description = Option.Some("template generated with Kondensor.");
      }
      else
      {
        Description = Option.Some(description);
      }
    }
  }

}