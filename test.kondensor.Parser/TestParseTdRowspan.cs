/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;
using kondensor.Pipes;

using Optional;
using Xunit;

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace test.kondensor.Parser;


public class TestParseTdRowspan
{
  const string
    ANNO_START_TR_DATA = "start:tr:data",
    ANNO_END_TR_DATA = "end:tr:data",
    ANNO_START_TD_TABINDEX = "start:td:tabindex",
    ANNO_START_TD_BORKED_AND_ROWSPAN = "start:td:borkedandrowspan",
    ANNO_START_TD_ROWSPAN_AND_TABINDEX = "start:td:rowspanandtabindex",
    ANNO_START_TD_EMTPY = "start:td:empty",
    ANNO_START_TD_ROWSPAN_AND_LATER = "start:td:rowspanandlater",
    ANNO_START_TD_NONSENSE = "start:td:nonsense",
    ANNO_START_TD_NO_ATTRIB_NO_VALUE = "start:td:no-attribs:no-value",
    ANNO_END_TD = "end:td",

    VAL_TABINDEX = "tabindex=1",
    VAL_BORKED_AND_ROWSPAN = "borked and rowspan=1",
    VAL_ROWSPAN_AND_TABINDEX = "rowspan=2 and tabindex",
    VAL_EMPTY = "Empty",
    VAL_ROWSPAN_AND_LATER = "rowspan=text and later",
    VAL_NONSENSE = "nonsense"
    ;

  private HtmlPipe _HtmlPipe;
  private ReplayWrapPipe _Pipe;
  private ParseAction _Parser;

  public TestParseTdRowspan()
  {
    _HtmlPipe = new HtmlPipe(ParserPipeValues.ONE_ROW_DATA_WITH_ODD_ATTIBS, Console.Out);
    _Pipe = new ReplayWrapPipe(_HtmlPipe);
    _Parser = Parsing.Group(_Pipe);
    _Parser.Expect(Production);
  }

  ParseAction Production(ParseAction parser)
    => parser
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE)
        .SkipUntil(HtmlRules.END_THEAD)
        .Expect(HtmlRules.END_THEAD)
        .Expect(HtmlRules.START_TR, ANNO_START_TR_DATA)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_TABINDEX)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_BORKED_AND_ROWSPAN)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_ROWSPAN_AND_TABINDEX)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_EMTPY)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_ROWSPAN_AND_LATER)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_NONSENSE)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .Expect(HtmlRules.START_TD_RS_VALUE, ANNO_START_TD_NO_ATTRIB_NO_VALUE)
            .Expect(HtmlRules.END_TD, ANNO_END_TD)
          .SkipUntil(HtmlRules.END_TR)
        .Expect(HtmlRules.END_TR, ANNO_END_TR_DATA)
      ;

  [Fact]
  public void common_production_parses_with_all_matches()
  {
    bool wasParsed = false;

    _Parser.MismatchesThen( (list, writer) => Assert.True(false));

    _Parser.AllMatchThen( (list, writer) => {
      wasParsed = true;
    });

    Assert.True( wasParsed);
  }

  [Fact]
  public void td_tabindex_only_can_be_collected()
  {
    bool wasParsed = false;

    _Parser.AllMatchThen( (list,_) => {
      wasParsed = true;

      var query = from node in list where node.Annotation == ANNO_START_TD_TABINDEX
        select node;
      
      Assert.Collection( query,
        node => {
          Assert.True( HtmlPartsUtils.TryGetTdValue(node, out string tdVal));
          Assert.Equal( expected: "tabindex=1", tdVal);
        }
      );
    });

    Assert.True( wasParsed );
  }

  [Fact]
  public void td_borked_and_rowspan_attribute_and_value_collected()
  {
    assert_values(
      ANNO_START_TD_BORKED_AND_ROWSPAN,
      VAL_BORKED_AND_ROWSPAN,
      rowspan: "1"
    );
  }

  [Fact]
  public void td_rowspan_and_tabindex_attribute_and_value_collected()
  {
    assert_values(
      ANNO_START_TD_ROWSPAN_AND_TABINDEX,
      VAL_ROWSPAN_AND_TABINDEX,
      rowspan: "2"
    );
  }

  [Fact]
  public void td_empty_can_give_empty_as_tag_value()
  {
    assert_values(
      ANNO_START_TD_EMTPY,
      VAL_EMPTY
    );
  }

  [Fact]
  public void td_rowspan_and_later_gives_text_as_rowspan_and_tag_value_can_be_collected()
  {
    assert_values(
      ANNO_START_TD_ROWSPAN_AND_LATER,
      VAL_ROWSPAN_AND_LATER,
      rowspan: "text"
    );
  }

  [Fact]
  public void td_nonsense_gives_no_attribute_and_nonsense_tagval_collected()
  {
    assert_values(
      ANNO_START_TD_NONSENSE,
      VAL_NONSENSE
    );
  }

  [Fact]
  public void td_without_attribs_and_tagValue_cannot_collect_values()
  {
    bool wasParsed = false;
    _Parser.AllMatchThen( (list, _) => {
      wasParsed = true;
      var query = from node in list
        where node.Annotation == ANNO_START_TD_NO_ATTRIB_NO_VALUE
        select node;
      
      Assert.Collection( query,
        item => {
          Assert.False( HtmlPartsUtils.TryGetTdRowspan(item, out string rsVal));
          Assert.False( HtmlPartsUtils.TryGetTdValue( item, out string tagVal));
        }
      );
    });

    Assert.True(wasParsed);
  }

  private void assert_values(
    string expectedAnnotation,
    string expectedTagValue,
    string? rowspan = null
  )
  {
    bool wasParsed = false;
    _Parser.AllMatchThen((list, _) =>
    {
      wasParsed = true;
      var query = from node in list
                  where node.Annotation == expectedAnnotation
                  select node;

      if (rowspan != null)
      {
        Assert.Collection(query,
          item => {
            Assert.True( HtmlPartsUtils.TryGetTdRowspan(item, out string rsValue));
            Assert.Equal(rowspan, rsValue);
          }
        );
      }

      var match = query.Last();
      Assert.True( HtmlPartsUtils.TryGetTdValue(match, out string tagVal));
      Assert.Equal(expectedTagValue, tagVal);
    });

    Assert.True(wasParsed);
  }
}