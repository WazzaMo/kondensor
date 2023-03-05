/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.composites
{

  public struct UserLoginProfile : IComposite
  {
    const string
      PASSWORD = "Password",
      PASSWORD_RESET_REQUIRED = "PasswordResetRequired";

    public string Password {get; private set; }
    public Option<Bool> PasswordResetRequired { get; private set; }

    public UserLoginProfile SetPassword(string _pass)
    {
      Password = _pass;
      return this;
    }

    public UserLoginProfile SetPasswordResetRequired(bool value)
    {
      PasswordResetRequired = Option.Some( value: new Bool(value));
      return this;
    }

    public void Write(StreamWriter output, string name, string indent)
    {
      string
        _0_indent = indent,
        _1_indent = _0_indent + YamlWriter.INDENT,
        prefix = $"{name}:",
        passwordLIne = $"{PASSWORD}: {Password}";

      YamlWriter.Write(output, prefix, _0_indent);
      YamlWriter.Write(output, passwordLIne, _1_indent);
      PasswordResetRequired.MatchSome(isRequired => {
        isRequired.WritePrefixed(output, $"{PASSWORD_RESET_REQUIRED}:", _1_indent);
        // string value = isRequired ? "True" : "False";
        // YamlWriter.Write(output, message: $"{PASSWORD_RESET_REQUIRED}: {value}", _1_indent);
      });
    }

    /// <summary>
    /// Not supported because this primitive is a sub-document and the
    /// <see cref="Write"/> method should be used instead.
    /// </summary>
    /// <param name="output"></param>
    /// <param name="prefix"></param>
    /// <param name="indent"></param>
    public void WritePrefixed(StreamWriter output, string prefix, string indent)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The best way to create a login profile for a user creation
    /// template.
    /// </summary>
    /// <param name="password">Initial password to set.</param>
    /// <returns><see cref="UserLoginProfile"/> value</returns>
    public static UserLoginProfile Create(string password)
    {
      return new UserLoginProfile(password);
    }

    public UserLoginProfile(string _pass)
    {
      Password = _pass;
      PasswordResetRequired = Option.None<Bool>();
    }
  }


}