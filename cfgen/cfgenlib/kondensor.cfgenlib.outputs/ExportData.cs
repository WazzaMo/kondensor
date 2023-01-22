/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

 using kondensor.cfgenlib.writer;


namespace kondensor.cfgenlib.outputs
{

  /// <summary>
  /// A data holding and standardising structure.
  /// </summary>
  public struct ExportData : IExport
  {
    private string _Environment;
    private string _Id;

    public string ExportValue => $"{_Environment}-{_Id}";

    public void Write(StreamWriter output, string indent)
    {
        string _1_indent = indent + YamlWriter.INDENT;
        YamlWriter.Write(output, message: "Export:", indent);
        YamlWriter.Write(output, message: $"Name: {ExportValue}", _1_indent);
    }

    public ExportData(string environment, string id)
    {
      _Environment = environment;
      _Id = id;
    }
  }

}