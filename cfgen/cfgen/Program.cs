using Optional;

using kondensor.cfgenlib;
using kondensor.cfgenlib.writer;

public class Program
{
  public static void Main(string[] args)
  {
    TemplateDocument template = new TemplateDocument(new Header("Test template"));
    
    YamlWriter writer = new YamlWriter();

    writer.WriteFile(writeFileName:"test.yaml", template);
  }
}
