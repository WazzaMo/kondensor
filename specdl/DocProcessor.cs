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

  private Queue<string> _Tokens;

  private int _LinesProcessed;

  public DocProcessor()
  {
    _ParseStack = new Stack<StackTask>();
    _CurrentContext = InitStackTaskForTable(_ParseStack, new NoneContext());
    _Tokens = new Queue<string>();
    _LinesProcessed = 0;
  }

  public void ProcessAllLines(out int countHandled, TextReader input, TextWriter output)
  {
    bool IsEof = false;

    if (input != null && output != null) 
    {
      string? line;

      if (!IsEof)
      {
        do
        {
          line = GetToken(input);
          if (line != null)
          {
            string topStack = _ParseStack.Peek().Element.GetType().Name;

            if (IsMatch(line))
            {
              _CurrentContext = Process(line, output, _CurrentContext);
              string contextName = _CurrentContext.GetType().Name;
              output.WriteLine( $"Mtch: Context:{contextName} Next: {topStack}, {line}");
            }

            if (IsFinalMatch(line))
            {
              topStack = _ParseStack.Peek().Element.GetType().Name;

              _CurrentContext = FinalProcess(line, output, _CurrentContext);

              output.WriteLine( $"Final: Context:{_CurrentContext.GetType().Name} Next: {topStack}, {line}");
            }
          }
        } while( line != null);
        Console.WriteLine($"Number lines processed: {_LinesProcessed}");
      }
    }
    else
    {
      if (input == null)
        throw new ArgumentException("Parameter input is NULL");
      else if (output == null)
        throw new ArgumentException("Parameter output is NULL");
    }
    countHandled = _LinesProcessed;
  }

  /// <summary>
  /// Get next token to process.
  /// </summary>
  /// <param name="input">Input to read line from.</param>
  /// <returns>NULL if end of input file stream or a string.</returns>
  private string? GetToken(TextReader input)
  {
    string? result;

    if (_Tokens.Count == 0)
    {
      string? line = input.ReadLine();
      if (line != null)
      {
        _LinesProcessed++;
        TokeniseLineParts(line);
        result = DequeueTokenOrEmpty();
      }
      else
        result = null;
    }
    else
      result = DequeueTokenOrEmpty();
    
    return result;
  }

  private string DequeueTokenOrEmpty()
    => _Tokens.Count > 0
        ? _Tokens.Dequeue()
        : "";

  private static readonly Regex LineSep = new Regex(pattern: @"\<");
  private void TokeniseLineParts(string line)
  {
    MatchCollection parts = LineSep.Matches(line);

    for(int partIndex = 0; partIndex < parts.Count; partIndex++)
    {
      int index1, index2, length;

      index1 = parts[partIndex].Index;
      index2 = partIndex < (parts.Count - 1)
        ? parts[partIndex + 1].Index
        : line.Length;
      length = index2 - index1;

      string sub = line.Substring(index1, length);
      _Tokens.Enqueue(sub);
    }
  }

  private bool IsMatch(string line)
    => _ParseStack.Peek().Element.IsMatch(line);

  private bool IsFinalMatch(string line)
    => _ParseStack.Peek().Element.IsFinalMatch(line);

  private IContext Process(string line, TextWriter output, IContext current)
  {
    StackTask task = _ParseStack.Peek();
    IContext context = task.Element.Processed(line, output, _CurrentContext);
    // Console.WriteLine($"Element: {task.Element.GetType().Name} for: {line}");
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
      UponMatch = DocGeneralProcessor.Fault
    };
    StackTask handleTableStart = new StackTask() {
      Element = new TableStartElement(),
      UponFinalMatch = ConfigParseForTHead,
      UponMatch = DocGeneralProcessor.Fault
    };
    stack.Push(handleTableEnd);
    stack.Push(handleTableStart);
    return new NoneContext();
  }

  private static IContext ConfigParseForTHead(Stack<StackTask> stack, IContext current)
  {
    StackTask headingEnd = new StackTask() {
      Element = new TableHeadEndElement(),
      UponFinalMatch = PrepareForDataRow,
      UponMatch = DocGeneralProcessor.Fault
    };
    StackTask headingStart = new StackTask() {
      Element = new TableHeadStartElement(),
      UponFinalMatch = ConfigForHeadingTRow,
      UponMatch = DocGeneralProcessor.Fault
    };
    stack.Push(headingEnd);
    stack.Push(headingStart);
    return new NoneContext();
  }

  private static IContext ConfigForHeadingTRow(Stack<StackTask> stack, IContext context)
  {
    StackTask
      headingTr = new StackTask() {
        Element = new TableRowStartElement(),
        UponFinalMatch = ConfigForHeadingSpec,
        UponMatch = DocGeneralProcessor.Fault
      },
      headingEndTr = new StackTask() {
        Element = new THSpecOrEndTrElement(),
        UponMatch = DocGeneralProcessor.ContextPassThrough,
        UponFinalMatch = DocGeneralProcessor.ContextPassThrough
      };
      stack.Push(headingEndTr);
      stack.Push(headingTr);
      return new NoneContext();
  }



  private static IContext ConfigForHeadingSpec(Stack<StackTask> stack, IContext context)
  {
    return new TableHeaderContext();
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
        UponMatch = DocGeneralProcessor.ContextPassThrough // replace
      },
      dataRowEnd = new StackTask() {
        Element = new TableDataOrRowEndElement(),
        UponMatch = DocGeneralProcessor.ContextPassThrough // replace
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

  const string
    ACTIONS = "Actions",
    RESOURCE_TYPES = "Resource types",
    CONDITION_KEYS = "Condition keys";

}