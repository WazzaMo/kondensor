/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.resources;

namespace kondensor.cfgenlib.api
{

  public struct Vpc
  {
    private AwsEc2Vpc _Vpc;
    private string _Id;
    private string _Environment;

    public Vpc SetCidrBlock(IpCidrAddress cidr)
    {
      _Vpc.SetCidrBlock(cidr);
      return this;
    }
    public Vpc SetEnableDnsHostnames(bool isEnable)
    {
      _Vpc.SetEnableDnsHostnames(isEnable);
      return this;
    }
    public Vpc SetEnableDnsSupport(bool isEnable)
    {
      _Vpc.SetEnableDnsSupport(isEnable);
      return this;
    }
    public Vpc SetInstanceTenancy(string tenancy)
    {
      _Vpc.SetInstanceTenancy(tenancy);
      return this;
    }
    public Vpc SetIpv4IpamPoolId(IpamPoolIdValues poolId)
    {
      _Vpc.SetIpv4IpamPoolId(poolId);
      return this;
    }
    public Vpc SetIpv4NetmaskLength(int length)
    {
      _Vpc.SetIpv4NetmaskLength(length);
      return this;
    }

    public Vpc AddTag(string key, string value)
    {
      _Vpc.AddTag(key, value);
      return this;
    }

    public Vpc SaveTo(TemplateDocument template, params string[] optionalText)
    {
      Resource resource = new Resource(_Id, _Vpc);
      _Vpc.AddOutput(template, _Environment, _Id, optionalText);
      template.Resources.Add(resource);
      return this;
    }

    public Ref Ref => new Ref(_Id);

    public Vpc(string id, string environment)
    {
      _Vpc = new AwsEc2Vpc();
      _Id = id;
      _Environment = environment;
    }
  }

}