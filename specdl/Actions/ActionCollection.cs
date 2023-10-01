/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using System.IO.Pipes;
using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;

namespace Actions;

internal static class ActionCollection
{
  internal static IEnumerable<Matching> FilterActionDeclaration(LinkedList<Matching> list)
  {
    Func<string, bool> filter = ActionCollection.IsActionDeclarationAnnotation;
    var query = from node in list where filter(node.Annotation) select node;
    return query;
  }

  internal static bool IsActionDeclarationAnnotation(string annotation)
    => IsStartNewActionRow(annotation)
    || IsEndNewActionRow(annotation)
    || IsStartDescriptionAndPropsActionRow(annotation)
    || IsEndDescriptionAndPropsActionRow(annotation)
    || IsStartExtraPropsActionRow(annotation)
    || IsEndExtraPropsActionRow(annotation)
    || IsActionId(annotation) || IsActionHref(annotation) || IsActionName(annotation)
    || IsDescriptionNewAction(annotation) || IsNewDescriptionSameAction(annotation)
    || IsAccessLevelOnAction(annotation)
    || IsResourceConditionKeyOrDependency(annotation)
    ;
  
  internal static bool IsActionPropertyRowStart(string annotation)
    => IsStartNewActionRow(annotation)
    || IsStartDescriptionAndPropsActionRow(annotation)
    || IsStartExtraPropsActionRow(annotation);

  internal static bool IsActionPropertyRowEnd(string annotation)
    => IsEndNewActionRow(annotation) || IsEndDescriptionAndPropsActionRow(annotation)
    || IsEndExtraPropsActionRow(annotation);

  internal static bool IsStartNewActionRow(string annotation)
    => annotation == ActionAnnotations.START_ROW_ACTION;

  internal static bool IsStartDescriptionAndPropsActionRow(string annotation)
    => annotation == ActionAnnotations.START_ROW_EXTENDED_ACTION;
  
  internal static bool IsStartExtraPropsActionRow(string annotation)
    => annotation == ActionAnnotations.START_ROW_ADD_PROPS_ACTION;
  
  internal static bool IsEndNewActionRow(string annotation)
    => annotation == ActionAnnotations.END_ROW_ACTION;
  
  internal static bool IsEndDescriptionAndPropsActionRow(string annotation)
    => annotation == ActionAnnotations.END_ROW_EXTENDED_ACTION;
  
  internal static bool IsEndExtraPropsActionRow(string annotation)
    => annotation == ActionAnnotations.END_ROW_ADD_PROPS_ACTION;

  internal static bool IsActionId(string annotation)
    => annotation == ActionAnnotations.ID_ACTION;
  
  internal static bool IsActionHref(string annotation)
    => annotation == ActionAnnotations.HREF_ACTION;
  
  internal static bool IsActionName(string annotation)
    => annotation == ActionAnnotations.NAME_ACTION;
  
  internal static bool IsDescriptionNewAction(string annotation)
    => annotation == ActionAnnotations.ACTION_DESCRIPTION_FIRST_ENTRY;
  
  internal static bool IsNewDescriptionSameAction(string annotation)
    => annotation == ActionAnnotations.NEW_ACTION_DESCRIPTION_SAME_ENTRY;
  
  internal static bool IsAccessLevelOnAction(string annotation)
    => annotation == ActionAnnotations.ACTION_ACCESS_LEVEL;
  
  internal static bool IsResourceConditionKeyOrDependency(string annotation)
    => IsResourceHref(annotation) || IsResourceName(annotation)
    || IsCondKeyHref(annotation) || IsCondKeyName(annotation)
    || IsDependentActionValue(annotation)
    ;

  internal static bool IsResourceHref(string annotation)
    => annotation == ActionAnnotations.RESOURCE_HREF;

  internal static bool IsResourceName(string annotation)
    => annotation == ActionAnnotations.RESOURCE_NAME;


  internal static bool IsCondKeyHref(string annotation)
    => annotation == ActionAnnotations.CONDKEY_HREF;
  
  internal static bool IsCondKeyName(string annotation)
    => annotation == ActionAnnotations.CONDKEY_NAME;

  internal static bool IsDependentActionValue(string annotation)
    => annotation == ActionAnnotations.DEPACT_VALUE;

}
