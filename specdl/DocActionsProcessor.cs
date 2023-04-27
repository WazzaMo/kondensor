/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using Optional;

public static class DocActionsProcessor
{
    // Handle data section
  //
  //<tr>
  //    <td>
  //        <a id="awsaccountmanagement-CloseAccount"></a>
  //        <a href="https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html">CloseAccount</a> [permission only]</td>
  //    <td>Grants permission to close an account</td>
  //    <td>Write</td>
  //    <td>
  //        <p>
  //            <a href="#awsaccountmanagement-account">account</a>
  //        </p>
  //    </td>
  //    <td></td>
  //    <td></td>
  //</tr>

  internal static IContext ActionDataRows(Stack<StackTask> stack, IContext context)
  {
    StackTask
      rowStart = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionId = new StackTask() {
        //
      },
      actionDesc = new StackTask() {
        Element = new TableDataActionDescriptionElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionAccess = new StackTask() {
        Element = new TableDataActionDescriptionElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      rowOrEnd = new StackTask() {
        Element = new TableDataOrRowEndElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
        //
      };
      stack.Push(rowOrEnd);
      stack.Push(actionAccess);
      stack.Push(actionDesc);
      stack.Push(actionId);
      stack.Push(rowStart);
      IContext actionContext = new ActionsTableContext();
      return actionContext;
  }

  internal static IContext ActionColumn(Stack<StackTask> stack, IContext context)
  {
    IContext result = context;
    if (context is ActionsTableContext actions)
    {
      StackTask
        actionId = new StackTask() {
          Element = new AnchorWithIdElement(),
          UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
          UponMatch = DocGeneralProcessor.Fault
        },
        actionDocAndName = new StackTask() {
          Element = new AnchorWithDocHrefElement(),
          UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
          UponMatch = DocGeneralProcessor.Fault
        };
      stack.Push(actionDocAndName);
      stack.Push(actionId);
    }
    return result;
  }

}