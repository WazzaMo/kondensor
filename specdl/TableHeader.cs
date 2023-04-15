/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


/// <summary>
/// A type that indicates what kind of table has been found.
/// </summary>
public record struct  TableHeader (TablePurpose Kind, string[] Headings, int LinesProcessed);