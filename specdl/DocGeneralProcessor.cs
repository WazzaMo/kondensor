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
    if (context is TableHeaderContext headerCtx)
    {
      var kind = Enum.Format(typeof(TablePurpose),headerCtx.Kind, "G");
      int numHeadings = headerCtx.Headings.Count;
      string headings = headerCtx.Headings.Aggregate("", (x, lst) => $"{lst},{x}");
      Console.WriteLine($"TableHeader: {kind}, {headings}");
    }
    else
    {
      Console.WriteLine(context.GetType().Name + " is current context.");
    }
    return context;
  }

}