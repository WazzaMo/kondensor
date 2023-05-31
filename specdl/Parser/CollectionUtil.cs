/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser;

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
}