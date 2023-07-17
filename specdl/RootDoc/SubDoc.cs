/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Parser;

namespace RootDoc;

/// <summary>
/// A subdoc has a relative path and a title.
/// </summary>
public record struct SubDoc(string Path, string Title);
