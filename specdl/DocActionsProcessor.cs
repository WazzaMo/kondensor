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

  internal static IContext ActionDataRows(Stack<StackTask> stack, IContext context)
  {
    StackTask rowOrEnd = new StackTask() {
        Element = new TableRowOrTableEndElement(),
        UponFinalMatch = EndActionTable,
        UponMatch = ActionRow // prepare for another row
        //
      };
      stack.Push(rowOrEnd);
      
      IContext actionContext;
      if (context is ActionsTableContext actions)
      {
        actionContext = actions;
      }
      else
      {
        actionContext = new ActionsTableContext();
      }
      return actionContext; //ActionRow(stack, actionContext);
  }

  internal static IContext EndActionTable(Stack<StackTask> stack, IContext context)
  {
    if (context is ActionsTableContext actions)
    {
      Console.WriteLine("END Actions Table!");
    }
    return context;
  }

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
  internal static IContext ActionRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      rowEnd = new StackTask() { // </tr>
        Element = new TableRowEndElement(),
        UponFinalMatch = EndDataRow,
        UponMatch = NewAction
      },
      tdStart = new StackTask() { // <td ...>
        Element = new TableDataElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionId = new StackTask() { // <a id ...>
        Element = new AnchorWithIdElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      actionNameDoc = new StackTask() { // <a href=...>name match </a> final
        Element = new AnchorWithDocHrefElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      tdEnd = new StackTask() { // </td>
        Element = new TableDataEndElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      actionDesc = new StackTask() { // <td>Describ 
        Element = new TableDataActionDescriptionElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      actionAccess = new StackTask() { // <td>Write
        Element = new TableDataActionAccessLevelElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      tdResourceRef = new StackTask() { // <td (optional rowspan)>
        Element = new TableDataElement(),
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough,
        UponMatch = DocGeneralProcessor.Fault
      },
      paraResRef = new StackTask() { // <p>
        Element = new ParaElement()
      },
      anchorResRef = new StackTask() { // <a href=...>name</a>
        Element = new AnchorEntryHrefElement(),
        UponMatch = DocGeneralProcessor.ContextPassThrough
      },
      paraEndResRef = new StackTask() { // </p>
        Element = new ParaEndElement()
      },
      tdEndResourceRef = new StackTask() { // <td (optional rowspan)>
        Element = new TableDataEndElement(),
        UponFinalMatch = ActionDataRows
      };

      stack.Push(rowEnd);
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

      IContext actionContext;
      if (context is ActionsTableContext actions)
        actionContext = actions;
      else
        actionContext = new ActionsTableContext();
      return actionContext;
  }

  internal static IContext EndDataRow(Stack<StackTask> stack, IContext context)
  {
    IContext result = context;
    if (context is ActionsTableContext actions)
    {
        actions.CollectResourceTypeAndReset();
        actions.CollectActionTypeAndReset();
        result = actions;
    }
    return result;
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