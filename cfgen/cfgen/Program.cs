using Optional;

using kondensor.cfgenlib;
using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

public class Program
{
  static readonly string
    ENVIRONMENT = "Test",
    VPC_NAME = "TestVpc";

  public static void Main(string[] args)
  {
    
  }

  private static void TestVpc()
  {
    TemplateDocument template = new TemplateDocument(new Header("Test VPC template"));
    AwsEc2Vpc vpcProps = new AwsEc2Vpc();
    vpcProps.SetCidrBlock(Values.CidrBlock(10,1,1,0, 16));
    vpcProps.SetEnableDnsHostnames(true);
    vpcProps.SetEnableDnsSupport(true);
    // vpcProps.SetIpv4IpamPoolId(IpamPoolIdValues.CidrBlock);
    vpcProps.AddTag("Environment", "Test");
    vpcProps.AddTag(key: "Name", value: "TestVpc");

    template.Resources.Add( new Resource("TestVpc", vpcProps));
    vpcProps.AddOutput(document: template, ENVIRONMENT, VPC_NAME, "First test VPC");
    
    YamlWriter writer = new YamlWriter();

    writer.WriteFile(writeFileName:"test.yaml", template);
  }

  private static void TestSecGroup()
  {
    TemplateDocument template = new TemplateDocument( new Header("Test import VPC and add security group."));
    AwsEc2VpcSecurityGroup secGroup = new AwsEc2VpcSecurityGroup("Test security group");
    secGroup.AddTag("CreatedBy", "Test program");
    secGroup.SetGroupName("TestSecurityGroup");
    secGroup.SetVpcId(new Import( new VpcOutput.VpcExport(ENVIRONMENT, VPC_NAME)));

    VpcEgress egress = new VpcEgress();
    egress.SetCidrIp( IpCidrAddress.AnyAddress() );
    egress.SetIpProtocol(new IpProtocol(IpProtocol.IpProtocolType.DNS_TCP));
  }
}
