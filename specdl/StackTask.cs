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
public record struct StackTask (IElement Element, Func<Stack<StackTask>, IContext, IContext> UponMatch);