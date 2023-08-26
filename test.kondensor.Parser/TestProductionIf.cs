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

namespace test.kondensor.Parser;

public class TestProductionIf
{
  public TestProductionIf()
  {}

  const string
    HEADINGS = "end:headings",
    DESC_ONEROW = "start:td:onerow",
    DESC_MULTIROW = "start:td:multirow",
    START_ROW = "start:tr",
    END_ROW = "end:tr",
    DECL_ONEROW = "start:td:decl:onerow",
    DECL_MULTIROW = "start:td:decl:multirow",
    DECL_END = "end:td:decl";

  private ParseAction SkipHeadingsProd(ParseAction parser)
    => parser
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, HEADINGS)
      ;
  
  private ParseAction DescriptionOneRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_VALUE, DESC_ONEROW)
      .Expect(HtmlRules.END_TD)
      ;
  
  private ParseAction DescriptionMultiRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ROWSPAN, DESC_MULTIROW )
      .Expect(HtmlRules.END_TD)
      ;
  
  private ParseAction DeclOneRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD, DECL_ONEROW)
        .SkipUntil(HtmlRules.END_TD)
      .Expect(HtmlRules.END_TD, DECL_END)
      ;
  
  private ParseAction DeclMultiRowProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TD_ROWSPAN, DECL_MULTIROW)
        .SkipUntil(HtmlRules.END_TD)
      .Expect(HtmlRules.END_TD, DECL_END)
      ;
  
  private ParseAction RemainderActionOneRow(ParseAction parser)
    => parser
      .Expect(DescriptionOneRowProd)
      .SkipUntil(HtmlRules.END_TR)
      ;
    
  private ParseAction RemainderActionMultiRow(ParseAction parser)
    => parser
      .Expect(DescriptionMultiRowProd)
      .SkipUntil(HtmlRules.END_TR)
      ;
  
  private ParseAction ConditionalActionProd(ParseAction parser)
    => parser
      .Expect(HtmlRules.START_TR, START_ROW)
      .EitherProduction(DeclMultiRowProd, DeclOneRowProd)
      .IfThenProduction( m => m.Annotation == DECL_ONEROW, RemainderActionOneRow)
      .IfThenProduction( m => m.Annotation == DECL_MULTIROW, RemainderActionMultiRow)
      .Expect(HtmlRules.END_TR, END_ROW)
      ;
  
  private ReplayWrapPipe GetPipe(StringReader data)
  {
    HtmlPipe html;
    ReplayWrapPipe pipe;

    html = new HtmlPipe(data, Console.Out);
    pipe = new ReplayWrapPipe(html);
    return pipe;
  }

  [Fact]
  public void SingleRowAction_matches_as_singleRow()
  {
    ReplayWrapPipe pipe = GetPipe(PipeValues.ONE_ROW_DATA);

    var parser = Parsing.Group(pipe);
    bool isMatched = false;

    parser
      .Expect(SkipHeadingsProd)
      .Expect(ConditionalActionProd)
      .AllMatchThen( (list, writer) => {
        isMatched = true;

        var query = from node in list
          where node.Annotation == DECL_ONEROW || node.Annotation == DESC_ONEROW
          select node.Annotation;
        Assert.Collection(query,
          a1 => Assert.Equal(DECL_ONEROW, a1),
          a2 => Assert.Equal(DESC_ONEROW, a2)
        );
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void MultiRowAction_does_not_match_as_SingleRow()
  {
    ReplayWrapPipe pipe = GetPipe(PipeValues.MULT_ROW_DATA);

    var parser = Parsing.Group(pipe);
    bool isParsedOk = false;

    parser
      .Expect(SkipHeadingsProd)
      .Expect(ConditionalActionProd)
      .AllMatchThen( (list, writer) => {
        isParsedOk = true;

        var query = from node in list
          where node.Annotation == DECL_ONEROW || node.Annotation == DESC_ONEROW
          select node.Annotation;
        
        int len = query.Count();
        Assert.Equal(expected:0, len);
      });
    Assert.True(isParsedOk);
  }

  [Fact]
  public void MultiRowAction_matches_as_mutltiRow()
  {
    var pipe = GetPipe(PipeValues.MULT_ROW_DATA);

    var parser = Parsing.Group(pipe);
    bool isParsedOk = false;

    parser
      .Expect(SkipHeadingsProd)
      .Expect(ConditionalActionProd)
      .AllMatchThen( (list, writer) => {
        isParsedOk = true;

        var query = from node in list
          where node.Annotation == DECL_MULTIROW || node.Annotation == DESC_MULTIROW
          select node.Annotation;
        
        Assert.Collection( query,
          a1 => Assert.Equal(DECL_MULTIROW, a1),
          a2 => Assert.Equal(DESC_MULTIROW, a2)
        );
      });
    Assert.True(isParsedOk);
  }

  [Fact]
  public void SingleRowAction_does_not_match_as_mutltiRow()
  {
    var pipe = GetPipe(PipeValues.ONE_ROW_DATA);

    var parser = Parsing.Group(pipe);
    bool isParsedOk = false;

    parser
      .Expect(SkipHeadingsProd)
      .Expect(ConditionalActionProd)
      .AllMatchThen( (list, writer) => {
        isParsedOk = true;

        var query = from node in list
          where node.Annotation == DECL_MULTIROW || node.Annotation == DESC_MULTIROW
          select node.Annotation;
        
        int length = query.Count();
        Assert.Equal(expected: 0, length);
      });
    Assert.True(isParsedOk);
  }
}