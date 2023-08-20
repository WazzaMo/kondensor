/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using Optional;

using Parser;
using HtmlParse;
using YamlWriters;

namespace Resources;

public struct ResourceTable
{
  private class InternalData
  {
    private bool _IsReadyToWrite = false;

    internal List<string> _Headings = new List<string>();
    internal List<ResourceDefinition> _Resources = new List<ResourceDefinition>();

    internal bool IsReadyToWrite => _IsReadyToWrite;
    internal void SignalReadyToWrite() => _IsReadyToWrite = true;
  }
  
  private InternalData _Data;

  public ResourceTable()
  {
    _Data = new InternalData();
  }

  public void AddHeading(string heading)
    => _Data._Headings.Add(heading);
  public IEnumerable<string> GetHeadings()
    => _Data._Headings.AsEnumerable();
  
  public void AddResourceDefinition(ResourceDefinition definition)
    => _Data._Resources.Add(definition);
  
  public IEnumerable<ResourceDefinition> GetResourceDefinitions()
    => _Data._Resources.AsEnumerable();
  
  public ParseAction ResourcesTable(ParseAction parser)
  {
    Action<LinkedList<Matching>>
      collectHeadings = CollectHeadings,
      collectResources = CollectResources;

    InternalData _data = _Data;

    parser
      .Expect(ResourceTableParser.ResourceTable)
      .AllMatchThen( (list,writer) => {
        collectHeadings(list);
        collectResources(list);
        _data.SignalReadyToWrite();
      });

    return parser;
  }

  public void WriteTable(YamlFormatter formatter)
  {
    IYamlHierarchy yaml = formatter;

    if (_Data.IsReadyToWrite)
      ResourcesYamlWriter.WriteTable(_Data._Headings, _Data._Resources, formatter);
    else
      yaml.Comment(message: "Resource table parsing did not succeed.");
  }

  private void CollectHeadings(LinkedList<Matching> matches)
  {
    var query = from nodes in matches
      where nodes.Annotation == ResourceAnnotations.S_RESOURCE_TH
      select nodes;
    
    string heading;
    var _headingsList = _Data._Headings;

    query.ForEach( (th, idx) => {
      heading = HtmlPartsUtils.GetThTagValue(th.Parts);
      if (!heading.IsEmptyPartsValue())
        _headingsList.Add(heading);
    });
  }

  private void CollectResources(LinkedList<Matching> matches)
  {
    var matchEnum = ResourceCollection.FilterForCollectables(matches);

    ResourceDefinition resource;

    while( matchEnum.MoveNext() )
    {
      if (ResourceCollection.IsResId(matchEnum.Current))
      {
        var id = matchEnum.Current;
        matchEnum.MoveNext();

        resource = new ResourceDefinition();
        resource.Id = HtmlPartsUtils.GetAIdAttribValue(id.Parts);

        if (ResourceCollection.IsResHref(matchEnum.Current)) // optional
        {
          var href = matchEnum.Current;
          matchEnum.MoveNext();

          resource.ApiLink = HtmlPartsUtils.GetAHrefAttribValue(href.Parts);
          resource.Name = HtmlPartsUtils.GetAHrefTagValue(href.Parts);
        }

        if (ResourceCollection.IsNameText( matchEnum.Current ))
        {
          var name = matchEnum.Current;
          matchEnum.MoveNext();
          resource.Name = HtmlPartsUtils.GetAEndValue(name);
        }

        if (ResourceCollection.IsResCode(matchEnum.Current))
        {
          var code = matchEnum.Current;
          matchEnum.MoveNext();

          resource.Arn = HtmlPartsUtils.GetCodeTagValue(code.Parts);

          while (ResourceCollection.IsCondKeyHref(matchEnum.Current))
          {
            var ckNode = matchEnum.Current;
            matchEnum.MoveNext();

            ResourceConditionKey conditionKey = new ResourceConditionKey();

            conditionKey.Id = HtmlPartsUtils.GetAHrefAttribValue(ckNode.Parts);
            conditionKey.Template = HtmlPartsUtils.GetAHrefTagValue(ckNode.Parts);
            resource.AddConditionKey( conditionKey );
          }
        }

        _Data._Resources.Add(resource);
      }
      // Should be resource end row at this point to be skipped in next cycle.
    }
  }

}