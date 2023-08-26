/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.Parser;
using kondensor.Parser.HtmlParse;
using kondensor.Pipes;

using Xunit;
using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.IO;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace test.kondensor.Parser;

public class Test_ProductionWhileMatch
{
  private ReplayWrapPipe GetPipe()
  {
    HtmlPipe _base = new HtmlPipe(PipeValues.MULT_ROW_DATA_WITH_REPEAT, Console.Out);
    return new ReplayWrapPipe(_base);
  }

  private ParseAction GetParser()
  {
    var pipe = GetPipe();
    return Parsing.Group(pipe);
  }

  const string
    TD_ROWSPAN = "start:td:rowspan",
    TD_TEXT = "start:td:text",
    TD_EMPTY = "start:td:empty",
    END_TD = "end:td",
    START_P = "start:p",
    END_P = "end:p",
    START_AID = "start:a:id",
    START_AHREF = "start:a:href",
    END_A = "end:a";

  private ParseAction ResourceCondKeyDepProd(ParseAction parser)
    => parser
      // resource
      .Expect(HtmlRules.START_TD, TD_EMPTY)
        .MayExpect(HtmlRules.START_PARA, START_P)
          .MayExpect(HtmlRules.START_A_HREF, START_AHREF)
          .MayExpect(HtmlRules.END_A, END_A)
        .MayExpect(HtmlRules.END_PARA, END_P)
      .Expect(HtmlRules.END_TD, END_TD)
      // cond key
      .Expect(HtmlRules.START_TD, TD_EMPTY)
        .MayExpect(HtmlRules.START_PARA, START_P)
          .MayExpect(HtmlRules.START_A_HREF, START_AHREF)
          .MayExpect(HtmlRules.END_A, END_A)
        .MayExpect(HtmlRules.END_PARA, END_P)
      .Expect(HtmlRules.END_TD, END_TD)
      // dependent actions
      .Expect(HtmlRules.START_TD, TD_EMPTY).Expect(HtmlRules.END_TD, END_TD)
      ;

  private ParseAction TdRowspanProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ROWSPAN, TD_ROWSPAN)
        .Expect(HtmlRules.START_A_ID, START_AID).Expect(HtmlRules.END_A, END_A)
        .Expect(HtmlRules.START_A_HREF, START_AHREF).Expect(HtmlRules.END_A, END_A)
      .Expect(HtmlRules.END_TD, END_TD)
      .Expect(HtmlRules.START_TD_ROWSPAN, TD_ROWSPAN).Expect(HtmlRules.END_TD, END_TD)
      .Expect(HtmlRules.START_TD_ROWSPAN, TD_ROWSPAN).Expect(HtmlRules.END_TD, END_TD)
      // end declaration
      .Expect(ResourceCondKeyDepProd)
      ;

  private IEnumerable<string> FilterStarts(LinkedList<Matching> list)
  {
    var query = from node in list
      where (node.Annotation == TD_ROWSPAN
      || node.Annotation == START_AID
      || node.Annotation == START_AHREF
      || node.Annotation == TD_EMPTY
      || node.Annotation == START_P
      || node.Annotation == START_TR
      ) select node.Annotation;
    return query;
  }

  [Fact]
  public void matches_td_start_end_until_tr()
  {
    bool isParsed = false;

    var parser = GetParser();
    parser
      .Expect(SkipToTdRowspan)
      .ProductionWhileMatch(TdRowspanProd)
      .AllMatchThen( (list, _) => {
        isParsed = true;
        var starts = FilterStarts(list);

        Assert.Collection(starts,
          a0 => Assert.Equal(START_TR, a0),
          a1 => Assert.Equal(TD_ROWSPAN, a1),
          a2 => Assert.Equal(START_AID, a2),
          a3 => Assert.Equal(START_AHREF, a3),
          a4 => Assert.Equal(TD_ROWSPAN, a4),
          a5 => Assert.Equal(TD_ROWSPAN, a5),
          a6 => Assert.Equal(TD_EMPTY, a6),
          a7 => Assert.Equal(START_P, a7),
          a8 => Assert.Equal(START_AHREF, a8),
          a9 => Assert.Equal(TD_EMPTY, a9),
          a10 => Assert.Equal(TD_EMPTY, a10)
        );
      });
      Assert.True(isParsed);
  }

  private ParseAction RepeatedRowsProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TR, START_TR)
        .Expect(ResourceCondKeyDepProd)
      .Expect(HtmlRules.END_TR, END_TR)
      ;

  private ParseAction DeclarationAndRepeatedRows(ParseAction parser)
    => parser
      .Expect(TdRowspanProd)
      .Expect(HtmlRules.END_TR, END_TR)
      .ProductionWhileMatch(RepeatedRowsProd)
      ;

  [Fact]
  public void matches_rowspan_and_repeated_rows()
  {
    bool isParsed = false;

    var parser = GetParser();
    parser
      .Expect(SkipToTdRowspan)
      .Expect(DeclarationAndRepeatedRows)
      .AllMatchThen( (list, _) => {
        isParsed = true;
        var starts = FilterStarts(list);

        Assert.Collection(starts,
          a0 => Assert.Equal(START_TR, a0),
          a1 => Assert.Equal(TD_ROWSPAN, a1),
          a2 => Assert.Equal(START_AID, a2),
          a3 => Assert.Equal(START_AHREF, a3),
          a4 => Assert.Equal(TD_ROWSPAN, a4),
          a5 => Assert.Equal(TD_ROWSPAN, a5),
          a6 => Assert.Equal(TD_EMPTY, a6),
          a7 => Assert.Equal(START_P, a7),
          a8 => Assert.Equal(START_AHREF, a8),
          a9 => Assert.Equal(TD_EMPTY, a9),
          a10 => Assert.Equal(TD_EMPTY, a10),
          a11 => Assert.Equal(START_TR, a11),
          a12 => Assert.Equal(TD_EMPTY, a12),
          a13 => Assert.Equal(START_P, a13),
          a14 => Assert.Equal(START_AHREF, a14),
          a15 => Assert.Equal(TD_EMPTY, a15),
          a16 => Assert.Equal(TD_EMPTY, a16),
          a17 => Assert.Equal(START_TR, a17),
          a18 => Assert.Equal(TD_EMPTY, a18),
          a19 => Assert.Equal(TD_EMPTY,a19),
          a20 => Assert.Equal(START_P,a20),
          a21 => Assert.Equal(START_AHREF,a21),
          a22 => Assert.Equal(TD_EMPTY,a22)
        );
      });
      Assert.True(isParsed);
  }

//---------------------
  private ParseAction SkipToTdRowspan(ParseAction parser)
    => parser
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, THEAD)
      .Expect(HtmlRules.START_TR, START_TR)
      ;

  const string
    THEAD = "end:thead",
    START_TR = "start:tr",
    END_TR = "end:tr";
}