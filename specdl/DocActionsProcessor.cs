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
    StackTask rowOrEnd = new StackTask() {
        Element = new TableRowOrTableEndElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = ActionRow // prepare for another row
        //
      };
      stack.Push(rowOrEnd);
      
      IContext actionContext = new ActionsTableContext();
      return ActionRow(stack, actionContext);
  }

  internal static IContext ActionRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      rowStart = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = NewAction,
        UponMatch = DocGeneralProcessor.Fault
      },
      tdStart = new StackTask() {
        Element = new TableDataElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionId = new StackTask() {
        Element = new AnchorWithIdElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      actionNameDoc = new StackTask() {
        Element = new AnchorWithDocHrefElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      tdEnd = new StackTask() {
        Element = new TableDataEndElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionDesc = new StackTask() {
        Element = new TableDataActionDescriptionAndAccessLevelElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionAccess = new StackTask() {
        Element = new TableDataActionDescriptionAndAccessLevelElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      tdResourceRef = new StackTask() {
        Element = new TableDataElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      paraResRef = new StackTask() {
        Element = new ParaElement()
      },
      anchorResRef = new StackTask() {
        Element = new AnchorEntryHrefElement(),
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      paraEndResRef = new StackTask() {
        Element = new ParaEndElement()
      },
      tdEndResourceRef = new StackTask() {
        Element = new TableDataEndElement()
      };
      stack.Push(tdEndResourceRef);
      stack.Push(paraEndResRef);
      stack.Push(anchorResRef);
      stack.Push(paraResRef);
      stack.Push(tdResourceRef);
      stack.Push(actionAccess);
      stack.Push(actionDesc);
      stack.Push(tdEnd);
      stack.Push(actionNameDoc);
      stack.Push(actionId);
      stack.Push(tdStart);
      stack.Push(rowStart);

      IContext actionContext;
      if (context is ActionsTableContext actions)
      {
        actions.CollectResourceTypeAndReset();
        actions.CollectActionTypeAndReset();
        actionContext = actions;
      }
      else
        actionContext = new ActionsTableContext();
      return actionContext;
  }

  internal static IContext NewAction(Stack<StackTask> stack, IContext context)
  {
    Console.WriteLine(value: $"---NOTE {nameof(NewAction)}: starting new row and context is {context.GetType().Name}");
    if (context is ActionsTableContext actionContext)
    {
      actionContext.NextActionDefinition();
    }
    return context;
  }

}