/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;
using System.Collections.Generic;


/// <summary>
/// A document processor reads line-by-line an HTML file given to it.
/// Source documents contain:
/// - Action, Resource type, condition keys and dependent actions table
/// - Resource types table
/// - Condition keys table
/// 
/// The combination of these make up the security specification for associating
/// AWS actions with resoures and conditions that can make up effective policies.
/// 
/// Source documents on the AWS website to process are found here:
/// <see
///   href="https://docs.aws.amazon.com/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html"
/// />
/// </summary>
public struct DocProcessor : IProcessor
{
  private Stack<StackTask> _ParseStack;
  private IContext _CurrentContext;

  public DocProcessor()
  {
    _ParseStack = new Stack<StackTask>();
    _CurrentContext = InitStackTaskForTable(_ParseStack, new NoneContext());
  }

  public void ProcessAllLines(out int countHandled, TextReader input, TextWriter output)
  {
    countHandled = 0;
    bool IsEof = false;

    if (input != null && output != null) 
    {
      string? line;

      if (!IsEof)
      {
        do
        {
          line = input.ReadLine();
          countHandled++;
          if (line != null)
          {
            bool isMatch;
            if (IsMatch(line))
            {
              isMatch = true;
              _CurrentContext = Process(line, output, _CurrentContext);
            }

            do
            {
              if (IsFinalMatch(line))
              {
                isMatch = true;
                _CurrentContext = FinalProcess(line, output, _CurrentContext);
              }
              else
                isMatch = false;
            }
            while( isMatch);

            string contextName = _CurrentContext.GetType().Name;
            string topStack = _ParseStack.Peek().Element.GetType().Name;
            if (topStack != nameof(TableStartElement))
              output.WriteLine( $"Ln: Context:{contextName} Next: {topStack}");
          }
        } while( line != null);
      }
    }
    else
      if (input == null)
        throw new ArgumentException("Parameter input is NULL");
      else if (output == null)
        throw new ArgumentException("Parameter output is NULL");
  }

  private bool IsMatch(string line)
    => _ParseStack.Peek().Element.IsMatch(line);

  private bool IsFinalMatch(string line)
    => _ParseStack.Peek().Element.IsFinalMatch(line);

  private IContext Process(string line, TextWriter output, IContext current)
  {
    StackTask task = _ParseStack.Peek();
    IContext context = task.Element.Processed(line, output, _CurrentContext);
    Console.WriteLine($"Element: {task.Element.GetType().Name} for: {line}");
    context = task.UponMatch(_ParseStack, context);
    return context;
  }

  private IContext FinalProcess(string line, TextWriter output, IContext current)
  {
    StackTask task = _ParseStack.Pop();
    IContext context = task.Element.Processed(line, output, _CurrentContext);
    context = task.UponFinalMatch(_ParseStack, context);
    return context;
  }

  private static IContext InitStackTaskForTable(Stack<StackTask> stack, IContext current)
  {
    StackTask handleTableEnd = new StackTask() {
      Element = new TableEndElement(),
      UponFinalMatch = InitStackTaskForTable,
      UponMatch = Fault
    };
    StackTask handleTableStart = new StackTask() {
      Element = new TableStartElement(),
      UponFinalMatch = ConfigParseForHeading,
      UponMatch = Fault
    };
    stack.Push(handleTableEnd);
    stack.Push(handleTableStart);
    return new NoneContext();
  }

  private static IContext ConfigParseForHeading(Stack<StackTask> stack, IContext current)
  {
    StackTask headingEnd = new StackTask() {
      Element = new TableHeadEndElement(),
      UponFinalMatch = PrepareForDataRow,
      UponMatch = Fault
    };
    StackTask headingStart = new StackTask() {
      Element = new TableHeadStartElement(),
      UponFinalMatch = ConfigForHeadingRow,
      UponMatch = Fault
    };
    stack.Push(headingEnd);
    stack.Push(headingStart);
    return new NoneContext();
  }

  private static IContext ConfigForHeadingRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      headingTr = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = ConfigForHeadingSpec,
        UponMatch = Fault
      },
      headingEndTr = new StackTask() {
        Element = new THSpecOrEndTrElement(),
        UponMatch = PrepareNextHeadingRow,
        UponFinalMatch = ContextPassThrough
      };
      stack.Push(headingEndTr);
      stack.Push(headingTr);
      return new NoneContext();
  }

  private static IContext Fault(Stack<StackTask> stack, IContext context)
    => throw new Exception(message: "No valid match task.");

  private static IContext PrepareNextHeadingRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      headingTr = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = ConfigForHeadingSpec,
        UponMatch = Fault
      },
      headingEndTr = new StackTask() {
        Element = new THSpecOrEndTrElement(),
        UponMatch = PrepareNextHeadingRow,
        UponFinalMatch = ContextPassThrough
      };
      stack.Push(headingEndTr);
      stack.Push(headingTr);
      return context;
  }

  private static IContext ConfigForHeadingSpec(Stack<StackTask> stack, IContext context)
  {
    StackTask
      headingSpec = new StackTask() {
        Element = new THSpecOrEndTrElement(),
        UponFinalMatch = ContextPassThrough,
        UponMatch = PrepareNextHeadingRow
      };
    stack.Push(headingSpec);
    return context;
  }

  private static IContext ContextPassThrough(Stack<StackTask> stack, IContext context)
  {
    return context;
  }

  /// <summary>
  /// Handle data row after heading is complete.
  /// </summary>
  /// <param name="stack"></param>
  /// <param name="context"></param>
  /// <returns></returns>
  private static IContext PrepareForDataRow(Stack<StackTask> stack, IContext context)
  {
    IContext newContext;

    if (context is TableHeaderContext header)
    {
      if (header.Headings.Count > 0)
      {
        header.Kind = GetKindFrom(header.Headings);
      }

      newContext = header.Kind switch {
        TablePurpose.Actions => new ActionsTableContext(),
        _ => new NoneContext()
      };
    }
    else
    {
      newContext = new NoneContext();
    }
    return newContext;
  }

  private static IContext ConfigForDataRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      dataRow = new StackTask() {
        Element = new TableRowStartElement(),
        UponMatch = ContextPassThrough // replace
      },
      dataRowEnd = new StackTask() {
        Element = new TableRowEndElement(),
        UponMatch = ContextPassThrough // replace
      };
    stack.Push(dataRowEnd);
    stack.Push(dataRow);
    return context;
  }

  private static TablePurpose GetKindFrom(List<string> headings)
  {
    TablePurpose kind = headings[0] switch {
      ACTIONS => TablePurpose.Actions,
      RESOURCE_TYPES => TablePurpose.ResourceTypes,
      CONDITION_KEYS => TablePurpose.ConditionKeys,
      _ => TablePurpose.Unknown
    };
    return kind;
  }

  private bool IsTableStart(string line)
  {
    bool result = false;

    var match = Regex.Match(line, @".*\<table.*");
    result = (match != null && match.Length > 0);

    return result;
  }

  private bool IsTableEnd(string line)
  {
    bool result = false;

    var match = Regex.Match(line, @".*\<\/table.*");
    result = (match != null && match.Length > 0);

    return result;
  }


  const string
    ACTIONS = "Actions",
    RESOURCE_TYPES = "Resource types",
    CONDITION_KEYS = "Condition keys";

}