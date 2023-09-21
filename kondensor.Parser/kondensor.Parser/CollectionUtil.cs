/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace kondensor.Parser;

/// <summary>
/// Utilities to make collections easier to work with.
/// </summary>
public static class CollectionUtil
{
  /// <summary>
  /// For looping through LINQ results.
  /// </summary>
  /// <param name="collection"><see href="IEnumerable<T>"/> collection, mostly from LINQ</param>
  /// <param name="job">work to be done on each item.</param>
  /// <typeparam name="T">Type of the value in the collection.</typeparam>
  /// <returns>The same collection back for any further work.</returns>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T, int> job)
  {
    int index;

    for(index = 0; index < collection.Count(); index++)
    {
      job(collection.ElementAt(index), index);
    }
    return collection;
  }

  /// <summary>
  /// Extension method for List<T> to support replacing an element at a given index.
  /// </summary>
  /// <param name="original">original list</param>
  /// <param name="atIndex">Index (int) </param>
  /// <param name="replacement">Value to use in replacing indexed element</param>
  /// <typeparam name="T">Valuetype of the list.</typeparam>
  /// <returns>True if successful, false on failure</returns>
  public static bool TryReplace<T>(this List<T> original, int atIndex, T replacement)
  {
    bool result = atIndex >= 0 && atIndex < original.Count;
    if (result)
    {
      original.Insert(atIndex, replacement);
      original.RemoveAt(atIndex + 1);
    }
    return result;
  }
}