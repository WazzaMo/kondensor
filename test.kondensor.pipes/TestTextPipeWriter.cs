/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Xunit;

using kondensor.Pipes;

using System;
using System.IO;

namespace test.kondensor.pipes;

public class TestTextPipeWriter
{
  private IPipeWriter Subject;
  private StringWriter _Data;

  public TestTextPipeWriter()
  {
    _Data = new StringWriter();
    Subject = new TextPipeWriter(_Data);
  }

  const string
    FRAGMENT_to_write = "Latte is a puppy",
    EXPECTED_fragment = "Latte is a puppy";

  [Fact]
  public void writing_fragment_does_not_terminate_line()
  {
    Subject.WriteFragment(FRAGMENT_to_write);
    var actual = _Data.ToString();
    Assert.Equal(EXPECTED_fragment, actual);
  }

  const string
    FRAGLINE_to_write = "Sky is blue",
    EXPECTED_fragline = @"Sky is blue
";

  [Fact]
  public void writing_fragmentline_terminates_the_line()
  {
    Subject.WriteFragmentLine(FRAGLINE_to_write);
    var actual = _Data.ToString();
    Assert.Equal(EXPECTED_fragline, actual);
  }

  const string
    EXPECTED_line_injected = FRAGLINE_to_write + @"
";

  [Fact]
  public void writing_fragment_causes_empty_WriteFragmentLine_to_inject_terminator()
  {
    Subject.WriteFragment(FRAGLINE_to_write);
    Subject.WriteFragmentLine(fragment: "");
    var actual = _Data.ToString();
    Assert.Equal(EXPECTED_line_injected, actual);
  }

  [Fact]
  public void writing_fragmentLine_causes_empty_WriteFragmentLine_to_not_inject_terminator()
  {
    Subject.WriteFragmentLine(FRAGLINE_to_write);
    Subject.WriteFragmentLine(fragment: "");
    var actual = _Data.ToString();
    Assert.Equal(EXPECTED_fragline, actual);
  }

  [Fact]
  public void writing_to_closedpipe_causes_exception()
  {
    Subject.WriteFragment(FRAGMENT_to_write);
    Subject.ClosePipe();
    Assert.Throws<InvalidOperationException>(
      () => Subject.WriteFragment(FRAGLINE_to_write)
    );
  }
}