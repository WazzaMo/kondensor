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
using Resources;
using ConditionKeys;
using YamlWriters;

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
  const string
    ACTIONS = "start:table:actions",
    RESOURCES = "start:table:resources",
    CONDKEYS = "start:table:conditionkeys";

  private ActionTable _Actions;
  private ResourceTable _Resources;
  private ConditionKeysTable _ConditionKeys;
  private DocStats _Stats;

  public DocProcessor()
  {
    _Actions = new ActionTable();
    _Resources = new ResourceTable();
    _ConditionKeys = new ConditionKeysTable();
    _Stats = new DocStats();
  }

  public void ProcessAllLines(string sourceUrl, ReplayWrapPipe pipe)
  {
    Production resourcesProd = _Resources.ResourcesTable;
    Production conditionKeysProd = _ConditionKeys.ParseConditionKeysTable;

    _Actions.SetSourceUrl(sourceUrl);
    var parser = Parsing.Group(pipe)
      .SkipUntil( HtmlRules.START_TABLE)
      .MayExpect(HtmlRules.START_TABLE, ACTIONS)
      .Expect( _Actions.ActionsTable )
      ;
      
    if (_Actions.IsReadyToWrite)
      _Stats.ActionStats(parser);

    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .MayExpect( HtmlRules.START_TABLE, RESOURCES)
      .If( node => node.Annotation == RESOURCES, 
        (p, _) => p.EitherProduction(resourcesProd, conditionKeysProd)
      );

    if (_Resources.IsReadyToWrite)
      _Stats.ResourceStats(parser);

    parser
      .SkipUntil(HtmlRules.START_TABLE)
      .MayExpect( HtmlRules.START_TABLE, CONDKEYS)
      .If( node => node.Annotation == CONDKEYS, 
        (p, _) => p.EitherProduction(resourcesProd, conditionKeysProd)
      );
    
    if(_ConditionKeys.IsReadyToWrite)
      _Stats.ConditionKeyStats(parser);
  }

  public void WriteOutput(ReplayWrapPipe pipe)
  {
    Action<YamlFormatter>
      _actions = _Actions.WriteTable,
      _resources = _Resources.WriteTable,
      _conditionKeys = _ConditionKeys.WriteTable;

    YamlFormatter formatter = new YamlFormatter(( IPipeWriter)pipe);
    IYamlHierarchy yaml = formatter;
    yaml.DeclarationLine("ActionsResourcesConditionKeys", (yy) => {
      _actions(formatter);
      _resources(formatter);
      _conditionKeys(formatter);
    });
  }

  public DocStats GetStats() => _Stats;
}