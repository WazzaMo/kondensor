
using System.Collections.Generic;

namespace kondensor.cfgenlib.policy.elements
{

  public static class AwsS3ConditionKeys
  {
    public static List<string> ValidKeysFor(AwsS3Actions action)
    {
      List<string> validList = action switch
      {
        AwsS3Actions.AbortMultipartUpload => new List<string>() {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.BypassGovernanceRetention => new() {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:RequestObjectTag/<key>",
          "s3:RequestObjectTagKeys",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-acl",
          "s3:x-amz-content-sha256",
          "s3:x-amz-copy-source",
          "s3:x-amz-grant-full-control",
          "s3:x-amz-grant-read",
          "s3:x-amz-grant-read-acp",
          "s3:x-amz-grant-write",
          "s3:x-amz-grant-write-acp",
          "s3:x-amz-metadata-directive",
          "s3:x-amz-server-side-encryption",
          "s3:x-amz-server-side-encryption-aws-kms-key-id",
          "s3:x-amz-server-side-encryption-customer-algorithm",
          "s3:x-amz-storage-class",
          "s3:x-amz-website-redirect-location",
          "s3:object-lock-mode",
          "s3:object-lock-retain-until-date",
          "s3:object-lock-remaining-retention-days",
          "s3:object-lock-legal-hold",
        },
        AwsS3Actions.CreateAccessPoint => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:locationconstraint",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-acl",
          "s3:x-amz-content-sha256",
        },
        AwsS3Actions.CreateAccessPointForObjectLambda => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.CreateBucket => new() {
          "s3:authType",
          "s3:locationconstraint",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-acl",
          "s3:x-amz-content-sha256",
          "s3:x-amz-grant-full-control",
          "s3:x-amz-grant-read",
          "s3:x-amz-grant-read-acp",
          "s3:x-amz-grant-write",
          "s3:x-amz-grant-write-acp",
          "s3:x-amz-object-ownership"
        },
        AwsS3Actions.CreateJob => new () {
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256",
          "s3:RequestJobPriority",
          "s3:RequestJobOperation",
          "aws:TagKeys",
          "aws:RequestTag/${TagKey}",
          "iam:PassRole"
        },
        AwsS3Actions.CreateMultiRegionAccessPoint => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureversion",
          "s3:signatureAge",
          "s3:TlsVersion"
        },
        AwsS3Actions.DeleteAccessPoint => new () {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteAccessPointForObjectLambda => new () {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteAccessPointPolicy => new () {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteAccessPointPolicyForObjectLambda => new () {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteBucket => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256",
        },
        AwsS3Actions.DeleteBucketPolicy => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteBucketWebsite => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteJobTagging => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256",
          "s3:ExistingJobPriority",
          "s3:ExistingJobOperation"
        },
        AwsS3Actions.DeleteMultiRegionAccessPoint => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureversion",
          "s3:signatureAge",
          "s3:TlsVersion"
        },
        AwsS3Actions.DeleteObject => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteObjectTagging => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:ExistingObjectTag/<key>",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteObjectVersion => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:versionid",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteObjectVersionTagging => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:ExistingObjectTag/<key>",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:versionid",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteStorageLensConfiguration => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DeleteStorageLensConfigurationTagging => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DescribeJob => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.DescribeMultiRegionAccessPointOperation => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureversion",
          "s3:signatureAge",
          "s3:TlsVersion"
        },
        AwsS3Actions.GetAccelerateConfiguration => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPoint => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointConfigurationForObjectLambda => new () {
          "s3:DataAccessPointArn",
          "s3:DataAccessPointAccount",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointForObjectLambda => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointPolicy => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointPolicyForObjectLambda => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointPolicyStatus => new List<string>() {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccessPointPolicyStatusForObjectLambda => new () {
          "s3:DataAccessPointAccount",
          "s3:DataAccessPointArn",
          "s3:AccessPointNetworkOrigin",
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAccountPublicAccessBlock => new List<string>() {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetAnalyticsConfiguration => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetBucketAcl => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },
        AwsS3Actions.GetBucketCORS => new () {
          "s3:authType",
          "s3:ResourceAccount",
          "s3:signatureAge",
          "s3:signatureversion",
          "s3:TlsVersion",
          "s3:x-amz-content-sha256"
        },

        _ => new List<string>()
      };
      return validList;
    }
  }

}