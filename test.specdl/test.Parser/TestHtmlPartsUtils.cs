/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Parser;
using HtmlParse;

using Optional;
using Xunit;

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace test.Parser;

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
        Matching matching = query.Last();
        Assert.Equal(EXPECTED_VALUE, HtmlPartsUtils.GetTdAttribValue(matching.Parts));
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
}