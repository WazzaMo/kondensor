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

  internal static IContext SetContextForDataRows(Stack<StackTask> stack, IContext context)
  {
    StackTask
      rowStart = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = DocActionsProcessor.SetContextForData,
        UponMatch = DocGeneralProcessor.Fault
      },
      rowOrEnd = new StackTask() {
        Element = new TableDataOrRowEndElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
        //
      };
      stack.Push(rowOrEnd);
      stack.Push(rowStart);
      IContext actionContext = new ActionsTableContext();
      return actionContext;
  }

  internal static IContext SetContextForData(Stack<StackTask> stack, IContext context)
  {
    StackTask
      headingTr = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = ActionColumn
      },
      headingEndTr = new StackTask() {
        Element = new TableDataOrRowEndElement(),
        UponMatch = DocGeneralProcessor.ContextPassThrough,
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough
      };
      stack.Push(headingEndTr);
      stack.Push(headingTr);

      IContext result = new ActionsTableContext();

      return context;
  }

  internal static IContext ActionColumn(Stack<StackTask> stack, IContext context)
  {
    IContext result = context;
    if (context is ActionsTableContext actions)
    {
      Action<ActionsTableContext,string> task = SetAction;
      actions.UpdateStringTask = Option.Some(task);
    }
    return result;
  }

  private static void SetAction(ActionsTableContext context, string value)
  {
    context.Set
  }

}