/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;
using System.Collections.Generic;

using Parser;
using HtmlParse;

using Actions;

namespace Spec;


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
  private ActionTable _Actions;

  public DocProcessor()
  {
    _Actions = new ActionTable();
  }

  public void ProcessAllLines(string sourceUrl, ReplayWrapPipe pipe)
  {
    _Actions.SetSourceUrl(sourceUrl);
    var parser = Parsing.Group(pipe)
      .Expect(_Actions.ActionsTable)
      ;
  }

  
}