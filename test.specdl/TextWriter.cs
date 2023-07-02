/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Text;

using Parser;

public struct TextWriter : IPipeWriter
{
  private StringBuilder _Text;

  public TextWriter()
  {
    _Text = new StringBuilder();
  }

  public IPipeWriter WriteFragment(string fragment)
  {
    _Text.Append(fragment);
    return this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    _Text.AppendLine(fragment);
    return this;
  }

  override public String ToString()
    => _Text.ToString();
}