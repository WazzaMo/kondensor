/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System;

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{


  /// <summary>
  /// Represents a reference to an existing resource in the same
  /// generated YAML.
  /// </summary>
  public struct Ref : IReference
  {
    private string _Reference;

    public string Referenced => _Reference;

    public void Write(ITextStream output, string name, string indent)
      => YamlWriter.Write(output, $"{name}: !Ref {_Reference}", indent);

    public void WritePrefixed(ITextStream output, string prefix, string indent)
      => YamlWriter.Write(output, $"{prefix} !Ref {_Reference}", indent);

    public Ref(string reference)
    {
      if (String.IsNullOrEmpty(reference))
        throw new ArgumentException(
          message: "Valid referenced identifier expected.",
          reference == null ? new NullReferenceException() : new Exception("Zero length string given.")
        );
      _Reference = reference;
    }
  }


}