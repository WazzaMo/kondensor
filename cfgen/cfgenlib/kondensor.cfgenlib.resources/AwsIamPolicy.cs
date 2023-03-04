/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.policy;
using kondensor.cfgenlib.outputs;

namespace kondensor.cfgenlib.resources
{

  public struct AwsIamPolicy : IResourceType
  {
    const string TYPE = "AWS::IAM::Policy",
      GROUPS = "Groups",
      POLICY_DOCUMENT = "PolicyDocument",
      POLICY_NAME = "PolicyName",
      ROLES = "Roles",
      USERS = "Users",
      TAGS = "Tags";


    private ResourceProperties _Properties;

    public string Type => TYPE;

    public string Id { get; private set; }

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      OutputData output = new OutputData(environment, this);
      ExportData export = new ExportData(environment, this);
      Outputs.AddOutput(document, output, optionalText);
      return this;
    }

    public AwsIamPolicy AddGroup(string groupName)
    {
      Text group = new Text(groupName);

      if (_Properties.HasValue<PrimitiveList<Text>>(GROUPS))
      {
        _Properties.Access<PrimitiveList<Text>>(GROUPS, list => {
          list.Add(group);
        });
          // _Properties.SetProp(GROUPS, list);
      }
      else
      {
        PrimitiveList<Text> list = new PrimitiveList<Text>(group);
        _Properties.SetProp<PrimitiveList<Text>>(GROUPS, list);
      }
      return this;
    }

    /// <summary>
    /// Specify all the groups as strings in a single call
    /// and set it as the group list alue.
    /// </summary>
    /// <param name="groups">string: names of groups to apply policy on.</param>
    /// <returns>Self</returns>
    public AwsIamPolicy SetGroups(params string[] groups)
    {
      PrimitiveList<Text> groupList = new PrimitiveList<Text>();
      for(int index = 0; index < groups.Length; index++)
      {
        groupList.Add( new Text(groups[index]));
      }
      _Properties.SetProp(GROUPS, groupList);
      return this;
    }

    public AwsIamPolicy SetPolicyDocument(PolicyDocument document)
    {
      _Properties.SetProp<PolicyDocValue>(POLICY_DOCUMENT, new PolicyDocValue(document));
      return this;
    }

    public AwsIamPolicy SetPolicyName(string name)
    {
      _Properties.SetProp<Text>(POLICY_NAME, new Text(name));
      return this;
    }

    public AwsIamPolicy SetRoles(params string[] roles)
    {
      PrimitiveList<Text> roleList = new PrimitiveList<Text>();
      for(int index = 0; index < roles.Length; index++)
      {
        Text roleName = new Text(roles[index]);
        roleList.Add(roleName);
      }
      _Properties.SetProp(ROLES, roleList);
      return this;
    }

    public AwsIamPolicy SetUsers(params string[] users)
    {
      PrimitiveList<Text> userList = new PrimitiveList<Text>(
        Primitive.ConvertToPrimitive<string,Text>( s => new Text(s), users)
      );

      _Properties.SetProp(USERS, userList);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public void AssertRequiredPropertiesSet()
    {
      _Properties.AssertHasValue<PolicyDocValue>(POLICY_DOCUMENT);
    }

    public void setId(string id)
      => Id = id;

    public AwsIamPolicy()
    {
      _Properties = new ResourceProperties(
        GROUPS,
        POLICY_DOCUMENT,
        POLICY_NAME,
        ROLES,
        USERS,
        TAGS
      );
      Id = Resource.DEFAULT_ID;
    }
  }
}
