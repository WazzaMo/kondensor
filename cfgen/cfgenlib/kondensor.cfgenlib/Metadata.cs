/*
 *  (c) Copyright 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */

using Optional;

namespace kondensor.cfgenlib
{

/*
AWSTemplateFormatVersion: 2010-09-09
Description: Test template
Resources:
  TestVpc:
    Type: 'AWS::EC2::VPC'
    Properties:
      CidrBlock: 10.1.1.0/16
      EnableDnsHostnames: 'True'
      EnableDnsSupport: 'True'
      Ipv4IpamPoolId: CidrBlock
      Tags:
        - Key: Environment
          Value: Test
    Metadata:
      'AWS::CloudFormation::Designer':
        id: ef1990e7-b3ed-4927-aeae-ffa5eda9fe4f
Metadata:
  'AWS::CloudFormation::Designer':
    ef1990e7-b3ed-4927-aeae-ffa5eda9fe4f:
      size:
        width: 150
        height: 150
      position:
        x: -200
        'y': 80
      z: 1
      embeds: []
*/

  /// <summary>
  /// Metadata works with the Cfn Designer but can be used to organise
  /// parameters.
  /// 
  /// 
  /// </summary>
  public struct Metadata {
    public Option<Description> Instances;
    public Option<Description> Databases;
    public Option<ParameterMetadata> ParameterGroups;


  /* Missing
  - Authentication - AWS::CloudFormation::Authentication
  - AWS::CloudFormation::Init
  */

    public Metadata(
      Option<Description> instances,
      Option<Description> databases,
      Option<ParameterMetadata> parameterGroups
    )
    {
      Instances = instances;
      Databases = databases;
      ParameterGroups = parameterGroups;
    }
  }
}