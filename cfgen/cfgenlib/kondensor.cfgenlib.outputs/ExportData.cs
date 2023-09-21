/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
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

    /// <summary>Resource to be exported.</summary>
    private IResourceType _Resource;

    public string ExportValue => ExportUtils.ExportIdFor(_Environment, _Resource);

    public void Write(ITextStream output, string indent)
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
    public ExportData(string environment, IResourceType resource)
    {
      _Environment = environment;
      _Resource = resource;
    }
  }

}