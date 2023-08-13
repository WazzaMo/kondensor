/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Parser;

using Xunit;

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace test.Parser;

public class TestListReplace
{
  string[] TEST_DATA
    = {
      "Hello",
      "Good bye",
      "test value",
      "replacable",
      "word values"
    };
  
  List<string> _Subject;
  
  public TestListReplace()
  {
    _Subject = new List<string>(TEST_DATA);
  }

  [Fact]
  public void StringList_replaces_correctly()
  {
    bool result = _Subject.TryReplace(2, "new-value");
    Assert.True(result);
    Assert.Collection(_Subject,
      x => Assert.Equal(expected: "Hello", x),
      x => Assert.Equal(expected:"Good bye", x),
      x => Assert.Equal(expected:"new-value", x),
      x => Assert.Equal(expected:"replacable", x),
      x => Assert.Equal(expected:"word values", x)
    );
  }

  [Fact]
  public void StringList_replaces_zeroth_entry()
  {
    bool result = _Subject.TryReplace(atIndex: 0, "Hi");
    Assert.True(result);
    Assert.Collection(_Subject,
      x => Assert.Equal(expected: "Hi", x),
      x => Assert.Equal(expected:"Good bye", x),
      x => Assert.Equal(expected:"test value", x),
      x => Assert.Equal(expected:"replacable", x),
      x => Assert.Equal(expected:"word values", x)
    );
  }

  [Fact]
  public void StringList_replaces_last_entry()
  {
    bool result = _Subject.TryReplace(atIndex: 4, "new last");
    Assert.True(result);
    Assert.Collection(_Subject,
      x => Assert.Equal(expected:"Hello", x),
      x => Assert.Equal(expected:"Good bye", x),
      x => Assert.Equal(expected:"test value", x),
      x => Assert.Equal(expected:"replacable", x),
      x => Assert.Equal(expected:"new last", x)
    );
  }

  [Fact]
  public void StringList_fails_for_negative_index()
  {
    Assert.False( _Subject.TryReplace(atIndex: -1, "new-value") );
  }

  [Fact]
  public void StringList_fails_for_bad_index()
  {
    Assert.False( _Subject.TryReplace(atIndex: 5, "new-value") );
  }

}