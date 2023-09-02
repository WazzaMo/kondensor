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
    ANNO_END_TD = "end:td"
    ;

  private HtmlPipe _HtmlPipe;
  private ReplayWrapPipe _Pipe;
  private ParseAction _Parser;

  public TestParseTdRowspan()
  {
    _HtmlPipe = new HtmlPipe(PipeValues.ONE_ROW_DATA_WITH_ODD_ATTIBS, Console.Out);
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
        node => Assert.Equal( expected: "tabindex=1", HtmlPartsUtils.GetTdValue( node))
      );
    });

    Assert.True( wasParsed );
  }
}