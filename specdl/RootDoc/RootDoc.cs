/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using Parser;
using HtmlParse;

namespace RootDoc;

public struct RootDoc
{
  private List<SubDoc> _Docs;

  public RootDoc()
  {
    _Docs = new List<SubDoc>();
  }

  public ParseAction ParseRootDoc(ParseAction parser)
  {
    parser
      .Expect(RootDocParser.RootDocProduction)
      .AllMatchThen( SuccessfulParse );
      
    return parser;
  }

  private void SuccessfulParse(LinkedList<Matching> matches, IPipeWriter writer)
  {
    CollectDocEntries(matches);
  }

  private void CollectDocEntries(LinkedList<Matching> matches)
  {
    IEnumerable<Matching> filtered = FilterNodes(matches);
    Matching aHref;
    string link, title;
    SubDoc doc;

    var entries = filtered.GetEnumerator();

    while( entries.MoveNext() )
    {
      aHref = entries.Current;
      link = HtmlPartsUtils.GetAHrefAttribValue(aHref.Parts);
      title = HtmlPartsUtils.GetAHrefTagValue(aHref.Parts);
      doc = new SubDoc(link, title);
      _Docs.Add(doc);
    }
  }

  private IEnumerable<Matching> FilterNodes(LinkedList<Matching> matches)
    => from node in matches
    where node.Annotation == RootAnnotations.START_AHREF
    select node;
}