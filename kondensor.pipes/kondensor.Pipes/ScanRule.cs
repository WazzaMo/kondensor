/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

namespace kondensor.Pipes;

/// <summary>
///   A proxy for Matcher rule in Parser assumes that the delegate function
///   uses a Regex object to find a SkipUntil value.
/// </summary>
/// <param name="text">Pipe input to seek a match within.</param>
/// <returns><see href="ScanResult"/> that locates the match.</returns>
public delegate ScanResult ScanRule(string text);
