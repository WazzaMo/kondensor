/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;
using kondensor.Parser.AwsHtmlParse.Frag;

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
    string accessLevel = "";

    level = ActionAccessLevel.Unknown;
    resourceType = new ActionResourceType();

    bool isOk = ActionCollection.IsAccessLevelOnAction(nodes.Current.Annotation)
      && UtilsFragHtml.TryGetTagValue(nodes.Current, out accessLevel);

    if (isOk)
    {
      nodes.MoveNext();

      level = accessLevel.GetLevelFrom();
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
    string
      resourceHref = "",
      resourceName = "";

    do
    {
      if (ActionCollection.IsResourceHref(nodes.Current.Annotation)
        && UtilsFragHtml.TryGetHrefValue(nodes.Current, out resourceHref))
      {
        nodes.MoveNext();

        if( ActionCollection.IsResourceName(nodes.Current.Annotation)
         && UtilsFragHtml.TryGetTagValue(nodes.Current, out resourceName))
        {
          isResourceTypeNew = true;
          resourceType.SetTypeIdAndName(resourceHref, resourceName);
        }
      }
      if ( ActionCollection.IsCondKeyHref(nodes.Current.Annotation)
           && UtilsFragHtml.TryGetHrefValue( nodes.Current, out string condKeyHref)
        )
      {
        nodes.MoveNext();
        if (ActionCollection.IsCondKeyName(nodes.Current.Annotation)
        && UtilsFragHtml.TryGetTagValue(nodes.Current, out string condKeyName) )
        {
          resourceType.AddConditionKeyId(condKeyHref);
        }
      }

      if (ActionCollection.IsDependentActionValue(nodes.Current.Annotation))

      if (ActionCollection.IsDependentActionValue(nodes.Current.Annotation)
        && UtilsFragHtml.TryGetTagValue(nodes.Current, out string depAction) )
      {
          resourceType.AddDependentActionId(depAction);
      }
    }
    while (nodes.MoveNext()
      && !ActionCollection.IsActionPropertyRowEnd(nodes.Current.Annotation)
    );

    return isResourceTypeNew;
  }

}