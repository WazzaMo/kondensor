/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using System.Collections.Generic;
using Optional;

using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib
{

  public struct Outputs
  {
    private List<IOutput> _OutputList;

    public void AddOutput(IOutput value)
      => _OutputList.Add(value);
    
    public bool HasOutputs => _OutputList.Count > 0;

    public void ForEachOutput( Action<IOutput> job)
      => _OutputList.ForEach(job);
    
    public Outputs()
    {
      _OutputList = new List<IOutput>();
    }

    /// <summary>
    /// Standard decoder for the optional AddOutput() strings
    /// that give definition or conditions. Order and presence
    /// is important so to pass condition,
    /// a description is mandatory.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="condition"></param>
    public static (Option<string> description, Option<string> condition)
      GetOutputOptionsFrom(string[] optionalText)
    {
      Option<string> description, condition;
      description = optionalText.Length >=1
        ? Option.Some(optionalText[0])
        : Option.None<string>();
      condition = optionalText.Length >= 2
        ? Option.Some(optionalText[1])
        : Option.None<string>();
      return (description, condition);
    }

    public static void AddOutput<Toutput>(TemplateDocument document, Toutput declaration, params string[] optionalText) where Toutput : struct, IOutput
    {
      var (description, condition) = Outputs.GetOutputOptionsFrom(optionalText);
      description.MatchSome( desc => declaration.SetDescription(desc) );
      condition.MatchSome(cond => declaration.SetCondition(cond));
      document.Outputs.MatchSome(outputs => outputs.AddOutput(declaration));
    }

  }

}