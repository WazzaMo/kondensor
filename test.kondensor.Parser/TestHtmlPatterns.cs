/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using Xunit;

using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.HtmlParse;

using System;
using System.Text.RegularExpressions;

namespace test.kondensor.Parser;

public class TestHtmlPatterns
{
  [Fact]
  public void TABLE_match()
  {
    Match match = HtmlPatterns.TABLE.Match("<table>");
    Assert.True(match.Length > 0);
    Assert.Single(match.Groups);
  }

  [Fact]
  public void TABLE_ATTRIB_match_attribute()
  {
    const string idValue = "w43aab5b9c19c11c11";
    const string html = "<table id=\"w43aab5b9c19c11c11\">", attrib = "id";
    Match match = HtmlPatterns.TABLE_ATTRIB.Match(html);

    Assert.True(match.Length > 0);
    Assert.Equal(expected: 3, match.Groups.Count);
    Assert.Equal(html, match.Groups[0].Value);
    Assert.Equal(attrib, match.Groups[1].Value);
    Assert.Equal(idValue, match.Groups[2].Value);
  }

  [Fact]
  public void END_TABLE_match()
  {
    Match match = HtmlPatterns.END_TABLE.Match("</table>");
    Assert.True(match.Length > 0);
    Assert.Single(match.Groups);
  }

  [Fact]
  public void THEAD_match()
  {
    Match match = HtmlPatterns.THEAD.Match(input: "<thead>");
    CheckMatches(count: 1, match);
  }

  [Fact]
  public void END_THEAD_match()
  {
    Match match = HtmlPatterns.END_THEAD.Match(input: "</thead>");
    CheckMatches(count: 1, match);
  }

  [Fact]
  public void TR_match()
  {
    Match match;
    match = HtmlPatterns.TR.Match("<tr>");
    CheckMatches(1, match);
  }

  [Fact]
  public void TH_matchWithDescription()
  {
    const string
      html = "<th>Resource types (*required)",
      desc = "Resource types (*required)";
    
    Match match = HtmlPatterns.TH_VALUE.Match(html);
    Assert.Equal(html.Length, match.Length);
    Assert.Equal(html, match.Groups[0].Value);
    Assert.Equal(desc, match.Groups[1].Value);
  }

  [Fact]
  public void END_TH_match()
  {
    const string html = "</th>";
    Match match = HtmlPatterns.END_TH.Match(html);
    Assert.Equal(html.Length, match.Length);
    Assert.Equal(html, match.Value);
  }

  [Fact]
  public void TD_match()
  {
    Match match;
    match = HtmlPatterns.TD.Match(input:"<td>");
    CheckMatches(1, match);
  }

  [Fact]
  public void TD_ATTRIB_VALUE_match()
  {
    const string
      html = "<td rowspan=\"4\">nameValue",
      attrib = "rowspan",
      rowCount = "4",
      description = "nameValue";
    Match match;

    match = HtmlPatterns.TD_ATTRIB_VALUE.Match(html);
    CheckMatches(count: 4, match);
    Assert.Equal(html, match.Groups[0].Value);
    Assert.Equal(attrib, match.Groups[1].Value);
    Assert.Equal(rowCount, match.Groups[2].Value);
    Assert.Equal(description, match.Groups[3].Value);
  }

  [Fact]
  public void TD_mismatched_on_bad_text()
  {
    Match match;
    match = HtmlPatterns.TD.Match(input: "<td rowspan=\"3\">");
    Assert.False(match.Length > 0);
    Assert.Single(match.Groups);
    Assert.Equal(0, match.Groups[0].Length);

    match = HtmlPatterns.TD_ATTRIB_VALUE.Match(input: "<td>");
    Assert.Equal(0, match.Groups[0].Length);
    Assert.Equal(0, match.Groups[1].Length);
    Assert.Equal(0, match.Groups[2].Length);
  }

  [Fact]
  public void END_TD_match()
  {
    const string html = "</td>";
    Match match = HtmlPatterns.END_TD.Match(html);
    Assert.Equal(html.Length, match.Length);
    Assert.Equal(html, match.Value);
  }

  [Fact]
  public void PARA_match()
  {
    Match match;
    const string html = "<p>";

    match = HtmlPatterns.PARA.Match(html);
    Assert.True(match.Length > 0);
    Assert.Equal(html, match.Value);
  }

  [Fact]
  public void END_PARA_match()
  {
    const string html = "</p>";
    Match match;

    match = HtmlPatterns.END_PARA.Match(html);
    Assert.True(match.Length > 0);
    Assert.Equal(html.Length, match.Length);
    Assert.Equal(html, match.Value);
  }

  [Fact]
  public void A_ID_match()
  {
    const string html_IdNoValue = "<a id=\"awsaccountmanagement-CloseAccount\">",
      name = "awsaccountmanagement-CloseAccount";
    Match match;

    match = HtmlPatterns.A_ID.Match(html_IdNoValue);
    Assert.Equal(html_IdNoValue.Length, match.Length);
    Assert.Equal(2, match.Groups.Count);
    Assert.Equal(html_IdNoValue, match.Groups[0].Value);
    Assert.Equal(name, match.Groups[1].Value);
  }

  [Fact]
  public void A_HREF_matches()
  {
    const string
      htmlDesc = "<a href=\"https://docsResourceTypeDefinitionId.aws.amazon.com/accounts/latest/reference/API_DeleteAlternateContact.html\">DeleteAlternateContact",
      desc = "DeleteAlternateContact",
      href = "https://docsResourceTypeDefinitionId.aws.amazon.com/accounts/latest/reference/API_DeleteAlternateContact.html",
      
      htmlDesc2 = "<a href=\"https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys\">account:AccountResourceOrgTags/${TagKey}",
      href2 = "https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys",
      desc2 = "account:AccountResourceOrgTags/${TagKey}";

    Match match;

    match = HtmlPatterns.A_HREF.Match(htmlDesc);
    Assert.Equal(htmlDesc.Length, match.Length);
    Assert.Equal(htmlDesc, match.Groups[0].Value);
    Assert.Equal(href, match.Groups[1].Value);
    Assert.Equal(desc, match.Groups[2].Value);

    match = HtmlPatterns.A_HREF.Match(htmlDesc2);
    Assert.Equal(htmlDesc2.Length, match.Length);
    Assert.Equal(href2, match.Groups[1].Value);
    Assert.Equal(desc2, match.Groups[2].Value);
  }

  [Fact]
  public void END_A_match()
  {
    const string html = "</a>";
    Match match;

    match = HtmlPatterns.END_A.Match(html);
    Assert.Equal(html.Length, match.Length);
    Assert.Equal(html, match.Value);
  }

  [Fact]
  public void PARA_VALUE_matches()
  {
    const string
      html = @"<p>
                                iam:CreateServiceLinkedRole",
      depAction = @"
                                iam:CreateServiceLinkedRole";
    Match match;

    match = HtmlPatterns.PARA_VALUE.Match(html);
    Assert.Equal(html.Length, match.Length);
    Assert.Collection(match.Groups.Values,
      g0 => Assert.Equal(html, g0.Value),
      g1 => Assert.Equal(depAction, g1.Value)
    );
  }

  private void CheckMatches(int count, Match match)
  {
    Assert.True(match.Length > 0);
    Assert.Equal(count, match.Groups.Count);
  }
}