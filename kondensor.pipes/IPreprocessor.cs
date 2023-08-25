/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


namespace Parser;

public interface IPreprocessor
{
  bool IsMatch(char[] textToMatch);

  bool ProcessText(char[] inputText, out char[] processedText);
}