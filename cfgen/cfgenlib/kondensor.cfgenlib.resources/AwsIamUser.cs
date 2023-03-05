/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.composites;
using kondensor.cfgenlib.policy;
using kondensor.cfgenlib.outputs;


namespace kondensor.cfgenlib.resources
{

  /// <summary>
  /// Creates an IAM user resource.
  /// </summary>
  public struct AwsIamUser : IResourceType
  {
    const string TYPE = "AWS::IAM::User",
      GROUPS = "Groups",
      LOGIN_PROFILE = "LoginProfile",
      MANAGED_POLICY_ARNS = "ManagedPolicyArns",
      PATH = "Path",
      PERMISSIONS_BOUNDARY = "PermissionsBoundary",
      POLICIES = "Policies",
      TAGS = "Tags",
      USERNAME = "UserName";

    private ResourceProperties _Properties;

    public string Type => TYPE;

    public string Id { get; private set; }

    public Dictionary<string, ResourceProperty> Properties => _Properties.Properties;

    public IResourceType AddOutput(TemplateDocument document, string environment, string name, params string[] optionalText)
    {
      OutputData output = new OutputData(environment, this);
      Outputs.AddOutput(document, output, optionalText);
      return this;
    }

    public IResourceType AddTag(string key, string value)
    {
      _Properties.AddTag(key, value);
      return this;
    }

    public void AssertRequiredPropertiesSet()
    {
      // None required
    }

    public void setId(string id)
      => Id = id;

    public AwsIamUser SetGroups(params string[] grpList)
    {
      List<Text> groups = Primitive.ConvertToPrimitive<string,Text>(s => new Text(s), grpList);
      PrimitiveList<Text> list = new PrimitiveList<Text>(groups);
      _Properties.SetProp<PrimitiveList<Text>>(GROUPS, list);
      return this;
    }

    public AwsIamUser SetLoginProfile(UserLoginProfile profile)
    {
      _Properties.SetProp<UserLoginProfile>(LOGIN_PROFILE, profile);
      return this;
    }

    public AwsIamUser SetManagedPolicyArns(params string[] arns)
    {
      List<Text> values = Primitive.ConvertToPrimitive<string, Text>(s => new Text(s), arns);
      PrimitiveList<Text> arnList = new PrimitiveList<Text>(values);
      _Properties.SetProp(MANAGED_POLICY_ARNS, arnList);
      return this;
    }

    public AwsIamUser SetPath(string path)
    {
      Text p = new Text(path);
      _Properties.SetProp(PATH, p);
      return this;
    }

    public AwsIamUser SetPermissionsBoundary(string policyArn)
    {
      Text arn = new Text(policyArn);
      _Properties.SetProp(PERMISSIONS_BOUNDARY, arn);
      return this;
    }

    public AwsIamUser SetPolicies(params PolicyDocValue[] policies)
    {
      PrimitiveList<PolicyDocValue> pList = new PrimitiveList<PolicyDocValue>( policies);
      _Properties.SetProp(POLICIES, pList);
      return this;
    }

    public AwsIamUser SetUserName(string uName)
    {
      _Properties.SetProp<Text>(USERNAME, new Text(uName));
      return this;
    }

    public AwsIamUser()
    {
      _Properties = new ResourceProperties(
        GROUPS,
        LOGIN_PROFILE,
        MANAGED_POLICY_ARNS,
        PATH,
        PERMISSIONS_BOUNDARY,
        POLICIES,
        TAGS,
        USERNAME
      );
      Id = Resource.DEFAULT_ID;
    }

  } // end resource

}