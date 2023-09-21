/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


namespace kondensor.Parser;

/// <summary>
/// Delegate describing a rule that attempts to match against a token
/// and returns a <see cref="Matching" /> value.
/// </summary>
/// <param name="token">String to match against</param>
/// <returns>Matching struct value.</returns>
public delegate Matching Matcher(string token );


