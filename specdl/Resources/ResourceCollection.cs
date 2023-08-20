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

internal static class ResourceCollection
{
  internal static IEnumerator<Matching> FilterForCollectables(LinkedList<Matching> list)
  {
    var filter = from node in list
      where IsResId(node) || IsResHref(node)
        || IsResCode(node) || IsCondKeyHref(node)
        || IsNameText(node) || IsEndResource(node)
      select node;
    return filter.GetEnumerator();
  }

  internal static bool IsResId(Matching m)
    => m.Annotation == ResourceAnnotations.S_A_ID;
  
  internal static bool IsResHref(Matching m)
    => m.Annotation == ResourceAnnotations.S_A_HREF_NAME;
  
  internal static bool IsResCode(Matching m)
    => m.Annotation == ResourceAnnotations.S_CODE;
  
  internal static bool IsCondKeyHref(Matching m)
    => m.Annotation == ResourceAnnotations.S_A_CONDKEY_HREF;
  
  internal static bool IsNameText(Matching m)
    => m.Annotation == ResourceAnnotations.E_A_ID_TEXT;

  internal static bool IsEndResource(Matching m)
    => m.Annotation == ResourceAnnotations.E_RESOURCE_TR;
}