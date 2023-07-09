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
  private bool _LineEnded;

  public TextWriter()
  {
    _Text = new StringBuilder();
    _LineEnded = false;
  }

  public IPipeWriter WriteFragment(string fragment)
  {
    _Text.Append(fragment);
    _LineEnded = false;
    return this;
  }

  public IPipeWriter WriteFragmentLine(string fragment)
  {
    if (fragment.Length == 0)
    {
      if (! _LineEnded)
        _Text.AppendLine();
    }
    else
    {
      _Text.AppendLine(fragment);
    }
    _LineEnded = true;
    return this;
  }

  public bool IsLineTerminated() => _LineEnded;

  override public String ToString()
    => _Text.ToString();
}