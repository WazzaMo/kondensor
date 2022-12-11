using System.IO;
using System.Collections.Generic;

using Optional;

namespace kondensor.cfgenlib.writer
{

  public struct YamlWriter
  {
    public const string INDENT = "  ";

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
      using (StreamWriter output = File.CreateText(writeFileName))
      {
        WhenHaveValue<Header>(document.Header, output, indent: "", GetWriter<Header>());
        WhenHaveMultiple<Resource>(document.Resources, output, indent: "", GetWriter<Resource>());
      }
    }

    public void RegisterWriter<T>(WriterDelegate<T> writer) where T : struct
    {
      Writers.Add(
        typeof(T),
        (StreamWriter sw, Option<object> o, Type type, string indent) => {
          o.MatchSome( (object value) => writer(sw, (T) value, indent) );
          return sw;
        }
      );
    }

    private void RegisterBasicWriters()
    {
      RegisterWriter( (WriterDelegate<Header>) HeaderWriter.Write );
      RegisterWriter( (WriterDelegate<Resource>) ResourceWriter.Write);
      RegisterWriter( (WriterDelegate<Metadata>) MetadataWriter.Write );
    }

    private WriterDelegate<T> GetWriter<T>() where T : struct
    {
      Type writerType = typeof(T);
      Writer writer = Writers.ContainsKey(writerType) 
        ? Writers[writerType]
        : ErrorWriter;
      return (WriterDelegate<T>) ((StreamWriter output, T value, string indent) => writer(output, Option.Some<object>((object)value), typeof(T), indent));
    }

    private static void WhenHaveValue<T>(Option<T> value, StreamWriter output, string indent, WriterDelegate<T> writer) where T : struct
    {
      value.MatchSome( toWrite => writer(output, toWrite, indent));
    }

    private static void WhenHaveMultiple<T>(List<T> values, StreamWriter output, string indent, WriterDelegate<T> writer) where T : struct
    {
      if (values != null && values.Count > 0) {
        foreach( T value in values)
        {
          writer(output, value, indent);
        }
      }
    }

    private static StreamWriter ErrorWriter(StreamWriter output, Option<object> o, Type type, string indent)
    {
      YamlWriter.Write(output, message: $"Writer for type {type.Name} was not registered.", indent);
      return output;
    }

    public static StreamWriter Write(StreamWriter output, string message, string indent)
    {
      output.WriteLine(indent + message);
      return output;
    }
  }

}