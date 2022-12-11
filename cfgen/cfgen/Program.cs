using Optional;

using kondensor.cfgenlib;


public class Program
{
  public static void Main(string[] args)
  {
    TemplateDocument template = new TemplateDocument(Option.Some("Application VPC"));
    Console.WriteLine("Hello, World!");
  }
}
