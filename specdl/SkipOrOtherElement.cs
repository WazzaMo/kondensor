/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

/// <summary>
/// Skips any non-matching on an IsMatch() and matches the given token IsFinalMatch().
/// </summary>
public struct SkipOrOtherElement : IElement
{
  private IElement _Other;

  public SkipOrOtherElement(IElement other)
  {
    _Other = other;
  }

  public bool IsFinalMatch(string line)
    => _Other.IsFinalMatch(line);

  public bool IsMatch(string line)
    => _Other.IsMatch(line);

  public IContext Processed(string line, TextWriter output, IContext context)
    => IsMatch(line)
    ? context
    : _Other.Processed(line, output, context);
}