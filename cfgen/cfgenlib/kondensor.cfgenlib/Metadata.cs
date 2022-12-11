using Optional;

namespace kondensor.cfgenlib
{
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