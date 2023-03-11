/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2VpcSecurityGroup : IResourceType
  {
    public readonly string 
        GROUP_DESCRIPTION = "GroupDescription",
        GROUP_NAME = "GroupName",
        GROUP_EGRESS = "SecurityGroupEgress",
        GROUP_INGRESS = "SecurityGroupIngress",
        GROUP_TAGS = "Tags",
        GROUP_VPC = "VpcId";

    private ResourceProperties _Properties;
    public string Type => "AWS::EC2::SecurityGroup";

    public string Id {get; private set;}

    public void setId(string id) => Id = id;

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public AwsEc2VpcSecurityGroup SetGroupDescription(string description)
    {
      _Properties.SetProp(name: GROUP_DESCRIPTION, new Text(description) );
      return this;
    }

    public AwsEc2VpcSecurityGroup SetGroupName(string name)
    {
      _Properties.SetProp(name: GROUP_NAME, new Text(name) );
      return this;
    }
       
    public AwsEc2VpcSecurityGroup SetVpcId(IReference vpcId)
    {
      _Properties.SetProp<IReference>(GROUP_VPC, vpcId);
      return this;
    }

    public AwsEc2VpcSecurityGroup AddEgressRule(AwsEc2SecurityGroupEgress egress)
    {
      if (_Properties.HasValue<ResourceList<AwsEc2SecurityGroupEgress>>(GROUP_EGRESS) )
        _Properties.Access<ResourceList<AwsEc2SecurityGroupEgress>>(GROUP_EGRESS, rList => rList.Add(egress));
      else
        _Properties.SetProp<ResourceList<AwsEc2SecurityGroupEgress>>(GROUP_EGRESS, new ResourceList<AwsEc2SecurityGroupEgress>(egress));
      return this;
    }

    public AwsEc2VpcSecurityGroup AddIngressRule(AwsEc2SecurityGroupIngress ingress)
    {
      if (_Properties.HasValue<ResourceList<AwsEc2SecurityGroupIngress>>(GROUP_INGRESS))
        _Properties.Access<ResourceList<AwsEc2SecurityGroupIngress>>(GROUP_INGRESS, rList => rList.Add(ingress));
      else
        _Properties.SetProp<ResourceList<AwsEc2SecurityGroupIngress>>(GROUP_INGRESS, new ResourceList<AwsEc2SecurityGroupIngress>(ingress));
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      Tag _newTag = new Tag(key, value);

      if (_Properties.HasValue<Tags>(GROUP_TAGS))
        _Properties.Access<Tags>(GROUP_TAGS, (Tags tags) => tags.TagList.Add(_newTag));
      else
        _Properties.SetProp<Tags>(GROUP_TAGS, new Tags(_newTag));
      return this;
    }



    IResourceType IResourceType.AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      OutputData data = new OutputData(environment, this);
      Outputs.AddOutput(document, data, optionalText);
      return this;
    }

    public void AssertRequiredPropertiesSet()
    {
      if (! _Properties.HasValue<Text>(GROUP_DESCRIPTION))
        throw new PropertyNeededException(GROUP_DESCRIPTION);
    }

    public AwsEc2VpcSecurityGroup()
    {
      _Properties = new ResourceProperties(
        GROUP_DESCRIPTION,
        GROUP_NAME,
        GROUP_EGRESS,
        GROUP_INGRESS,
        GROUP_TAGS,
        GROUP_VPC
      );
      Id = Resource.DEFAULT_ID;
    }
  }

}