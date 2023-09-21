/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using Optional;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib
{

  /// <summary>
  /// Represents the CloudFormation header
  /// sequence and the description of the template.
  /// </summary>
  public struct Header
  {
    public Text Description;

    public void SetDescription(string header)
      => Description = new Text(header);

    public Header(string description)
    {
      Description = default;
      if (string.IsNullOrEmpty(description))
      {
        SetDescription("template generated with Kondensor.");
      }
      else
      {
        SetDescription(description);
      }
    }
  }

}