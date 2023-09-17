/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Xunit;

using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse.Frag;

using test.kondensor.fixtures;

using System;
using System.Text.RegularExpressions;
using System.Linq;
using kondensor.Parser.AwsHtmlParse;

namespace test.kondensor.Parser;

public class TestFragmentsHtml
{
  private FragHtmlPipe _FragPipe;
  private ReplayWrapPipe _Pipe;

  public TestFragmentsHtml()
  {
    var writer = new TextPipeWriter(Console.Out);
    _FragPipe = new FragHtmlPipe(FixtureFragHtml.HtmlToFrag(), writer);
    _Pipe = new ReplayWrapPipe(_FragPipe);
  }

  [Fact]
  public void can_parse_headings()
  {
    bool isMatched = false;

    Parser.Expect(TableStart)
      .AllMatchThen( (list, _) => {
        isMatched = true;
        var values = list
          .Where( x => x.HasNamedGroups )
          .Select( x => {
            x.TryGetNamedPart("tagValue", out string value);
            return value;
          });
        Assert.Collection(values,
          a => Assert.Equal(expected: "Actions", a),
          a => Assert.Equal(expected: "Description", a),
          a => Assert.Equal(expected: "Access level", a),
          a => Assert.Equal(expected: "Resource types (*required)", a),
          a => Assert.Equal(expected: "Condition keys", a),
          a => Assert.Equal(expected: "Dependent actions", a)
        );
      });
    
    Assert.True(isMatched);
  }

  private ParseAction TableStart(ParseAction parser)
    => parser
      .SkipUntil(BasicFragmentsRules.START_TABLE)
      .Expect(BasicFragmentsRules.START_TABLE, ATN_TABLE_HEADING_START)
      .SkipUntil(BasicFragmentsRules.START_TR)
      .Expect(BasicFragmentsRules.START_TR, ATN_HEADING_ROW)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      .ExpectProductionUntil(Heading, BasicFragmentsRules.END_TR, ATN_END_HEADING_ROW)
      ;

  private ParseAction Heading(ParseAction parser)
    => parser
      .Expect(BasicFragmentsRules.START_TH, ATN_TABLE_HEADING_START)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      .Expect(BasicFragmentsRules.TAG_VALUE, ATN_TABLE_HEADING)
      .Expect(BasicFragmentsRules.END_TH).Expect(BasicFragmentsRules.TAG_CLOSE)
      ;

  private ParseAction Parser
    => Parsing.Group(_Pipe);

  const string
    ATN_START_TABLE = "table:action:start",
    ATN_END_TABLE = "table:action:end",
    ATN_HEADING_ROW = "table:heading:row:start",
    ATN_END_HEADING_ROW = "table:heading:row:end",
    ATN_TABLE_HEADING_START = "table:heading:start",
    ATN_TABLE_HEADING = "table:heading:value";
    
}

