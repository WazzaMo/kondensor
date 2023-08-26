/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace kondensor.Parser;

/// <summary>
/// Used to declare a named production of expected symbols.
/// </summary>
/// <param name="parser">The parseAction object from where symbols are Expected.</param>
/// <returns>The same parser back for fluid API.</returns>
public delegate ParseAction Production(ParseAction parser);