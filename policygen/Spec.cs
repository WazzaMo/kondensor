/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Optional;

using System.Text.RegularExpressions;
using System.IO;


public static class Spec
{
  private static readonly Regex Re = new Regex(
    @"^(\w+)\s([\w \-]+)[\t]?([\w \-]+)[\t]?([\w \-]*)[\t]?",
    RegexOptions.Compiled | RegexOptions.IgnoreCase
  );

  private static Option<string> OptLineBuffer = Option.None<string>();
  private static bool EndOfFile = false;

  public static Option<string> GetLine(TextReader reader)
  {
    Option<string> line;

    if (OptLineBuffer.HasValue)
    {
      OptLineBuffer.MatchSome( text => line = Option.Some(text) );
      OptLineBuffer = Option.None<string>();
    }
    else
    {
      if (reader.Peek() > 0)
      {
        try {
          string? value = reader.ReadLine();
          if (value == null)
          {
            EndOfFile = true;
          }
          else
          {
            line = Option.Some(value);
          }
        }
        catch(Exception e)
        {
          
        }
      }
    }
  }

  public static void ProcessDeclaration(string line)
  {
    var matches = Re.Matches(line);
    //mm = re.search("^(\w+)\s([\w \-]+)[\t]?([\w \-]+)[\t]?([\w \-]*)[\t]?", line)
    if (matches != null && matches.Count > 0)
    {
      GroupCollection groups = matches[0].Groups;
      Console.WriteLine($"There are {groups.Count} matched groups.");
      var action = groups[1].Value;
      var description = groups[2].Value;
      var access = groups[3].Value;

      // Console.WriteLine($"#0 -> '{groups[0]}'");

      Console.WriteLine($"Action: {action}");
      Console.WriteLine($"Desc: {description}");
      Console.WriteLine($"Access: {access}");
      for (int index = 4; index < groups.Count; index++)
      {
        Console.WriteLine($"#{index} -> '{groups[index]}'");
      }
      //nextLine = getLine()
    // resource_types = getWord(nextLine)
    // items = getList()
    // (conditions, dependent_actions) = splitList(items, "iam")
    // print("Action: %s" % action)
    // print("Desc: %s" % description)
    // print("Access: %s" % access)
    // print("Res Types: %s" % resource_types)
    // print("%d Conditions: %s" % (len(conditions),conditions) )
    // print("%d Dependent Actions: %s" % (len(dependent_actions), dependent_actions))
    // print("-----")

    }
  }

  public static void ReadStream(TextReader reader)
  {
    var line = reader.ReadLine();
    if (line != null)
    {
      ProcessDeclaration(line);
    }
    //
  }

}
