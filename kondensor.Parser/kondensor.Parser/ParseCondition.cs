/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace kondensor.Parser;

/// <summary>
/// Used in if/else construct to drive branched parsing.
/// </summary>
/// <param name="matchResult">Match result to find</param>
/// <returns>True if matching, false when not.</returns>
public delegate bool ParseCondition(Matching matchResult);