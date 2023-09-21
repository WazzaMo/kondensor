/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using Xunit;

using System.IO;

using kondensor.YamlFormat;
using kondensor.Pipes;
using System.Collections.Generic;

namespace test.kondensor.YamlFormat;

public class SimpleYamlOutput
{
  private StringWriter _TextWritten;
  private IPipe _Pipe;
  private IYamlHierarchy _Yaml;

  public SimpleYamlOutput()
  {
    _TextWritten = new StringWriter();
    _Pipe = new TextPipe( _TextWritten );
    _Yaml = new YamlFormatter(_Pipe);
  }

  [Fact]
  public void Yaml_recipe_for_toast()
  {
    List<string> ingredients = new List<string>(new string[] {
      "Bread", "Butter"
    });
    List<string> method = new List<string>(new string[] {
      "Place toast in a toaster",
      "Depress toasting arm to cook the toast",
      "When it pops up, move the toast to a plate",
      "Spread butter over slice of toast",
      "Enjoy while it is still warm"
    });

    string Expected =
@"RecipeForToast:
  Ingredients:
    - Bread
    - Butter
  Method:
    - Place toast in a toaster
    - Depress toasting arm to cook the toast
    - When it pops up, move the toast to a plate
    - Spread butter over slice of toast
    - Enjoy while it is still warm
".ReplaceLineEndings();

    _Yaml.DeclarationLine(declared: "RecipeForToast", yaml =>
    {
      yaml
        .DeclarationLine(declared: "Ingredients",
          yy => yy.List(ingredients, (i, val) => val.Value(i))
        )
        .DeclarationLine(declared: "Method",
          yy => yy.List(method, (mm, val) => val.Value(mm))
        );
    });
    var builder = _TextWritten.GetStringBuilder();
    var generated = builder.ToString();

    Assert.Equal(Expected, generated);
  }
}