/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


namespace kondensor.Pipes;


public interface IPreprocessor
{
  bool IsMatch(char[] textToMatch);

  bool ProcessText(char[] inputText, out char[] processedText);
}