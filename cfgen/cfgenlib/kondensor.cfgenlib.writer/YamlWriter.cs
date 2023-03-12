/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using System.IO;
using System.Collections.Generic;

using Optional;

namespace kondensor.cfgenlib.writer
{

  public struct YamlWriter
  {
    public const string
      INDENT = "  ",
      COMMENT = "#";

    private Dictionary<Type, Writer> Writers;

    public YamlWriter()
    {
      Writers = new Dictionary<Type, Writer>();
      RegisterBasicWriters();
    }

    /// <summary>
    /// Takes a filename and document to write, creates the new YAML template
    /// file and populates it based on template values.
    /// </summary>
    /// <param name="writeFileName">File name to create.</param>
    /// <param name="document">Document to write</param>
    public void WriteFile(string writeFileName, TemplateDocument document)
    {
      using (TextFileStream output = new TextFileStream(writeFileName))
        WriteToStream(output, document);
    }

    /// <summary>
    /// Takes template document to write and writes the text to a buffer
    /// returning the content of the buffer as a single string.
    /// </summary>
    /// <param name="document">Document to write</param>
    /// <returns>String containing all text, formatted as YAML.</returns>
    public string WriteString(TemplateDocument document)
    {
      TextBufferStream bufferStream = new TextBufferStream();
      WriteToStream(bufferStream, document);
      return bufferStream.TakeContentAndClear();
    }

    public void RegisterWriter<T>(WriterDelegate<T> writer) where T : struct
    {
      Writers.Add(
        typeof(T),
        (ITextStream sw, Option<object> o, Type type, string indent) => {
          o.MatchSome( (object value) => writer(sw, (T) value, indent) );
          return sw;
        }
      );
    }

    public void RegisterListWriter<T>(ListWriterDelegate<T> writer) where T : struct
    {
      Writers.Add(
        typeof(List<T>),
        (ITextStream sw, Option<object> o, Type type, string indent) => {
          o.MatchSome( (object list) => writer(sw, (List<T>) list, indent));
          return sw;
        }
      );
    }

    private void WriteToStream(ITextStream output, TemplateDocument document)
    {
      WhenHaveValue<Header>(document.Header, output, indent: "", GetWriter<Header>());
      WhenHaveMultiple<Resource>(document.Resources, output, indent: "", GetListWriter<Resource>());
      WhenHaveValue<Metadata>(document.Metadata, output, indent: "", GetWriter<Metadata>() );
      WhenHaveValue<Outputs>(document.Outputs, output, indent: "", GetWriter<Outputs>() );
    }

    private void RegisterBasicWriters()
    {
      RegisterWriter( (WriterDelegate<Header>) HeaderWriter.Write );
      RegisterListWriter( (ListWriterDelegate<Resource>) ResourceWriter.Write);
      RegisterWriter( (WriterDelegate<Metadata>) MetadataWriter.Write );
      RegisterWriter( (WriterDelegate<Outputs>) OutputsWriter.Write );
    }

    private WriterDelegate<T> GetWriter<T>() where T : struct
    {
      Type writerType = typeof(T);
      Writer writer = Writers.ContainsKey(writerType) 
        ? Writers[writerType]
        : ErrorWriter;
      return (WriterDelegate<T>) ((ITextStream output, T value, string indent) => writer(output, Option.Some<object>((object)value), typeof(T), indent));
    }

    private ListWriterDelegate<T> GetListWriter<T>() where T: struct
    {
      Type listWriterType = typeof(List<T>);
      Writer writer = Writers.ContainsKey(listWriterType)
        ? Writers[listWriterType]
        : ErrorWriter;
      return (ListWriterDelegate<T>) ((ITextStream output, List<T> list, string indent) => writer(output, Option.Some<object>((object) list), typeof(List<T>), indent));
    }

    private static void WhenHaveValue<T>(Option<T> value, ITextStream output, string indent, WriterDelegate<T> writer) where T : struct
    {
      value.MatchSome( toWrite => writer(output, toWrite, indent));
    }

    private static void WhenHaveMultiple<T>(List<T> values, ITextStream output, string indent, ListWriterDelegate<T> writer) where T : struct
    {
      if (values != null && values.Count > 0) {
        writer(output, values, indent);
      }
    }

    private static ITextStream ErrorWriter(ITextStream output, Option<object> o, Type type, string indent)
    {
      YamlWriter.Write(output, message: $"Writer for type {type.Name} was not registered.", indent);
      return output;
    }

    public static ITextStream Write(ITextStream output, string message, string indent)
    {
      string formatted = indent + message;
      output.WriteLine(formatted);
      return output;
    }

    public static ITextStream WriteComment(ITextStream output, string comment, string indent )
    {
      output.WriteLine($"{indent}{COMMENT} {comment}");
      return output;
    }
  }

}