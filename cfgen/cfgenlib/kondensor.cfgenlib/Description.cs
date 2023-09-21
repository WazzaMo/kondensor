/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

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