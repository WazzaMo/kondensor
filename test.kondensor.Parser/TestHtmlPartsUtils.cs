/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.HtmlParse;

using Optional;
using Xunit;

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace test.kondensor.Parser;

public class TestHtmlPartsUtils
{
  private HtmlPipe _HtmlPipe;
  private ReplayWrapPipe _Pipe;

  public TestHtmlPartsUtils()
  {
    Console.Out.Flush();
    _HtmlPipe = new HtmlPipe(PipeValues.HTML, Console.Out);
    _Pipe = new ReplayWrapPipe(_HtmlPipe);
  }

  [Fact]
  public void GetTableAttribName_returnsAttribName()
  {
    const string EXPECTED_ATTRIB_NAME = "id";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_ATTRIB_NAME, HtmlPartsUtils.GetTableAttribName(matching.Parts));
        isMatched = true;
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void GetTableAttribValue_returnsAttribValue()
  {
    const string EXPECTED_ATTRIB_VALUE = "w43aab5b9c19c11c11";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_ATTRIB_VALUE, HtmlPartsUtils.GetTableAttribValue(matching.Parts));
        isMatched = true;
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void GetThTagValue_returnsTagValue()
  {
    const string EXPECTED_VALUE = "Actions";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.START_TH_VALUE)
      .Expect(HtmlRules.START_TH_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetThTagValue(matching.Parts));
        isMatched = true;
      });
    Assert.True(isMatched);
  }

  [Fact]
  public void GetTdAttribName_returnsAttribName()
  {
    const string EXPECTED_VALUE = "rowspan";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
      .SkipUntil(HtmlRules.END_TR)
      .Expect(HtmlRules.END_TR, annotation: "end:tr-firstRow")
      .Expect(HtmlRules.START_TR, annotation: "start:tr-secondRow")
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetTdAttribName(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetTdAttribValue_returnsAttribValue()
  {
    const string EXPECTED_VALUE = "3";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
      .SkipUntil(HtmlRules.END_TR)
      .Expect(HtmlRules.END_TR, annotation: "end:tr-firstRow")
      .Expect(HtmlRules.START_TR, annotation: "start:tr-secondRow")
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Assert.NotEmpty(query);
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetTdAttribValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetTdAttribIntValue_returnsNumericValue()
  {
    const int EXPECTED_VALUE = 3;
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
      .SkipUntil(HtmlRules.END_TR)
      .Expect(HtmlRules.END_TR, annotation: "end:tr-firstRow")
      .Expect(HtmlRules.START_TR, annotation: "start:tr-secondRow")
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        int rowspan = HtmlPartsUtils.GetTdAttribIntValue(matching.Parts);
        Assert.False(rowspan.IsEmptyIntValue());
        Assert.Equal(EXPECTED_VALUE, rowspan);
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetTdTagValue_returnsTagValue()
  {
    const string EXPECTED_VALUE = "Grants permission to close an account";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
        .Expect(HtmlRules.START_TD_VALUE, annotation: "start:td-firstCell")
          .Expect(HtmlRules.START_A_ID, annotation: "start:a-id").Expect(HtmlRules.END_A)
          .Expect(HtmlRules.START_A_HREF, annotation: "start:a-href").Expect(HtmlRules.END_A)
        .Expect(HtmlRules.END_TD, annotation: "end:td-firstCell")
        .Expect(HtmlRules.START_TD_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetTdTagValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetAIdAttribValue_returnIdValue()
  {
    const string EXPECTED_VALUE = "awsaccountmanagement-CloseAccount";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
        .Expect(HtmlRules.START_TD_VALUE, annotation: "start:td-firstCell")
          .Expect(HtmlRules.START_A_ID, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetAIdAttribValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetAHrefAttribValue_returnsAttribValue()
  {
    const string EXPECTED_VALUE = "https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
        .Expect(HtmlRules.START_TD_VALUE, annotation: "start:td-firstCell")
          .Expect(HtmlRules.START_A_ID, annotation: "start:a-id").Expect(HtmlRules.END_A)
          .Expect(HtmlRules.START_A_HREF, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetAHrefAttribValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetAHrefTagValue_returnsTagValue()
  {
    const string EXPECTED_VALUE = "CloseAccount";
    bool isMatched = false;

    var parser = Parsing.Group(_Pipe)
      .SkipUntil(HtmlRules.START_TABLE)
      .Expect(HtmlRules.START_TABLE, annotation: "table")
      .SkipUntil(HtmlRules.END_THEAD)
      .Expect(HtmlRules.END_THEAD, annotation: "end:thead")
      .Expect(HtmlRules.START_TR, annotation: "start:tr")
        .Expect(HtmlRules.START_TD_VALUE, annotation: "start:td-firstCell")
          .Expect(HtmlRules.START_A_ID, annotation: "start:a-id").Expect(HtmlRules.END_A)
          .Expect(HtmlRules.START_A_HREF, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetAHrefTagValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

  [Fact]
  public void GetPTagValue_returnsTagValue()
  {
    const string EXPECTED_VALUE = "iam:CreateServiceLinkedRole";
    bool isMatched = false;

    _HtmlPipe = new HtmlPipe(PipeValues.REPEAT, Console.Out);
    _Pipe = new ReplayWrapPipe(_HtmlPipe);

    var parser = Parsing.Group(_Pipe)
      .Expect(HtmlRules.START_TD_VALUE, annotation: "start:td-firstCell")
        .SkipUntil(HtmlRules.END_TD)
      .Expect(HtmlRules.END_TD, annotation:"end:td-firstCell")
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation:"start:td-2ndcell")
      .Expect(HtmlRules.END_TD, annotation: "end:td-2ndcell")
      .Expect(HtmlRules.START_TD_ATTRIB_VALUE, annotation: "start:td-3rdcell")
        .Expect(HtmlRules.START_PARA_VALUE, annotation: "subject")
      .AllMatchThen( (list,idx) => {
        var query = from node in list
          where node.HasAnnotation && node.Annotation == "subject"
          select node;
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetPTagValue(matching.Parts));
        isMatched = true;
      })
      ;
    Assert.True(isMatched);
  }

}