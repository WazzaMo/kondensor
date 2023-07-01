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


namespace Resources;

public struct ResourceTable
{
  private class InternalData
  {
    internal List<string> _Headings = new List<string>();
    internal List<ResourceDefinition> _Resources = new List<ResourceDefinition>();
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

    parser
      .Expect(ResourceTableParser.ResourceTable)
      .AllMatchThen( (list,writer) => {
        collectHeadings(list);
        collectResources(list);
      });
    Console.WriteLine(value: $"Parsed resource table - {_Data._Headings.Count}");

    return parser;
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
    var matchEnum = matches.GetEnumerator();

    bool isOK = SeekTo(ref matchEnum, ResourceAnnotations.E_RESOURCE_THEAD);
    isOK = GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.E_RESOURCE_THEAD, out var _);

    ResourceDefinition resource;

    while(isOK)
    {
      resource = new ResourceDefinition();

      isOK = GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.S_RESOURCE_TR, out var _)
        && GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.S_DATA_ROW_TYPE, out var _);
      if (isOK)
      {
        if (GetId(ref matchEnum, out var IdNode))
          resource.Id = HtmlPartsUtils.GetAIdAttribValue(IdNode.Parts);
        if( GetHRef(ref matchEnum, out var hrefNode) )
        {
          resource.ApiLink = HtmlPartsUtils.GetAHrefAttribValue(hrefNode.Parts);
          resource.Name = HtmlPartsUtils.GetAHrefTagValue(hrefNode.Parts);
        }
        isOK = GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.E_DATA_ROW_TYPE, out var _);
      }
      isOK = isOK && GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.S_DATA_ROW_ARN, out var _);
      if (isOK)
      {
        if (GetArnCode(ref matchEnum, out var arnNode))
          resource.Arn = HtmlPartsUtils.GetCodeTagValue( arnNode.Parts );
        isOK = GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.E_DATA_ROW_ARN, out var _);
      }

      isOK = isOK && GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.S_DATA_ROW_CK, out var _);
      if (isOK)
      {
        if ( GetConditionKey(ref matchEnum, out var condKeyNode) )
        {
          ResourceConditionKey condKey = new ResourceConditionKey();
          condKey.Id = HtmlPartsUtils.GetAHrefAttribValue(condKeyNode.Parts);
          condKey.Template = HtmlPartsUtils.GetAHrefTagValue( condKeyNode.Parts );
          resource.ConditionKey = Option.Some( condKey );
        }
        isOK = GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.E_DATA_ROW_CK, out var _);
      }
      if (isOK && resource.IsValid() )
        _Data._Resources.Add(resource);

      isOK = isOK && GetCurrentAndAdvance(ref matchEnum, ResourceAnnotations.E_RESOURCE_TR, out var _);
    }
  }

  private static bool GetId(ref LinkedList<Matching>.Enumerator _enum, out Matching idNode)
  {
    bool isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.S_A_ID, out idNode);
    if (isOk) isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.E_A_ID, out var _);
    return isOk;
  }

  private static bool GetHRef(ref LinkedList<Matching>.Enumerator _enum, out Matching hrefNode)
  {
    bool isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.S_A_HREF_NAME, out hrefNode);
    if (isOk) isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.E_A_HREF_NAME, out var _);
    return isOk;
  }

  private static bool GetArnCode(ref LinkedList<Matching>.Enumerator _enum, out Matching arn)
  {
    bool isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.S_CODE, out arn);
    if (isOk) isOk = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.E_CODE, out var _);
    return isOk;
  }

  private static bool GetConditionKey(ref LinkedList<Matching>.Enumerator _enum, out Matching condKeyNode)
  {
    bool isOK = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.S_P_CONDKEY, out var _);
    if (isOK)
    {
      isOK = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.S_A_CONDKEY_HREF, out condKeyNode);
      if (isOK) isOK = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.E_A_CONDKEY_HREF, out var _);
      if (isOK) isOK = GetCurrentAndAdvance(ref _enum, ResourceAnnotations.E_P_CONDKEY, out var _);
    }
    else
      condKeyNode = new Matching();
    return isOK;
  }

  private static bool GetCurrentAndAdvance(ref LinkedList<Matching>.Enumerator _enum, string expectAnnotation, out Matching node)
  {
    bool isMatch = GetCurrent(ref _enum, expectAnnotation, out node);
    if (isMatch)
    {
      _enum.MoveNext();
    }
    
    return isMatch;
  }

  private static bool GetCurrent(ref LinkedList<Matching>.Enumerator _enum, string expectAnnotation, out Matching node)
  {
    bool isOk = _enum.Current.Annotation == expectAnnotation;
    node = isOk
      ?_enum.Current
      : new Matching();
    return isOk;
  }

  private static bool SeekTo(ref LinkedList<Matching>.Enumerator _enum, string seekAnno)
  {
    bool isOK = true;
    while(_enum.Current.Annotation != seekAnno && isOK)
    {
      isOK = _enum.MoveNext();
    }
    return isOK;
  }
}