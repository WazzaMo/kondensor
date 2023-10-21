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

public class TestReplayWrapPipe_FragHtmlPipe
{
  private PipeFixture_FragHtmlPipe _Fixture;

  public TestReplayWrapPipe_FragHtmlPipe()
  {
    _Fixture = new PipeFixture_FragHtmlPipe();
  }

  const string
      DEFAULT = "++++++",
      TOK1 = @"<html",
      TOK2 = @"xmlns=""http://www.w3.org/1999/xhtml""",
      TOK3 = @"lang=""en-US""",
      TOK4 = @">",
      TOK5 = "<head"
      ;

  [Fact]
  public void TokensFetchedInOrderFromRootPipe()
  {
    string token = DEFAULT;
    var basePipe = _Fixture.RootPipe;

    Assert.True(basePipe.ReadToken(out token));
    Assert.Equal(TOK1, token);

    Assert.True(basePipe.ReadToken(out token));
    Assert.Equal(TOK2, token);

    Assert.True(basePipe.ReadToken(out token));
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

  // [Fact]
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

    IPipeCheckPoint checkPoint = _Fixture.Subject.GetCheckPoint();

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK3, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK4, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK5, token);

    _Fixture.Subject.RestoreToCheckPoint(checkPoint);
    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK3, token);

    Assert.True(_Fixture.Subject.ReadToken(out token));
    Assert.Equal(TOK4, token);
  }

}