/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

/// <summary>
/// Handle HTML element as part of a spec.
/// Provide ability to do match test and then
/// process the information.
/// </summary>
public interface IElement
{
  bool IsMatch(string line);

  IContext Processed(string line, TextWriter output, IContext context);
}