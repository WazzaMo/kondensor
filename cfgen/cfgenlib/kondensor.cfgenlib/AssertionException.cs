using System;

namespace kondensor.cfgenlib
{

  /// <summary>
  /// Fundamental assertion failure indicating that the
  /// API for cfgen was misused.
  /// </summary>
  [System.Serializable]
  public class AssertionException : System.Exception
  {
    private AssertionKind _TypeOfAssertion;

    public AssertionKind Reason => _TypeOfAssertion;

    public AssertionException(AssertionKind kind, string message)
    : base(message)
    {
      _TypeOfAssertion = kind;
    }

    public AssertionException(AssertionKind kind, string message, System.Exception inner)
    : base(message, inner)
    {
      _TypeOfAssertion = kind;
    }

    protected AssertionException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context
    ) : base(info, context)
    { }

  }

}