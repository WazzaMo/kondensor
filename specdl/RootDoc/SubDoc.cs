/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


namespace RootDoc;

/// <summary>
/// A subdoc has a relative path and a title.
/// </summary>
public record struct SubDoc(string Path, string Title);
