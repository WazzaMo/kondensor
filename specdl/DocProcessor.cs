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
  private TextReader? _Input;
  private TextWriter? _Output;

  public DocProcessor()
  {
    _Input = null;
    _Output = null;
  }

  public void ProcessAllLines(out int countHandled)
  {
    countHandled = 0;
    if (_Input != null && _Output != null) 
    {
      string? line;
      do
      {
        line = _Input.ReadLine();
        countHandled++;
        if (line != null)
        {
          _Output.WriteLine(line);
        }
      } while( line != null);
    }
  }

  public void SetInput(TextReader input) => _Input = input;

  public void SetOutput(TextWriter output) => _Output = output;
}