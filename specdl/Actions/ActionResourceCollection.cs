/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

namespace Actions;

internal static class ActionResourceCollection
{
  /// <summary>Return a copy of the action resource given</summary>
  /// <param name="resourceType">value to copy</param>
  /// <returns>Copied, independent action resource</returns>
  internal static ActionResourceType CopyResourceType(ActionResourceType resourceType)
  {
    ActionResourceType copy = new ActionResourceType();
    copy.SetTypeIdAndName(
      resourceType.ResourceTypeDefId,
      resourceType.ResourceTypeName
    );
    copy.SetDescription(resourceType.Description);
    return copy;
  }

  /// <summary>Attempts to collect action properties returning success indicator.</summary>
  /// <param name="nodes">Matchings to scan</param>
  /// <param name="description">Description collected earlier</param>
  /// <param name="level">Access level collected and returned</param>
  /// <param name="resourceType">Action resource collected and returned</param>
  /// <returns>True if success, fail if not</returns>
  internal static bool HasCollectedActionProperties(
      IEnumerator<Matching> nodes,
      string description,
      out ActionAccessLevel level,
      out ActionResourceType resourceType
    )
  {
    bool result = false;

    level = ActionAccessLevel.Unknown;
    resourceType = new ActionResourceType();

    bool isOk = nodes.Current.Annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION;
    if (isOk)
    {
      Matching levelNode = nodes.Current;
      nodes.MoveNext();

      string levelTxt = HtmlPartsUtils.GetTdTagValue(levelNode.Parts);
      level = levelTxt.GetLevelFrom();
      if (level != ActionAccessLevel.Unknown)
      {
        resourceType = new ActionResourceType();
        resourceType.SetDescription(description);
        result = true;

        CollectActionPropertyRow(
          nodes,
          ref resourceType
        );
      }
    }
    return result;
  }

  internal static bool CollectActionPropertyRow(
    IEnumerator<Matching> nodes,
    ref ActionResourceType resourceType
  )
  {
    bool isResourceTypeNew = false;
    do
    {
      if (ActionCollection.IsResourceIdAndName(nodes.Current.Annotation))
      {
        Matching aHrefResource = nodes.Current;

        string resourceId, resourceName;

        isResourceTypeNew = true;

        resourceId = HtmlPartsUtils.GetAHrefAttribValue(aHrefResource.Parts);
        resourceName = HtmlPartsUtils.GetAHrefTagValue(aHrefResource.Parts);
        resourceType.SetTypeIdAndName(resourceId, resourceName);
      }
      if (ActionCollection.IsCondKeyIdAndNameHref(nodes.Current.Annotation))
      {
        Matching aHrefConditionKey = nodes.Current;
        string ckId, ckName;

        ckId = HtmlPartsUtils.GetAHrefAttribValue(aHrefConditionKey.Parts);
        ckName = HtmlPartsUtils.GetAHrefTagValue(aHrefConditionKey.Parts);
        resourceType.AddConditionKeyId(ckId);
      }

      if (ActionCollection.IsDependentKey(nodes.Current.Annotation))
      {
        Matching tdDependency = nodes.Current;
        string depId = HtmlPartsUtils.GetPTagValue(tdDependency.Parts);

        if (!depId.IsEmptyPartsValue())
          resourceType.AddDependentActionId(depId);
      }
    }
    while (nodes.MoveNext()
      && !ActionCollection.IsActionPropertyRowEnd(nodes.Current.Annotation)
    );

    return isResourceTypeNew;
  }

}