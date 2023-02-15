/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// This exception class should be the only class in the project
  /// other than static classes for extension methods.
  /// </summary>
  public class PropertyNeededExcetpion : System.Exception
  {
    public PropertyNeededExcetpion(string propertyName )
    : base(message: $"Property needed and missing: {propertyName}")
    {}
  }

}
