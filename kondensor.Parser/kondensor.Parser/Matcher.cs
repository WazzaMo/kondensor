/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace kondensor.Parser;

/// <summary>
/// Delegate describing a rule that attempts to match against a token
/// and returns a <see cref="Matching" /> value.
/// </summary>
/// <param name="token">String to match against</param>
/// <returns>Matching struct value.</returns>
public delegate Matching Matcher(string token );


