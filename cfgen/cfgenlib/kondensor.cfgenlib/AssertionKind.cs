/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */


namespace kondensor.cfgenlib
{

  public enum AssertionKind
  {
    ResourcePropertyNameMismatch,

    /// <summary>
    /// Type of value stored in the ResourceProperty
    /// does not match the expected primitive value.
    /// </summary>
    ResourcePropertyHeldTypeUnexpected,

    ///<summary>
    /// Indicates that the ResourceProperty type did not
    /// have a value set.
    ///</summary>
    ResourcePropertyAccessedWhenEmpty,
  }

}

  