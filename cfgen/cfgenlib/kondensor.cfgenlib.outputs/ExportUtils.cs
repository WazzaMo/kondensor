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
    public static string ExportNameFor(string environment, IResourceType resource)
      => $"{environment}-{GetPrefixFrom(resource)}{resource.Id}";

    public static string ExportIdFor(string environment, IResourceType resource)
      => $"{GetPrefixFrom(resource)}{resource.Id}";

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