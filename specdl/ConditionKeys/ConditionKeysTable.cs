/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;
using kondensor.Pipes;

using kondensor.YamlFormat;

using YamlWriters;

namespace ConditionKeys;

public struct ConditionKeysTable
{
  internal class InternalData
  {
    internal bool IsParsedSuccessfully;
    internal List<string> Headings;
    internal List<ConditionKeyEntry> Entries;

    internal InternalData()
    {
      Headings = new List<string>();
      Entries = new List<ConditionKeyEntry>();
      IsParsedSuccessfully = false;
    }
  }

  private InternalData _Data;

  public ConditionKeysTable()
  {
    _Data = new InternalData();
  }

  public ParseAction ParseConditionKeysTable(ParseAction parser)
  {
    parser
      .Expect(ConditionKeysTableParser.Parser)
      .AllMatchThen( CollectValuesFromMatchings )
      .MismatchesThen( OnFailedParsing );
    return parser;
  }

  public bool IsReadyToWrite => _Data.IsParsedSuccessfully;

  public void WriteTable(YamlFormatter formatter)
  {
    const string
      ERROR_NO_ENTRIES = "Condition keys were not parsed.";
    IYamlHierarchy yaml = formatter;

    if (_Data.IsParsedSuccessfully )
    {
      ConditionKeysYamlWriter.WriteYaml(_Data.Headings, _Data.Entries, formatter);
    }
    else
      yaml.Comment(ERROR_NO_ENTRIES);
  }

  private void CollectValuesFromMatchings(LinkedList<Matching> list, IPipeWriter writer)
  {
    CollectHeadings(list);
    CollectEntries(list);
    _Data.IsParsedSuccessfully = true;
  }

  private void OnFailedParsing(
    LinkedList<Matching> list,
    IPipeWriter writer,
    ParseAction _
  )
    => _Data.IsParsedSuccessfully = false;

  private void CollectHeadings(LinkedList<Matching> list)
  {
    var query = from node in list
      where node.Annotation == ConditionAnnotations.S_TH_VALUE
      select node;
    query.ForEach( GetHeading );
  }

  private void CollectEntries(LinkedList<Matching> list)
  {
    IEnumerable<Matching> matchings = FilterNodes(list);
    Matching aId, aHref, tdDesc, tdType;

    var step = matchings.GetEnumerator();
    while(step.MoveNext())
    {
      aId = step.Current;
      if (step.MoveNext())
      {
        aHref = step.Current;
        if (step.MoveNext())
        {
          tdDesc = step.Current;
          if (step.MoveNext())
          {
            tdType = step.Current;
            AddEntry(aId, aHref, tdDesc, tdType);
          }
        }
      }
    }
  }

  private void AddEntry(
    Matching aId,
    Matching aHref,
    Matching tdDesc,
    Matching tdType
  )
  {
    ConditionKeyEntry entry = new ConditionKeyEntry();
    var id = HtmlPartsUtils.GetAIdAttribValue(aId.Parts);
    entry.SetId(id);
    var url = HtmlPartsUtils.GetAHrefAttribValue(aHref.Parts);
    var name = HtmlPartsUtils.GetAHrefTagValue(aHref.Parts);
    entry.SetDocLink(url);
    entry.SetName(name);
    var description = HtmlPartsUtils.GetTdTagValue(tdDesc.Parts);
    entry.SetDescription(description);
    var typeInfo = HtmlPartsUtils.GetTdTagValue(tdType.Parts);
    ConditionKeyType ckType = GetType(typeInfo);
    entry.SetCkType(ckType);
    _Data.Entries.Add(entry);
  }

  private IEnumerable<Matching> FilterNodes(LinkedList<Matching> list)
    => from node in list
      where (node.Annotation == ConditionAnnotations.S_AID
        || node.Annotation == ConditionAnnotations.S_AHREF
        || node.Annotation == ConditionAnnotations.S_TD_DESC
        || node.Annotation == ConditionAnnotations.S_TD_TYPE
      ) select node;

  private ConditionKeyType GetType(string value)
  {
    ConditionKeyType _type;
    if (! Enum.TryParse<ConditionKeyType>(value, ignoreCase: true, out _type))
    {
      _type = ConditionKeyType._Unknown;
    }
    return _type;
  }

  private void GetHeading(Matching match, int id)
  {
    string heading = HtmlPartsUtils.GetThTagValue(match.Parts);
    _Data.Headings.Add( heading );
  }
}