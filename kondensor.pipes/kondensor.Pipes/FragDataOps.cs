/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System;
using System.IO;
using System.Text;

namespace kondensor.Pipes;

internal static class FragDataOps
{
  internal static bool NeedNewBuffer(ref FragHtmlPipe.FragData _Data)
    => !_Data._EoInput
    && !BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex);

  internal static void GetNewBuffer(ref FragHtmlPipe.FragData _Data)
  {
    var input = _Data._Input.ReadLine();
    if (input == null)
    {
      _Data._EoInput = true;
    }
    _Data._Buffer = BufferUtils.GetWhitespaceTerminatedBufferFromString(input);
    _Data._BufferIndex = 0;
  }

}
