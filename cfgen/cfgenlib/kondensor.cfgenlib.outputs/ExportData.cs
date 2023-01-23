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

    /// <summary>Prefix indicating type</summary>
    private string _ExpPrefix;
    private string _Id;

    public string ExportValue => $"{_Environment}-{_ExpPrefix}{_Id}";

    public void Write(StreamWriter output, string indent)
    {
        string _1_indent = indent + YamlWriter.INDENT;
        YamlWriter.Write(output, message: "Export:", indent);
        YamlWriter.Write(output, message: $"Name: {ExportValue}", _1_indent);
    }

    /// <summary>
    /// Create export with information for uniqueness and clarity
    /// about the environment and resource being declared.
    /// </summary>
    /// <param name="environment">Environment name</param>
    /// <param name="prefix">Prefix (Sub, Vpc etc)</param>
    /// <param name="id">Name or ID</param>
    public ExportData(string environment, string prefix, string id)
    {
      _Environment = environment;
      _ExpPrefix = prefix;
      _Id = id;
    }
  }

}