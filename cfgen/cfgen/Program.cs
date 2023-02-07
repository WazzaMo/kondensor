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
    Stack stack = new Stack(
      environment:"Test", name: "TestVpc", description: "Test VPC template"
    );

    IpCidrAddress baseRange = new IpCidrAddress(8, 10);
    stack.AddResource<AwsEc2Vpc>(
      id: "MainVpc",
      (vpcProps) => vpcProps
        .SetCidrBlock(baseRange)
        .SetEnableDnsHostnames(true)
        .SetEnableDnsSupport(true)
        .AddTag("Name", "TestVpc"),
      "Second test VPC creatd by API."
    );
    
    AvailabilityZone az = new AvailabilityZone(0, Regions.CurrentRegion());
    IpCidrAddress cidrBlock = new IpCidrAddress(24, 10,1,1,0);
    stack.AddResource<AwsEc2Subnet>(
      id: "InnerSubnet",
      (subnetProps) => subnetProps
        .SetVpcId( stack.Ref("MainVpc"))
        .SetAvailabilityZoneAndCidrBlock(az, cidrBlock),
      optText: "Internal subnet in AZ0"
    );

    YamlWriter writer = new YamlWriter();

    writer.WriteFile(VPC_TEST, stack.Document);
  }

  private static void TestSecGroup()
  {
    const string SECGROUP = "TestSecurityGroup";

    Stack stack = new Stack(
      ENVIRONMENT, SECGROUP,
      description: "Test import VPC and add security group."
    );

    VpcEgress egress = new VpcEgress();
    egress.SetCidrIp( IpCidrAddress.AnyAddress() );
    egress = egress.SetProtocolAndPortRange(IpProtocolType.ALL_PROTOCOLS);

    VpcIngress ingress = new VpcIngress();
    ingress = ingress.SetProtocolAndPortRange(IpProtocolType.HTTP);
    ingress.SetDescription("Allow web traffic");
    ingress.SetCidrIp(IpCidrAddress.AnyAddress());

    stack.AddResource<AwsEc2VpcSecurityGroup>( SECGROUP_ID,
      secGroup => secGroup
        .SetGroupName(SECGROUP)
        .SetVpcId(new Import( new VpcOutput.VpcExport(ENVIRONMENT, VPC_NAME)))
        .AddEgressRule(egress)
        .AddIngressRule(ingress)
        .AddTag(key: "CreatedBy", value: "Test program")
    );

/*
Invalid value for portRange. Must specify both from and to ports with TCP/UDP.
(Service: AmazonEC2; Status Code: 400; Error Code: InvalidParameterValue;
Request ID: 6537f0b6-cf43-4c9a-8c61-d01f3ab1fa56; Proxy: null)
*/
    // secGroup.AddIngressRule(ingress);

    // secGroup.AddOutput( template, ENVIRONMENT, SECGROUP_ID, optionalText: "Allow web traffic.");

    // template.Resources.Add( new Resource( SECGROUP_ID, secGroup) );

    YamlWriter writer = new YamlWriter();
    writer.WriteFile(SEC_GROUP, stack.Document);
  }
}
