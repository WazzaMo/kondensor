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
using kondensor.cfgenlib.composites;
using kondensor.cfgenlib.policy;

public class Program
{
  static readonly string
    ENVIRONMENT = "Test",
    VPC_ID = "MainVpc",
    SECGROUP_ID = "TestSecGrp",

    VPC_TEST = "VpcTest.yaml",
    SEC_GROUP = "SecGrp.yaml",
    IAM_USER_WM_ID = "WMtestUser",
    IAM_IAM_LIST_POLICY = "ListUsersAndRoles",
    IAM_TEST_USER = "TestUser",
    TEST_USERNAME = "WTest";

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
    )
    .AddResource<AwsIamUser>(
      IAM_TEST_USER,
      user => user
        .SetUserName(TEST_USERNAME)
        .SetLoginProfile(UserLoginProfile.Create("badPassword#").SetPasswordResetRequired(false)),
      "Test user to later apply policy against."
    )
    .AddResource<AwsIamPolicy>(IAM_USER_WM_ID, policy => policy
      .SetPolicyName("ListUsersAndRoles")
      .SetUsers(TEST_USERNAME)
      .SetPolicyDocument( PolicyDocValue.Create( IAM_IAM_LIST_POLICY,
        s1 => s1.SetSid("S1")
        .SetEffect(EffectValue.Allow)
        .AddAction("iam:ListRoles")
        .AddAction("iam:ListUsers")
        .AddResource("*")
      )),
      optText: "User role for IAM"
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

    stack
      .AddImport<AwsEc2Vpc>(VPC_ID, out Import vpc)
      .AddResource<AwsEc2VpcSecurityGroup>( SECGROUP_ID,
        secGroup => secGroup
          .SetGroupDescription(description: "Allow web traffic in and all protocols-1234567890123456789")
          .SetGroupName(SECGROUP)
          .SetVpcId(vpc)
          .AddIngressRule( stack.AddChild<VpcIngress>(
            "Web", ingress => ingress
            .SetProtocolAndPortRange(IpProtocolType.HTTP)
            .SetDescription("Allow web traffic - inlined")
            .SetCidrIp(IpCidrAddress.AnyAddress())
          ))
          .AddEgressRule( stack.AddChild<VpcEgress>("AnyEgress",
            propSetter: egress => 
              egress
              .SetCidrIp(IpCidrAddress.AnyAddress())
              .SetProtocolAndPortRange(IpProtocolType.ALL_PROTOCOLS)
            )
          )
          .AddTag(key: "CreatedBy", value: "Test program")
      );

    YamlWriter writer = new YamlWriter();
    writer.WriteFile(SEC_GROUP, stack.Document);
  }
}
