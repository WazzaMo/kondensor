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
    private ResourceProperties _Properties;
    public string Type => "AWS::EC2::SecurityGroup";

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public void AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      throw new NotImplementedException();
    }

    public void SetGroupDescription(Text description)
      => _Properties.SetProp(name: GROUP_DESCRIPTION, description);

    public void SetGroupName(Text name)
      => _Properties.SetProp(name: GROUP_NAME, name);
    
    

    public void AddTag(string key, string value)
    {
      Tag _newTag = new Tag(key, value);

      if (_Properties.HasValue<Tags>(GROUP_TAGS))
      {
        _Properties.Access<Tags>(GROUP_TAGS, (Tags tags) => tags.TagList.Add(_newTag));
      }
      else
      {
        Tags _tags = new Tags(_newTag);
        _Properties.SetProp<Tags>(GROUP_TAGS, _tags);
      }
    }

    public readonly string 
        GROUP_DESCRIPTION = "GroupDescription",
        GROUP_NAME = "GroupName",
        GROUP_EGRESS = "SecurityGroupEgress",
        GROUP_INGRESS = "SecurityGroupIngress",
        GROUP_TAGS = "Tags",
        GROUP_VPC = "VpcId";

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
    }

    
  }
}