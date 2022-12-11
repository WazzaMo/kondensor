using Optional;
using System.IO;

using kondensor.cfgenlib;

namespace kondensor.cfgenlib.writer
{
  
  public struct MetadataWriter
  {
    public const string
      CFN_INTERFACE = "AWS::CloudFormation::Interface",
      PARAM_GROUP_TITLE = "ParameterGroups",
      PARAM_LABELS_TITLE = "ParameterLabels",
      LABEL = "Label",
      PARAMS = "Parameters",
      DEFAULT = "default";

    public static StreamWriter Write(
      StreamWriter output,
      Metadata metadata,
      string indent
    )
    {
      YamlWriter.Write(output, "Metadata:", indent);

      var topIndent = indent + YamlWriter.INDENT;
      var topChild = topIndent + YamlWriter.INDENT;

      metadata.Instances.MatchSome( description => {
        YamlWriter.Write(output, "Instances:", topIndent);
        YamlWriter.Write(output, $@"Description: ""{description}""", topChild);
      });
      metadata.Databases.MatchSome( description => {
        YamlWriter.Write(output, "Databases:", topIndent);
        YamlWriter.Write(output, $@"Description: ""{description}""", topChild);
      });
      
      metadata.ParameterGroups.MatchSome(
        (ParameterMetadata paramMeta) => WriteCloudFormationInterface(output, paramMeta, topIndent)
      );
      return output;
    }

    private static void WriteCloudFormationInterface(
      StreamWriter output,
      ParameterMetadata paramMetadata,
      string interfaceIndent
    )
    {
      /*
       0 -> | AWS::CloudFormation::Interface:
       1 -> |   ParameterGroups:
       2 -> |     - 
       3->  |       Label
       4 -> |         default: Foobar
       3 -> |       Parameters
       4 -> |         - VPCID
       1->  |   ParameterLabels: 
       2 -> |     VPCID: 
       3 -> |       default: "Which VPC should this be deployed to?"
       */
      var _0_indent = interfaceIndent;
      var _1_indent = _0_indent + YamlWriter.INDENT;

      if (paramMetadata.ParameterGroups != null && paramMetadata.ParameterGroups.Count > 0)
      {
        YamlWriter.Write(output, $"{CFN_INTERFACE}:", _0_indent);
        YamlWriter.Write(output, $"{PARAM_GROUP_TITLE}:", _1_indent);

        foreach(ParameterMetadata.ParameterGroup group in paramMetadata.ParameterGroups)
        {
          WriteCfnInterfaceParamDeclaration(output, group, _1_indent);
        }
        YamlWriter.Write(output, message: $"{PARAM_LABELS_TITLE}:", _1_indent);

        foreach(ParameterMetadata.ParameterGroup group in paramMetadata.ParameterGroups)
        {
          WriteCfnInterfaceParamLabels(output, group, _1_indent);
        }
      }
    }

    private static void WriteCfnInterfaceParamDeclaration(StreamWriter output, ParameterMetadata.ParameterGroup group, string _1_indent)
    {
      var _2_indent = _1_indent + YamlWriter.INDENT;
      var _3_indent = _2_indent + YamlWriter.INDENT;
      var _4_indent = _3_indent + YamlWriter.INDENT;

      YamlWriter.Write(output, message: "-", _2_indent);
      YamlWriter.Write(output, message: $"{LABEL}:",_3_indent);
      YamlWriter.Write(output, message: $"{DEFAULT}: {group.Label}", _4_indent);
      YamlWriter.Write(output, message: $"{PARAMS}:",_3_indent);

      foreach (var paramNameAndDesc in group.Parameters)
      {
        YamlWriter.Write(output, message: $"- {paramNameAndDesc.Name}", _4_indent);
      }
    }

    private static void WriteCfnInterfaceParamLabels(StreamWriter output, ParameterMetadata.ParameterGroup group, string _1_indent)
    {
      var _2_indent = _1_indent + YamlWriter.INDENT;
      var _3_indent = _2_indent + YamlWriter.INDENT;
      var _4_indent = _3_indent + YamlWriter.INDENT;
      
      /*
       0 -> | AWS::Cloud...
       1 -> |   ParameterLabels:
       2 -> |     VPCID:
       3 -> |       default: "which VPC does this deploy into?"
       */
      foreach(var paramNameAndDesc in group.Parameters)
      {
        YamlWriter.Write(output, message: $"{paramNameAndDesc.Name}:", _2_indent);
        YamlWriter.Write(output, message: $"default: {paramNameAndDesc.Description}:", _3_indent);
      }
    }
  }

}
