/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// This exception class should be the only class in the project
  /// other than static classes for extension methods.
  /// </summary>
  public class PropertyNeededException : System.Exception
  {
    public PropertyNeededException(string propertyName )
    : base(message: $"Property needed and missing: {propertyName}")
    {}
  }

}
