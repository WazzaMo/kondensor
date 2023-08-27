/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0 or later.
 */


namespace Spec;


public enum TablePurpose
{
  /// <summary>Table is not a known kind and does not help with policies.</summary>
  Unknown,

  /// <summary>Table declares actions and associates with resource types and condition keys</summary>
  Actions,

  /// <summary>Defines the resource type</summary>
  ResourceTypes,

  /// <summary>Defines condition keys</summary>
  ConditionKeys
}