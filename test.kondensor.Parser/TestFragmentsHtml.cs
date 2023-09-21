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
using System.Linq;
using kondensor.Parser.AwsHtmlParse;
using System.Security.Principal;
using System.Net.Cache;

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

  [Fact]
  public void can_parse_first_action_declaration()
  {
    bool isMatched = false;
    Parser
      .Expect(TableStart)
      .Expect(ActionRow)
      .AllMatchThen( (list, _) => {
        var query = from token in list 
          where token.Annotation == ATN_VALUE_ACTION_NAME
            || token.Annotation == ATN_VALUE_DESCRIPTION
            || token.Annotation == ATN_ROWSPAN
            || token.Annotation == ATN_ID
            || token.Annotation == ATN_ACTION_HREF
            select token;

        isMatched = true;
        Assert.Collection( query,
          rowspan => {
            Assert.True( UtilsFragHtml.TryGetText(rowspan, "rowspanValue", out string span));
            Assert.Equal(expected:"2", span);
          },
          id => {
            Assert.True( UtilsFragHtml.TryGetText(id, key: "idValue", out string span));
            Assert.Equal(
              expected: "amazonplaygroundmanagement-AddCertificateToPlayGround",
              span
            );
          },
          href => {
            Assert.True( UtilsFragHtml.TryGetText(href, key: "hrefValue", out string value));
            Assert.Equal(
              expected: "https://docs.aws.amazon.com/playground/latest/api/PG_funtimes.html",
              value
            );
          },
          name => {
            Assert.True( UtilsFragHtml.TryGetText(name, key: "tagValue", out string tag));
            Assert.Equal(
              expected: "AddCertificateToPlayGround",
              tag
            );
          }
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
      .Expect(ActionsHeading)
      .ExpectProductionUntil(Heading, BasicFragmentsRules.END_TR, ATN_END_HEADING_ROW)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      .Expect(BasicFragmentsRules.END_THEAD)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      ;

  private ParseAction ActionsHeading(ParseAction parser)
    => parser
      .Expect(BasicFragmentsRules.START_TH, ATN_TABLE_HEADING_START)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      .Expect(BasicFragmentsRules.ACTIONS_TAG_VALUE, ATN_ACTIONS_HEADING)
      .Expect(BasicFragmentsRules.END_TH).Expect(BasicFragmentsRules.TAG_CLOSE)
      ;

  private ParseAction Heading(ParseAction parser)
    => parser
      .Expect(BasicFragmentsRules.START_TH, ATN_TABLE_HEADING_START)
      .Expect(BasicFragmentsRules.TAG_CLOSE)
      .Expect(BasicFragmentsRules.TAG_VALUE, ATN_TABLE_HEADING)
      .Expect(BasicFragmentsRules.END_TH).Expect(BasicFragmentsRules.TAG_CLOSE)
      ;

  private ParseAction ActionRow(ParseAction parser)
    => parser
      .Expect(BasicFragmentsRules.START_TR)
        .SkipUntil(BasicFragmentsRules.ROWSPAN_VALUE)
        .Expect(BasicFragmentsRules.ROWSPAN_VALUE, ATN_ROWSPAN)
        .SkipUntil(BasicFragmentsRules.ID_VALUE)
        .Expect(BasicFragmentsRules.ID_VALUE, ATN_ID)
        .SkipUntil(BasicFragmentsRules.START_A)
        .Expect(BasicFragmentsRules.START_A)
          .Expect(BasicFragmentsRules.HREF_VALUE, ATN_ACTION_HREF)
        .Expect(BasicFragmentsRules.TAG_CLOSE)
        .Expect(BasicFragmentsRules.TAG_VALUE, ATN_VALUE_ACTION_NAME)
      .SkipUntil(BasicFragmentsRules.END_TR)
      .Expect(BasicFragmentsRules.END_TR);

  private ParseAction Parser
    => Parsing.Group(_Pipe);

  const string
    ATN_START_TABLE = "table:action:start",
    ATN_END_TABLE = "table:action:end",
    ATN_HEADING_ROW = "table:heading:row:start",
    ATN_END_HEADING_ROW = "table:heading:row:end",
    ATN_TABLE_HEADING_START = "table:heading:start",
    ATN_TABLE_HEADING = "table:heading:value",
    ATN_ACTIONS_HEADING = "actions:heading:value",
    ATN_START_ACTIONROW = "row:action:start",
    ATN_END_ACTIONROW = "row:action:end",
    ATN_ROWSPAN = "rowspan:num",
    ATN_ID = "id:value",
    ATN_ACTION_HREF = "href:action",
    ATN_VALUE_ACTION_NAME = "value:action:name",
    ATN_VALUE_DESCRIPTION = "value:description",
    ATN_VALUE = "value:access-level";
}

