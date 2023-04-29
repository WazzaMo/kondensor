/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

/// <summary>
/// A matching task on a parsing stack with a job to perform
/// upon a match.
/// </summary>
public struct StackTask
{
  private static readonly IElement __EMPTY_ELEMENT = new ParaElement();

  public IElement Element;
  public Func<Stack<StackTask>, IContext, IContext> UponMatch;
  public Func<Stack<StackTask>, IContext, IContext> UponFinalMatch;

  public StackTask()
  {
    Element = __EMPTY_ELEMENT;
    UponMatch = DocGeneralProcessor.Fault;
    UponFinalMatch = DocGeneralProcessor.ContextPassThrough;
  }
}