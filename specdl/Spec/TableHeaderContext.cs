/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System.Collections.Generic;

namespace Spec;

/// <summary>
/// A type that indicates what kind of table has been found.
/// </summary>
public record struct  TableHeaderContext: IContext //(TablePurpose Kind, string[] Headings, int LinesProcessed);
{
  public TablePurpose Kind;
  public List<string> Headings;

  public TableHeaderContext()
  {
    Kind = TablePurpose.Unknown;
    Headings = new List<string>();
  }
}