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
using System.Globalization;

namespace Spec;

/// <summary>
/// Facility to capture metrics during parsing phases.
/// </summary>
public class DocStats
{
  private int _CountOfActionMismatches;
  private int _CountOfResourceMismatches;
  private int _CountOfConditionKeyMismatches;
  private bool _IsEmpty;

  public DocStats()
  {
    _IsEmpty = true;
    _CountOfActionMismatches = 0;
    _CountOfConditionKeyMismatches = 0;
    _CountOfResourceMismatches = 0;
  }

  /// <summary>Record stats after parsing actions table.</summary>
  /// <param name="parser">Parser to sample</param>
  public ParseAction ActionStats(ParseAction parser)
  {
    _IsEmpty = false;
    _CountOfActionMismatches = parser.NumberOfMismatches;
    return parser;
  }
  
  /// <summary>Record metrics after resources table.</summary>
  /// <param name="parser">Parser to sample</param>
  public ParseAction ResourceStats(ParseAction parser)
  {
    _IsEmpty = false;
    _CountOfResourceMismatches = parser.NumberOfMismatches;
    return parser;
  }
  
  /// <summary>Record metrics after condition keys table</summary>
  /// <param name="parser">Parser to sample.</param>
  public ParseAction ConditionKeyStats(ParseAction parser)
  {
    _IsEmpty = false;
    _CountOfConditionKeyMismatches = parser.NumberOfMismatches;
    return parser;
  }
  
  public bool IsEmpty => _IsEmpty;

  public int ActionTableErrors => _CountOfActionMismatches;

  public int ResourceTableErrors => _CountOfResourceMismatches;

  public int ConditionKeyTableErrors => _CountOfConditionKeyMismatches;

  public bool IsParsedOk
    => ! _IsEmpty
    && _CountOfActionMismatches == 0
    && _CountOfResourceMismatches == 0
    && _CountOfConditionKeyMismatches == 0;
}