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
  public void ProcessAllLines(out int countHandled, TextReader input, TextWriter output)
  {
    countHandled = 0;
    if (input != null && output != null) 
    {
      string? line;
      countHandled = FindAnyTableStart(countHandled, out bool IsEof, input, output);

      if (!IsEof)
      {
        do
        {
          line = input.ReadLine();
          countHandled++;
          if (line != null)
          {
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

  private int FindAnyTableStart(int countHandled, out bool IsEof, TextReader input, TextWriter output)
  {
    string? line;
    bool isTableFound = false;
    int lineCount = countHandled;
    IsEof = false;

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
        if (IsTableStart(line))
        {
          isTableFound = true;
          TableHeader header = IdentifyTable(lineCount, out IsEof, input, output);
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

  private TableHeader IdentifyTable(int countHandled, out bool IsEof, TextReader input, TextWriter output)
  {
    string? line;
    TableHeader header = new TableHeader();
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