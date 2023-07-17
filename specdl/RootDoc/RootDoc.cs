/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Parser;

namespace RootDoc;

public struct RootDoc
{
  private List<SubDoc> _Docs;
  bool _IsAllMatched;

  public RootDoc()
  {
    _Docs = new List<SubDoc>();
    _IsAllMatched = false;
  }

  public ParseAction ParseRootDoc(ParseAction parser)
  {
    parser
      .Expect(RootDocParser.RootDocProduction)
      .AllMatchThen( (list, writer) => {
        _IsAllMatched = true;
      });
      
    return parser;
  }
}