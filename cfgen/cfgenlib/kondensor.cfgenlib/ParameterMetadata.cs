/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using Optional;
using System;
using System.Collections.Generic;

namespace kondensor.cfgenlib
{

  public struct ParameterMetadata
  {
    public List<ParameterGroup> ParameterGroups;

    public ParameterMetadata(params ParameterGroup[] groups)
    {
      ParameterGroups = new List<ParameterGroup>();
      if (groups != null && groups.Length > 0)
      {
        foreach(var group in groups)
        {
          ParameterGroups.Add(group);
        }
      }
    }

    public record struct ParameterNameAndDescription(string Name, string Description);

    public struct ParameterGroup
    {
      public string Label;
      public List<ParameterNameAndDescription> Parameters;

      public ParameterGroup(string label, params ParameterNameAndDescription[] nameAndDescriptions)
      {
        if (string.IsNullOrEmpty(label)) {
          throw new ArgumentException(
            message: $"{nameof(ParameterGroup)} constructor argument {nameof(label)} label requires a non-null, non-empty string value."
          );
        }
        Label = label;
        Parameters = nameAndDescriptions.ToList();
      }
    } // - ParameterGroup

  }

}