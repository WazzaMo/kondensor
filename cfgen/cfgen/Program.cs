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
    VPC_ID = "MainVpc",
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

    IpCidrAddress baseRange = new IpCidrAddress(16, 10,1,1);
    stack.AddResourceAndGetRef<AwsEc2Vpc>(
      VPC_ID,
      out Ref vpcId,
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
        .SetVpcId( vpcId)
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


    VpcIngress ingress = new VpcIngress();
    ingress = ingress.SetProtocolAndPortRange(IpProtocolType.HTTP);
    ingress.SetDescription("Allow web traffic");
    ingress.SetCidrIp(IpCidrAddress.AnyAddress());

    stack
      .AddImport<AwsEc2Vpc>(VPC_ID, out Import vpc)
      .AddResource<AwsEc2VpcSecurityGroup>( SECGROUP_ID,
        secGroup => secGroup
          .SetGroupDescription(description: "Allow web traffic in and all protocols-1234567890123456789")
          .SetGroupName(SECGROUP)
          .SetVpcId(vpc)
          .AddEgressRule( stack.AddChild<VpcEgress>("AnyEgress",
            propSetter: egress => 
              egress
              .SetCidrIp(IpCidrAddress.AnyAddress())
              .SetProtocolAndPortRange(IpProtocolType.ALL_PROTOCOLS)
            )
          )
          .AddIngressRule(ingress)
          .AddTag(key: "CreatedBy", value: "Test program")
      );

    YamlWriter writer = new YamlWriter();
    writer.WriteFile(SEC_GROUP, stack.Document);
  }
}
