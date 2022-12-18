
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.outputs
{

  public interface IExport
  {
    Text Name { get; }
    Text ExportValue { get; }

    void Write( StreamWriter output, string indent);
  }
}