
using Optional;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib
{
  /// <summary>
  /// A resource type that has the ability to export its
  /// definition for later use.
  /// </summary>
  public interface IOutputResourceType : IResourceType
  {
    /// <summary>
    /// Instructs the resource to create an output entry
    /// for the resource so it can be referred to in future
    /// templates.
    /// </summary>
    /// <param name="document">The document to that should have the output entry.</param>
    /// <param name="environment">Environment name for uniqueness</param>
    /// <param name="name">resource name</param>
    /// <param name="optionalText">List of strings in order: description, conditionId</param>
    void AddOutput(
      TemplateDocument document,
      string environment,
      string name,
      params string[] optionalText
    );
  }
}