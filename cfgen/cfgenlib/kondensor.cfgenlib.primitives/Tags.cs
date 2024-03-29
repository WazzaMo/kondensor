/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Collections.Generic;

using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{
  public record struct Tag(string Name, string Value);


  public struct Tags : IPrimitive
  {
    public List<Tag> TagList;

    public void Write(ITextStream output, string name, string indent)
    {
      var _1_indent = indent + YamlWriter.INDENT;
      var _2_indent = _1_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, message: $"{name}:", indent);
      foreach(Tag tag in TagList)
      {
        YamlWriter.Write(output, message: $"- Key: {tag.Name}", _1_indent);
        YamlWriter.Write(output, message: $"Value: {tag.Value}", _2_indent);
      }
    }

    public void WritePrefixed(ITextStream output, string prefix, string indent)
    {
      var _1_indent = indent + YamlWriter.INDENT;
      var _2_indent = _1_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, message: $"{prefix}", indent);
      foreach(Tag tag in TagList)
      {
        YamlWriter.Write(output, message: $"- Key: {tag.Name}", _1_indent);
        YamlWriter.Write(output, message: $"Value: {tag.Value}", _2_indent);
      }
    }

    public Tags(params Tag[] tags)
    {
      TagList = tags.Length > 0 ? tags.ToList() : new List<Tag>();
    }
  }

}