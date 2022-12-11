
using System;
using System.IO;

using kondensor.cfgenlib;

namespace kondensor.cfgenlib.writer
{
  public struct HeaderWriter
  {
    private const string HEADER = @"AWSTemplateFormatVersion: ""2010-09-09""";
    private const string DESCRIPTION = @"Description: ";

    public static StreamWriter Write(StreamWriter output, Header header, string indent)
    {
      YamlWriter.Write(output, HEADER, indent: "");
      header.Description.MatchSome( description =>
        YamlWriter.Write(output, message: $"{DESCRIPTION}: {description}", YamlWriter.INDENT)
      );
      return output;
    }
  }
}