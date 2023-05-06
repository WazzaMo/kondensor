/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


public static class DocGeneralProcessor
{
  internal static IContext Fault(Stack<StackTask> stack, IContext context)
  => throw new Exception(message: "No valid match task.");

  internal static IContext ContextPassThrough(Stack<StackTask> stack, IContext context)
  {
    return context;
  }

}