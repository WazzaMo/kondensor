
using System;
using System.IO;

namespace kondensor.cfgenlib.primitives
{

  public interface IPrimitive
  {
    void Write(StreamWriter output, string indent);
  }

}