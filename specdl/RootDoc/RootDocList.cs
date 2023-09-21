/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using kondensor.Pipes;
using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

namespace RootDoc;

/// <summary>
/// Parses and builds a list of the policy
/// documents for different AWS services.
/// </summary>
public struct RootDocList
{
  private List<SubDoc> _Docs;

  public RootDocList()
  {
    _Docs = new List<SubDoc>();
  }

  /// <summary>
  /// Provides access to the sub-documents for each service type.
  /// </summary>
  /// <returns>Enumerator instance for <see cref="SubDoc"/> </returns>
  public IEnumerator<SubDoc> EnumerateDocs() => _Docs.GetEnumerator();

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