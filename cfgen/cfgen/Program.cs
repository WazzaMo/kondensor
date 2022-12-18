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
    AwsEc2Vpc vpcProps = new AwsEc2Vpc();
    vpcProps.SetCidrBlock(Values.CidrBlock(10,1,1,0, 16));
    vpcProps.SetEnableDnsHostnames(true);
    vpcProps.SetEnableDnsSupport(true);
    // vpcProps.SetIpv4IpamPoolId(IpamPoolIdValues.CidrBlock);
    vpcProps.AddTag("Environment", "Test");
    vpcProps.AddTag(key: "Name", value: "TestVpc");

    template.Resources.Add( new Resource("TestVpc", vpcProps));
    
    YamlWriter writer = new YamlWriter();

    writer.WriteFile(writeFileName:"test.yaml", template);
  }
}
