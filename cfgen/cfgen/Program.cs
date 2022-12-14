using Optional;

using kondensor.cfgenlib;
using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.primitives;

public class Program
{
  public static void Main(string[] args)
  {
    TemplateDocument template = new TemplateDocument(new Header("Test template"));
    AwsEc2Vpc vpc = new AwsEc2Vpc();
    vpc.SetCidrBlock(Values.CidrBlock(10,1,1,0, 16));
    vpc.SetEnableDnsHostnames(true);
    vpc.SetEnableDnsSupport(true);
    vpc.SetIpv4IpamPoolId("poolId");
    vpc.AddTag("Environment", "Test");

    template.Resources.Add( new Resource("TestVpc", vpc));
    
    YamlWriter writer = new YamlWriter();

    writer.WriteFile(writeFileName:"test.yaml", template);
  }
}
