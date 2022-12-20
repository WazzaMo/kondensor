
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.outputs
{

  public interface IExport
  {
    string ExportValue { get; }

    void Write( StreamWriter output, string indent);
  }
}