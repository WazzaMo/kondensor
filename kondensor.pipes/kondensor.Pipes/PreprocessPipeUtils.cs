/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.Collections.Generic;

namespace kondensor.Pipes;

public static class PreprocessPipeUtils
{
  /// <summary>
  /// Apply all preprocessors registered into a list against the
  /// given unprocessed text.
  /// </summary>
  /// <param name="preprocessors">List of preprocessors to apply.</param>
  /// <param name="unprocessedText">Original text</param>
  /// <param name="preprocessedText">Final result if applied, or original if not.</param>
  /// <returns>True if preprocessors were applied, False otherwise.</returns>
  public static bool TryApplyPreprocessors(
    List<IPreprocessor> preprocessors,
    char[] unprocessedText,
    out char[] preprocessedText
  )
  {
    char[] text = unprocessedText;
    char[] nextText;

    bool isUpdated = false;
    preprocessors.ForEach(preproc =>
    {
      if (preproc.IsMatch(text))
      {
        isUpdated = preproc.ProcessText(text, out nextText);
        text = nextText;
      }
    });
    if (isUpdated)
      preprocessedText = text;
    else
      preprocessedText = unprocessedText;
    return isUpdated;
  }

}
