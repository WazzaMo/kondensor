/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Text;
using System.Collections.Generic;

using Parser;

namespace YamlWriters;

public static class YamlUtils
{
  public const string
    INDENT = "  ",
    SEP = ": ",
    CHILD = "  ",
    UNESCQUOTE = "'",
    ESCQUOTE = "\"",
    SEQUENCE = "- ";

  public static IPipeWriter Key(this IPipeWriter writer, string key)
    => writer.WriteFragment(key + SEP);

  public static IPipeWriter KeyLine(this IPipeWriter writer, string key)
    => writer.WriteFragmentLine(key + SEP);

  public static IPipeWriter Indent(this IPipeWriter writer, int count)
    => writer.WriteFragment( NTimes(count, INDENT) );

  public static IPipeWriter Url(this IPipeWriter writer, string url)
    =>  writer.WriteFragment(UNESCQUOTE).WriteFragment(url).WriteFragment(UNESCQUOTE);
  
  public static IPipeWriter UrlLIne(this IPipeWriter writer, string url)
    =>  writer.WriteFragmentLine(UNESCQUOTE + url + UNESCQUOTE);
  
  public static IPipeWriter Quote(this IPipeWriter writer, string url)
    =>  writer.WriteFragment(UNESCQUOTE).WriteFragment(url).WriteFragment(UNESCQUOTE);
  
  public static IPipeWriter QuoteLine(this IPipeWriter writer, string value)
    =>  writer.WriteFragmentLine(UNESCQUOTE + value + UNESCQUOTE);

  public static IPipeWriter ListItem(this IPipeWriter writer, int indent, Action<IPipeWriter> member)
  {
    writer.Indent(count: indent).WriteFragment(SEQUENCE);
    member(writer);
    return writer;
  }

  public static IPipeWriter StringList(this IPipeWriter writer, string member)
    => writer.WriteFragment(SEQUENCE).WriteFragment(fragment: $"{UNESCQUOTE}{member}{UNESCQUOTE}");

  public static IPipeWriter StringListLine(this IPipeWriter writer, string member)
    => writer.WriteFragmentLine( SEQUENCE + UNESCQUOTE + member + UNESCQUOTE);
  
  public static string NTimes(int count, string fragment)
  {
    StringBuilder text = new StringBuilder();
    for(int index = 0; index < count; index++)
    {
      text.Append(fragment);
    }
    return text.ToString();
  }
}