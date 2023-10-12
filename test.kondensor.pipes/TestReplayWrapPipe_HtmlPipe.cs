/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Xunit;

using kondensor.Pipes;

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace test.kondensor.pipes;

public class TestReplayWrapPipe_HtmlPipe
{
  private PipeFixture_HtmlPipe _Fixture;

  public TestReplayWrapPipe_HtmlPipe()
  {
    _Fixture = new PipeFixture_HtmlPipe();
  }

  const string
      DEFAULT = "++++++",
      TOK1 = @"<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-US"">",
      TOK2 = "<head>",
      TOK3 = @"<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />",
      TOK4 = @"<title>Actions, resources, and condition keys for AWS Account Management - Service Authorization Reference",
      TOK5 = @"</title>";

  [Fact]
  public void TokensFetchedInOrderFromRootPipe()
  {
    string token = DEFAULT;

    Assert.True(_Fixture.RootPipe.ReadToken(out token));
    Assert.Equal(TOK1, token);

    Assert.True(_Fixture.RootPipe.ReadToken(out token));
    Assert.Equal(TOK2, token);

    Assert.True(_Fixture.RootPipe.ReadToken(out token));
    Assert.Equal(TOK3, token);
  }

  [Fact]
  public void TokensFetchedInOrderFromReplayPipe()
  {
    string token = DEFAULT;

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK1, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK2, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK3, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK4, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK5, token);
  }

  [Fact]
  public void TryScanAheadFor_makes_scanned_token_next()
  {
    string token = DEFAULT;
    char[] search = TOK2.ToCharArray();

    Assert.True( _Fixture.Subject.TryScanAheadFor(search, out int matchIdx));
    Assert.True( _Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK2, token);
  }

  [Fact]
  public void ScanAhead_makes_scanned_token_next()
  {
    string token = DEFAULT;
    Regex regex = new Regex(TOK2);

    ScanRule tok2Scan = (string token) => {
      ScanResult result = new ScanResult();
      var match = regex.Match(token);
      if (match.Success) {
        result.IsMatched = true;
        result.Index = match.Index;
      }
      return result;
    };

    var scan = _Fixture.Subject.ScanAhead(tok2Scan);
    Assert.True( scan.IsMatched );
    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK2, token);
  }

  [Fact]
  public void ReplayPipe_CanReplayFromCheckpoint()
  {
    string token = DEFAULT;

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK1, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK2, token);

    int checkPoint = _Fixture.Subject.GetCheckPoint();

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK3, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK4, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK5, token);

    _Fixture.Subject.ReturnToCheckPoint(checkPoint);
    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK3, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK4, token);
  }

}