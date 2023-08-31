/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

using kondensor.Parser;

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
    => IsActionDeclRowStart(annotation)
    || IsActionPropertyRowStart(annotation)
    || IsActionPropertyRowEnd(annotation)
    || IsStartActionIdAnnotation(annotation)
    || annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION
    || IsFirstActionDescription(annotation)
    || IsSameActionNewDescriptionAnnotation(annotation)
    || annotation == ActionAnnotations.START_ACCESSLEVEL_ANNOTATION
    || annotation == ActionAnnotations.A_HREF_RESOURCE
    || IsCondKeyIdAndNameHref(annotation)
    || annotation == ActionAnnotations.START_PARA_DEPENDENT
    ;

  internal static bool IsActionDeclRowStart(string annotation)
    => annotation == ActionAnnotations.START_ACTION_ROW_ANNOTATION;

  internal static bool IsActionPropertyRowStart(string annotation)
    => annotation == ActionAnnotations.START_ACTION_PROP_ROW_ANNOTATION;

  internal static bool IsActionPropertyRowEnd(string annotation)
    => annotation == ActionAnnotations.END_ACTION_PROP_ROW_ANNOTATION;

  internal static bool IsStartActionIdAnnotation(string annotation)
    => annotation == ActionAnnotations.START_ID_ACTION_ANNOTATION;

  internal static bool IsStartActionNameAndRef(string annotation)
    => annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION;

  internal static bool IsFirstActionDescription(string annotation)
    => annotation == ActionAnnotations.START_CELL_ACTIONDESC_ANNOTATION;

  internal static bool IsSameActionNewDescriptionAnnotation(string annotation)
    => annotation == ActionAnnotations.START_NEWDECL_PARA;

  internal static bool IsResourceConditionKeyOrDependency(string annotation)
    => IsResourceIdAndName(annotation)
    || IsCondKeyIdAndNameHref(annotation)
    || IsDependentKey(annotation)
    ;

  internal static bool IsResourceIdAndName(string annotation)
    => annotation == ActionAnnotations.A_HREF_RESOURCE;

  internal static bool IsCondKeyIdAndNameHref(string annotation)
    => annotation == ActionAnnotations.A_HREF_CONDKEY;

  internal static bool IsDependentKey(string annotation)
    => annotation == ActionAnnotations.START_PARA_DEPENDENT;

}
