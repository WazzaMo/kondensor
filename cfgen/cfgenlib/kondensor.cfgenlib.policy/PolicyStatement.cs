/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Collections.Generic;
using Optional;

namespace kondensor.cfgenlib.policy
{

  /// <summary>
  /// IAM policy document type.
  /// 
  /// <see href="https://docs.aws.amazon.com/IAM/latest/UserGuide/access_policies.html#policies_session"/>
  /// </summary>
  public struct PolicyStatement
  {
    public Option<string> Sid { get; private set; }
    public EffectValue Effect;
    public Option<string> Principal { get; private set; }

    public List<string> Actions { get; private set; }

    public List<string> Resources { get; private set; }

    public PolicyStatement SetSid(string sid)
    {
      if (String.IsNullOrEmpty(sid))
        return this;
      else if (IsValidSid(sid))
        Sid = Option.Some(sid);
      else
        throw new ArgumentException($"Sid {sid} must be all alphanumeric only.");
      return this;
    }

    public PolicyStatement SetPrincipal(string principal)
    {
      Principal = Option.Some(principal);
      return this;
    }

    public PolicyStatement SetEffect(EffectValue effect)
    {
      Effect = effect;
      return this;
    }

    public PolicyStatement AddAction(string action)
    {
      string quotedAction = $"'{action}'";
      Actions.Add(quotedAction);
      return this;
    }

    public PolicyStatement AddResource(string resource)
    {
      string quotedResource = $"'{resource}'";
      Resources.Add(quotedResource);
      return this;
    }

    public PolicyStatement()
    {
      Sid = Option.None<string>();
      Effect = EffectValue.Empty;
      Principal = Option.None<string>();
      Actions = new List<string>();
      Resources = new List<string>();
    }

    public static bool IsValidSid(string sid)
    {
      bool result;
      if (String.IsNullOrEmpty(sid))
        result = false;
      else
      {
        var chars = sid.ToCharArray();
        if (chars == null)
          result = false;
        else
        {
          result = chars.All(x => Char.IsLetterOrDigit(x));
        }
      }
      return result;
    }
  }

}