/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0 or later.
 */


namespace kondensor.cfgenlib.policy.elements
{

  public enum AwsS3Actions
  {

///	Grants permission to abort a multipart upload	Write	object
AbortMultipartUpload,
/// <summary>
/// Grants permission to allow circumvention of governance-mode
/// object retention settings	Permissions management object
/// </summary>
BypassGovernanceRetention,


/// <summary>
/// Grants permission to create a new access point
/// Write	accesspoint
/// </summary>
CreateAccessPoint,

/// <summary>
/// Grants permission to create an object lambda enabled accesspoint
/// Write	objectlambdaaccesspoint
/// </summary>
CreateAccessPointForObjectLambda,

/// <summary>
/// Grants permission to create a new bucket
/// Write bucket
/// </summary>
CreateBucket,

/// <summary>
/// Grants permission to create a new Amazon S3 Batch Operations job
/// Write s3:authType
/// </summary>
CreateJob,

/// <summary>
/// Grants permission to create a new Multi-Region Access Point
/// Write multiregionaccesspoint*
/// </summary>
CreateMultiRegionAccessPoint,

/// <summary>
/// Grants permission to delete the access point named in the URI
/// Write accesspoint*
/// </summary>
DeleteAccessPoint,

/// <summary>
/// Grants permission to delete the object lambda enabled access point named in the URI
/// Write	objectlambdaaccesspoint*
/// </summary>
DeleteAccessPointForObjectLambda,

/// <summary>
/// Grants permission to delete the policy on a specified access point
/// Permissions management accesspoint*
/// </summary>
DeleteAccessPointPolicy,

/// <summary>
/// Grants permission to delete the policy on a specified object lambda enabled access point
/// Permissions management objectlambdaaccesspoint*
/// </summary>
DeleteAccessPointPolicyForObjectLambda,

/// <summary>
/// Grants permission to delete the bucket named in the URI
/// Write	bucket*
/// </summary>
DeleteBucket,

/// <summary>
/// Grants permission to delete the policy on a specified bucket
/// Permissions management bucket*
/// </summary>
DeleteBucketPolicy,

/// <summary>
/// Grants permission to remove the website configuration for a bucket
/// Write	bucket*
/// </summary>
DeleteBucketWebsite,

/// <summary>
/// Grants permission to remove tags from an existing Amazon S3 Batch Operations job
/// Tagging	job*
/// </summary>
DeleteJobTagging,

/// <summary>
/// Grants permission to delete the Multi-Region Access Point named in the URI
/// Write	multiregionaccesspoint*
/// </summary>
DeleteMultiRegionAccessPoint,

/// <summary>
/// Grants permission to remove the null version of an object and insert a delete marker, which becomes the current version of the object
/// Write	object*
/// </summary>
DeleteObject,

/// <summary>
/// Grants permission to use the tagging subresource to remove the entire tag set from the specified object
/// Tagging	object*
/// </summary>
DeleteObjectTagging,

/// <summary>
/// Grants permission to remove a specific version of an object
/// Write	 object*
/// </summary>
DeleteObjectVersion,

/// <summary>
/// Grants permission to remove the entire tag set for a specific version of the object
/// Tagging	object*
/// </summary>
DeleteObjectVersionTagging,

/// <summary>
/// Grants permission to delete an existing Amazon S3 Storage Lens configuration
/// Write	storagelensconfiguration*
/// </summary>
DeleteStorageLensConfiguration,

/// <summary>
/// Grants permission to remove tags from an existing Amazon S3 Storage Lens configuration
/// Tagging	storagelensconfiguration*
/// </summary>
DeleteStorageLensConfigurationTagging,

/// <summary>
/// Grants permission to retrieve the configuration parameters and status for a batch operations job
/// Read	job*
/// </summary>
DescribeJob,

/// <summary>
/// Grants permission to retrieve the configurations for a Multi-Region Access Point
/// Read multiregionaccesspointrequestarn*
/// </summary>
DescribeMultiRegionAccessPointOperation,

/// <summary>
/// Grants permission to uses the accelerate subresource to return the Transfer Acceleration
/// state of a bucket, which is either Enabled or Suspended
/// Read bucket*
/// </summary>
GetAccelerateConfiguration,

/// <summary>
/// Grants permission to return configuration information about the specified access point
/// Read
/// </summary>
GetAccessPoint,

/// <summary>
/// Grants permission to retrieve the configuration of the object lambda enabled access point
/// Read objectlambdaaccesspoint*
/// </summary>
GetAccessPointConfigurationForObjectLambda,

/// <summary>
/// Grants permission to create an object lambda enabled accesspoint
/// Read objectlambdaaccesspoint*
/// </summary>
GetAccessPointForObjectLambda,

/// <summary>
/// Grants permission to returns the access point policy associated with the specified
/// access point
/// Read accesspoint*
/// </summary>
GetAccessPointPolicy,

/// <summary>
/// Grants permission to returns the access point policy associated with
/// the specified object lambda enabled access point
/// Read objectlambdaaccesspoint*
/// </summary>
GetAccessPointPolicyForObjectLambda,

/// <summary>
/// Grants permission to return the policy status for a specific access point policy
/// Read accesspoint*
/// </summary>
GetAccessPointPolicyStatus,

/// <summary>
/// Grants permission to return the policy status for a specific object lambda access\
/// point policy
/// Read objectlambdaaccesspoint*
/// </summary>
GetAccessPointPolicyStatusForObjectLambda,

/// <summary>
/// Grants permission to retrieve the PublicAccessBlock configuration for an AWS account
/// Read
/// </summary>
GetAccountPublicAccessBlock,

/// <summary>
/// Grants permission to get an analytics configuration from an Amazon S3 bucket,
/// identified by the analytics configuration ID
/// Read bucket*
/// </summary>
GetAnalyticsConfiguration,

/// <summary>
/// Grants permission to use the acl subresource to return the access control list (ACL) of an Amazon S3 bucket
/// Read bucket*
/// </summary>
GetBucketAcl,

/// <summary>
/// Grants permission to return the CORS configuration information set for an Amazon S3 bucket
/// Read bucket*
/// </summary>
GetBucketCORS


  }

}