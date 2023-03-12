/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System;
using System.IO;

using kondensor.cfgenlib;

namespace kondensor.cfgenlib.writer
{
  public struct HeaderWriter
  {
    private const string HEADER = @"AWSTemplateFormatVersion: ""2010-09-09""";
    private const string DESCRIPTION = @"Description: ";

    public static ITextStream Write(ITextStream output, Header header, string indent)
    {
      string  _0_indent = indent;

      YamlWriter.Write(output, HEADER, indent: _0_indent);
      header.Description.Write(output, "Description", _0_indent);
      return output;
    }
  }
}