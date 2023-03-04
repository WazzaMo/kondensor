
using System.Collections.Generic;
using kondensor.cfgenlib.writer;

namespace kondensor.cfgenlib.primitives
{

  public struct PrimitiveList<T> : IPrimitive where T : IPrimitive
  {
    private List<T> _List;

    public PrimitiveList(List<T> list)
      => _List = list;
    
    public PrimitiveList(params T[] primitives)
    {
      _List = new List<T>();
      foreach( var onePrimitive in primitives)
        _List.Add(onePrimitive);
    }

    public void Add(T value)
      => _List.Add(value);

    public void Write(StreamWriter output, string name, string indent)
    {
      string  _0_indent = indent,
              _1_indent = _0_indent + YamlWriter.INDENT;
      
      YamlWriter.Write(output, message: $"{name}:", _0_indent);
      foreach( T primitive in _List)
      {
        primitive.WritePrefixed(output, prefix: "-", _1_indent);
      }
    }

    public void WritePrefixed(StreamWriter output, string prefix, string indent)
    {
      string  _0_indent = indent,
              _1_indent = _0_indent + YamlWriter.INDENT;
      
      YamlWriter.Write(output, message: $"{prefix}", _0_indent);
      foreach( T primitive in _List)
      {
        primitive.WritePrefixed(output, prefix: "-", _1_indent);
      }
    }
  }

}