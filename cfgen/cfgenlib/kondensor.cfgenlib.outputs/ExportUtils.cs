/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Globalization;

namespace kondensor.cfgenlib.outputs
{

  /// <summary>
  /// Standard algorithms for determining an output's export ID
  /// and the output's export Name.
  /// </summary>
  public static class ExportUtils
  {
    /// <summary>
    /// The Id of the Output entry used for the export.
    /// </summary>
    /// <param name="environment">Environment name</param>
    /// <param name="resource"><see cref="IResourceType"/> declaration value to output.</param>
    /// <returns>String with standard output entry Id.</returns>
    public static string OutputIdFor(string environment, IResourceType resource)
      => $"{environment}Output{GetPrefixFrom(resource)}{resource.Id}";

    public static string ExportIdFor(string environment, IResourceType resource)
      => $"{environment}{GetPrefixFrom(resource)}{resource.Id}";

    public static string ExportIdFor<Tres>(string environment, string id, Tres dummyResource)
    where Tres : IResourceType
      => $"{environment}{GetPrefixFrom(dummyResource)}{id}";

    private static readonly TextInfo TEXT = CultureInfo.CurrentCulture.TextInfo;
    private static string GetPrefixFrom(IResourceType resource)
    {
      var typeParts = resource.Type.Split(separator: "::");
      if (typeParts != null && typeParts.Length > 0)
      {
        int numParts = typeParts.Length;
        var lowerPrefix = typeParts[numParts - 1].ToLower();
        return TEXT.ToTitleCase( lowerPrefix.ToLower() );
      }
      else
        throw new ArgumentException(message: $"Could not get valid Type string from {resource.GetType().Name}");
    }
  }

}