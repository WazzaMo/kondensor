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
    _CurrentContext = new NoneContext();
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
            if 
            output.WriteLine(line);
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
  
  private IContext Process(string line, TextWriter output, IContext current)
  {
    StackTask task = _ParseStack.Pop();
    IContext context = task.Element.Processed(line, output, _CurrentContext);
    context = task.UponMatch(_ParseStack, context);
    return context;
  }

  private static IContext InitStackTaskForTable(Stack<StackTask> stack, IContext current)
  {
    StackTask handleTableEnd = new StackTask() {
      Element = new TableEndElement(),
      UponMatch = InitStackTaskForTable
    };
    StackTask handleTableStart = new StackTask() {
      Element = new TableStartElement(),
      UponMatch = ConfigParseForHeading
    };
    stack.Push(handleTableEnd);
    stack.Push(handleTableStart);
    return new NoneContext();
  }

  private static IContext ConfigParseForHeading(Stack<StackTask> stack, IContext current)
  {
    StackTask headingEnd = new StackTask() {
      Element = new TableHeadEndElement(),
      UponMatch = PrepareForDataRow
    };
    StackTask headingStart = new StackTask() {
      Element = new TableHeadStartElement(),
      // UponMatch = H
    };
    return new NoneContext();
  }

  private static IContext HandleEndHeading(Stack<StackTask> stack, IContext context)
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

  /// <summary>
  /// Handle data row after heading is complete.
  /// </summary>
  /// <param name="stack"></param>
  /// <param name="context"></param>
  /// <returns></returns>
  private static IContext PrepareForDataRow(Stack<StackTask> stack, IContext context)
  {
    //
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

  private int FindAnyTableStart(int countHandled, out bool IsEof, TextReader input, TextWriter output)
  {
    string? line;
    bool isTableFound = false;
    int lineCount = countHandled;
    IsEof = false;

    _Elements.Push(new TableStartElement());
    // _Elements.Push(new TableHeadStartElement());
    // _Elements.Push(new TableRowStartElement());
    // _Elements.Push(new THSpecElement());
    // _Elements.Push(new TableRowEndElement());
    // _Elements.Push(new TableHeadEndElement());
    _Elements.Push(new TableEndElement());
    do
    {
      line = input.ReadLine();
      lineCount++;
      if (line == null)
      {
        IsEof = true;
      }
      else
      {
        if (_Elements.Peek().IsMatch(line))
        //---- Make stack based.
        
        if (IsTableStart(line))
        {
          isTableFound = true;
          TableHeaderContext header = IdentifyTable(lineCount, out IsEof, input, output);
          Console.WriteLine($"Table: {header.Kind}  - |{header.Headings[0]}|");
          lineCount = header.LinesProcessed;
        }
      }
    } while( line != null && ! isTableFound);
    return lineCount;
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

  private TableHeaderContext IdentifyTable(int countHandled, out bool IsEof, TextReader input, TextWriter output)
  {
    string? line;
    TableHeaderContext header = new TableHeaderContext();
    List<string> columns = new List<string>();
    int countLines = countHandled;

    bool isEndHeader = false;

    IsEof = false;
    
    do{
      line = input.ReadLine();
      if (line == null)
      {
        IsEof = true;
      }
      else
      {
        countLines++;
        if (IsStartRow(line))
        {}
        else if (IsEndRow(line))
          isEndHeader = true;
        else if (IsHeading(line, out string heading))
          columns.Add(heading);
      }
    }
    while(! IsEof && ! isEndHeader);
    header.LinesProcessed = countLines;
    header.Headings = columns.ToArray();
    header.Kind = header.Headings[0] switch {
      ACTIONS => TablePurpose.Actions,
      RESOURCE_TYPES => TablePurpose.ResourceTypes,
      CONDITION_KEYS => TablePurpose.ConditionKeys,
      _ => TablePurpose.Unknown
    };
    return header;
  }

  private bool IsStartRow(string line)
  {
    bool result = false;

    var match = Regex.Match(line, pattern: @".*\<tr\>");
    if (match != null && match.Length > 0)
      result = true;
    return result;
  }

  private bool IsEndRow(string line)
  {
    bool result = false;

    var match = Regex.Match(line, pattern: @".*\<\/tr\>");
    if (match != null && match.Length > 0)
      result = true;
    return result;
  }

  private bool IsHeading(string line, out string headingText)
  {
    bool result = false;
    headingText = "";

    var match = Regex.Match(line, pattern: @".*\<th\>([\w\s\(\*\)]+)<\/th\>.*");
    if (match != null && match.Length > 0)
    {
      result = true;
      var groups = match.Groups;
      for(int index = 1; index < groups.Count; index++)
        Console.WriteLine($"Match {index} = {groups[index].Value}");
      if (groups.Count > 1)
        headingText = groups[1].Value;
    }
    return result;
  }

  private void HandleActionsTable(string line, TextWriter output)
  {

  }
}