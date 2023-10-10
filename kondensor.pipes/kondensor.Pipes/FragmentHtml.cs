/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


namespace kondensor.Pipes;

/// <summary>
/// Defines the idea of fragmented HTML through base functions, 
/// answering what is or is not a fragment?
/// </summary>
internal static class FragmentHtml
{
  internal static bool IsFragmentSpace(ref FragContext _Data, char _char)
  {
    bool result = Char.IsWhiteSpace(_char);
    DoStartEndTracking(ref _Data, _char);

    return result;
  }

  internal static bool IsFragmentLone(ref FragContext _Data, char _char)
  {
    bool result = _char == '>';
    DoStartEndTracking(ref _Data, _char);
    return result;
  }

  internal static bool IsFragment(ref FragContext _Data, char _char)
  {
    bool result;

    if (IsInTag(ref _Data))
    {
      result = Char.IsPunctuation(_char)
        || Char.IsLetterOrDigit(_char)
        || _char == '<'
        || _char == '=';
    }
    else
    {
      result = _char != '<';
    }
    DoStartEndTracking(ref _Data, _char);
    return result;
  }

  internal static bool IsFragmentEnd(ref FragContext _Data, char _char)
  {
    bool result = (IsInTag(ref _Data) && Char.IsWhiteSpace(_char))
      || _char == '>'
      || _char == '<';

    DoStartEndTracking(ref _Data, _char);

    return result;
  }

  internal static bool IsInTag(ref FragContext _Data)
    => _Data._NumTagStart == (_Data._NumTagEnd + 1);

  internal static bool IsBetweenTags(ref FragContext _Data)
    => _Data._NumTagStart == _Data._NumTagEnd;


  internal static bool IsSingleCharFragment(ref FragContext _Data)
    => BufferUtils.IsValidIndex(_Data._Buffer, _Data._BufferIndex)
      && IsFragmentLone(ref _Data, _Data._Buffer[_Data._BufferIndex]);

  /// <summary>Internal state tracking that can be called many times on same char.</summary>
  /// <param name="_Data">Context to use</param>
  /// <param name="_char">Char to handle</param>
  private static void DoStartEndTracking(ref FragContext _Data, char _char)
  {
    if (_char == '<')
      _Data._NumTagStart = _Data._NumTagEnd + 1;
    else if (_char == '>')
      _Data._NumTagEnd = _Data._NumTagStart + 1;
  }

}