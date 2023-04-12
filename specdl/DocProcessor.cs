/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;

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
        var match = Regex.Match(line, @".*\<table.*");
        if (match != null && match.Length > 0)
        {
          Console.WriteLine($"Match table: {match.Groups[1].Value}");
          isTableFound = true;
        }
      }
    } while( line != null && ! isTableFound);
    return lineCount;
  }

  // private void FindActionsTable(out int countHandled, TextReader input, TextWriter output)
  // {

  // }

  // private void HandleActionsTable(out int countHandled, TextReader input, TextWriter output)
  // {

  // }
}