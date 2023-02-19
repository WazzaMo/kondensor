/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// Represents an internetGateway on a VPC.
  /// Needs an attachment after this is declared.
  /// See https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-ec2-internetgateway.html
  /// </summary>
  public struct AwsEc2InternetGateway : IResourceType
  {
    private ResourceProperties _Properties;

    public string Type => "Type: AWS::EC2::InternetGateway";

    public string Id { get; private set; }

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      OutputData declaration = new OutputData(environment, this);
      Outputs.AddOutput(document, declaration, optionalText);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public void AssertRequiredPropertiesSet()
    {}

    public void setId(string id)
      => Id = id;

    public AwsEc2InternetGateway()
    {
      _Properties = new ResourceProperties(
        "Tags"
      );
      Id = Resource.DEFAULT_ID;
    }
  }

}