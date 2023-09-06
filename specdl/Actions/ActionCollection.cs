/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */

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

  internal static bool IsActionDeclarationStart(
    ref IEnumerator<Matching> node,
    out string id
  )
  {
    bool isDeclaration = false;

    if (IsStartActionIdAnnotation(node.Current.Annotation))
    {
      Matching aId = node.Current;
      id = HtmlPartsUtils.GetAIdAttribValue(aId.Parts);
      node.MoveNext();
      isDeclaration = true;
    } else if (IsStartActionDeclarationWithId(node.Current.Annotation))
    {
      Matching tdWithId = node.Current;
      isDeclaration = HtmlPartsUtils.TryGetTdId(node.Current, out id);
      if (isDeclaration)
        node.MoveNext();
    }
    else
      id = "";
    return isDeclaration;
  }

  internal static bool IsActionDeclarationAnnotation(string annotation)
    => IsActionDeclRowStart(annotation)
    || IsStartActionDeclarationWithId(annotation)
    || IsActionPropertyRowStart(annotation)
    || IsActionPropertyRowEnd(annotation)
    || IsStartActionIdAnnotation(annotation)
    || annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION
    || IsFirstActionDescription(annotation)
    || IsSameActionNewDescriptionAnnotation(annotation)
    || annotation == ActionAnnotations.START_TD_ACCESSLEVEL
    || annotation == ActionAnnotations.A_HREF_RESOURCE
    || IsCondKeyIdAndNameHref(annotation)
    || annotation == ActionAnnotations.START_PARA_DEPENDENT
    ;

  internal static bool IsStartActionDeclarationWithId(string annotation)
    => annotation == ActionAnnotations.START_TD_ID_ACTION;

  internal static bool IsActionDeclRowStart(string annotation)
    => annotation == ActionAnnotations.START_ACTION_ROW_ANNOTATION;

  internal static bool IsActionPropertyRowStart(string annotation)
    => annotation == ActionAnnotations.START_TR_ACTION_PROP_ROW;

  internal static bool IsActionPropertyRowEnd(string annotation)
    => annotation == ActionAnnotations.END_TR_ACTION_PROP_ROW;

  internal static bool IsStartActionIdAnnotation(string annotation)
    => annotation == ActionAnnotations.START_ID_ACTION_ANNOTATION;

  internal static bool IsStartActionNameAndRef(string annotation)
    => annotation == ActionAnnotations.START_HREF_ACTION_ANNOTATION;

  internal static bool IsFirstActionDescription(string annotation)
    => annotation == ActionAnnotations.START_TD_ACTIONDESC;

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
