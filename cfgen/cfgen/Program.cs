/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;

using kondensor.cfgenlib;
using kondensor.cfgenlib.writer;
using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;
using kondensor.cfgenlib.api;

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

    IpCidrAddress baseRange = new IpCidrAddress(8, 10);
    Vpc vpc = new Vpc("TestVpc", ENVIRONMENT);
    vpc
      .SetCidrBlock(baseRange)
      .SetEnableDnsHostnames(true)
      .SetEnableDnsSupport(true)
      .AddTag("Environment", ENVIRONMENT)
      .AddTag("Name", "TestVpc")
      .SaveTo(template, "First test VPC via API");

    // AwsEc2Vpc vpcProps = new AwsEc2Vpc();
    // vpcProps.SetCidrBlock(baseRange);
    // vpcProps.SetEnableDnsHostnames(true);
    // vpcProps.SetEnableDnsSupport(true);
    // // vpcProps.SetIpv4IpamPoolId(IpamPoolIdValues.CidrBlock);
    // vpcProps.AddTag("Environment", "Test");
    // vpcProps.AddTag(key: "Name", value: "TestVpc");

    // template.Resources.Add( new Resource("TestVpc", vpcProps));
    // vpcProps.AddOutput(document: template, ENVIRONMENT, VPC_NAME, "First test VPC");
    
    // Ref vpcRef = new Ref("TestVpc");


    AwsEc2Subnet subProps = new AwsEc2Subnet();
    // subProps.SetVpcId(vpcRef);
    subProps.SetVpcId(vpc.Ref);
    AvailabilityZone az = new AvailabilityZone(0, Regions.CurrentRegion());
    IpCidrAddress cidrBlock = new IpCidrAddress(24, 10,1,1,0);
    subProps.SetAvailabilityZoneAndCidrBlock(az, cidrBlock);
    subProps.AddOutput(template, ENVIRONMENT, "InnerSubnet", "Internal subnet in AZ0");
    template.Resources.Add( new Resource(id: "InnerSubnet", subProps));

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
