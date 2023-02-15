/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib
{

  /// <summary>
  /// Properties for a resource that has the ability to export its
  /// definition for later use.
  /// </summary>
  public interface IResourceType
  {
    /// <summary>
    /// Resource type string for example, AWS::EC2::VPC
    /// This is used to generate the line.
    /// "Type" : "AWS::EC2::VPC"
    /// </summary>
    /// <value>Fixed string value</value>
    string Type { get; }

    /// <summary>
    /// The resource's ID
    /// </summary>
    /// <value>Id of the resource when it is defined in YAML.</value>
    string Id { get; }

    /// <summary>
    /// Accessor to the stored resource types.
    /// </summary>
    /// <value></value>
    Dictionary<string, ResourceProperty> Properties { get; }

    void setId(string id);

    /// <summary>
    /// Instructs the resource to create an output entry
    /// for the resource so it can be referred to in future
    /// templates.
    /// </summary>
    /// <param name="document">The document to that should have the output entry.</param>
    /// <param name="environment">Environment name for uniqueness</param>
    /// <param name="name">resource name</param>
    /// <param name="optionalText">List of strings in order: description, conditionId</param>
    IResourceType AddOutput(
      TemplateDocument document,
      string environment,
      string name,
      params string[] optionalText
    );

    /// <summary>
    /// Throws an exception if any of the required property values
    /// are missing from the resource.
    /// </summary>
    void AssertRequiredPropertiesSet();

    IResourceType AddTag(string key, string value);
  }

}