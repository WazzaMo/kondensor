

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

  