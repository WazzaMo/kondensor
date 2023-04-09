/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.IO;

public interface IProcessor
{
  void SetInput(TextReader input);
  void SetOutput(TextWriter output);

  void ProcessAllLines(out int countHandled);
}