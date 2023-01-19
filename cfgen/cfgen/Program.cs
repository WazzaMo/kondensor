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
    VPC_NAME = "TestVpc",
    SECGROUP_ID = "TestSecGrp",

    VPC_TEST = "VpcTest.yaml",
    SEC_GROUP = "SecGrp.yaml";

  public static void Main(string[] args)
  {
    TestVpc();
    TestSecGroup();
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

    writer.WriteFile(VPC_TEST, template);
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
    egress = egress.SetProtocolAndPortRange(IpProtocolType.ALL_PROTOCOLS);
    secGroup.AddEgressRule(egress);

/*
Invalid value for portRange. Must specify both from and to ports with TCP/UDP.
(Service: AmazonEC2; Status Code: 400; Error Code: InvalidParameterValue;
Request ID: 6537f0b6-cf43-4c9a-8c61-d01f3ab1fa56; Proxy: null)
*/
    VpcIngress ingress = new VpcIngress();
    ingress = ingress.SetProtocolAndPortRange(IpProtocolType.HTTP);
    ingress.SetDescription("Allow web traffic");
    ingress.SetCidrIp(IpCidrAddress.AnyAddress());
    secGroup.AddIngressRule(ingress);

    secGroup.AddOutput( template, ENVIRONMENT, SECGROUP_ID, optionalText: "Allow web traffic.");

    template.Resources.Add( new Resource( SECGROUP_ID, secGroup) );

    YamlWriter writer = new YamlWriter();
    writer.WriteFile(SEC_GROUP, template);
  }
}
