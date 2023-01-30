/*
 *  (c) Copyright 2020 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;

namespace kondensor.cfgenlib.resources
{

  public struct AwsEc2VpcSecurityGroup : IOutputResourceType, IHasTags
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

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void AddOutput(
      TemplateDocument document,
      string environment,
      string name,
      params string[] optionalText
    )
    {
      // Need security group output
      
      // var (description, condition) = Outputs.GetOutputOptionsFrom(optionalText);
      //  vpcOut = new VpcOutput(environment, name);
      // description.MatchSome( desc => vpcOut.SetDescription(desc) );
      // condition.MatchSome(cond => vpcOut.SetCondition(cond));
      // document.Outputs.MatchSome(outputs => outputs.AddOutput(vpcOut));
    }

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
    
    public AwsEc2VpcSecurityGroup SetVpcId(Ref vpcRef)
    {
      _Properties.SetProp<Ref>(GROUP_VPC, vpcRef);
      return this;
    }
    
    public AwsEc2VpcSecurityGroup SetVpcId(Import vpcImport)
    {
      _Properties.SetProp<Import>(GROUP_VPC, vpcImport);
      return this;
    }

    public AwsEc2VpcSecurityGroup AddEgressRule(VpcEgress egress)
    {
      if (_Properties.HasValue<ResourceList<VpcEgress>>(GROUP_EGRESS) )
        _Properties.Access<ResourceList<VpcEgress>>(GROUP_EGRESS, rList => rList.Add(egress));
      else
        _Properties.SetProp<ResourceList<VpcEgress>>(GROUP_EGRESS, new ResourceList<VpcEgress>(egress));
      return this;
    }

    public AwsEc2VpcSecurityGroup AddIngressRule(VpcIngress ingress)
    {
      if (_Properties.HasValue<ResourceList<VpcIngress>>(GROUP_INGRESS))
        _Properties.Access<ResourceList<VpcIngress>>(GROUP_INGRESS, rList => rList.Add(ingress));
      else
        _Properties.SetProp<ResourceList<VpcIngress>>(GROUP_INGRESS, new ResourceList<VpcIngress>(ingress));
      return this;
    }

    public void AddTag(string key, string value)
    {
      Tag _newTag = new Tag(key, value);

      if (_Properties.HasValue<Tags>(GROUP_TAGS))
        _Properties.Access<Tags>(GROUP_TAGS, (Tags tags) => tags.TagList.Add(_newTag));
      else
        _Properties.SetProp<Tags>(GROUP_TAGS, new Tags(_newTag));
    }

    public AwsEc2VpcSecurityGroup(string description)
    {
      _Properties = new ResourceProperties(
        GROUP_DESCRIPTION,
        GROUP_NAME,
        GROUP_EGRESS,
        GROUP_INGRESS,
        GROUP_TAGS,
        GROUP_VPC
      );
      SetGroupDescription(description);
    }
  }

}