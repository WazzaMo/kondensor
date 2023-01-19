/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using kondensor.cfgenlib.resources;

namespace kondensor.cfgenlib.primitives
{

  /// <summary>
  /// Utililty extension methods for supporting Tags.
  /// Internal to the library.
  /// </summary>
  public static class TagsUtils
  {
    /// <summary>
    /// Create a tag with given values
    /// and set or add the tags value to the
    /// properties.
    /// </summary>
    /// <param name="props"><see cref="ResourceProperties"/>to set/add Tags property</param>
    /// <param name="key">Tag key</param>
    /// <param name="value">Tag value</param>
    internal static void AddTag( this ResourceProperties props, string key, string value)
    {
      Tag tag = new Tag(key, value);
      if (! props.HasValue<Tags>("Tags"))
      {
        Tags tags = new Tags(tag);
        props.SetProp<Tags>("Tags", tags);
      }
      else
      {
        props.Access<Tags>("Tags", tags=> tags.TagList.Add(tag));
      }
    }
  }

}