
AbortMultipartUpload	Grants permission to abort a multipart upload	Write	
object*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

BypassGovernanceRetention	Grants permission to allow circumvention of governance-mode object retention settings	Permissions management	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:RequestObjectTag/<key>

s3:RequestObjectTagKeys

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-copy-source

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

s3:x-amz-metadata-directive

s3:x-amz-server-side-encryption

s3:x-amz-server-side-encryption-aws-kms-key-id

s3:x-amz-server-side-encryption-customer-algorithm

s3:x-amz-storage-class

s3:x-amz-website-redirect-location

s3:object-lock-mode

s3:object-lock-retain-until-date

s3:object-lock-remaining-retention-days

s3:object-lock-legal-hold

CreateAccessPoint	Grants permission to create a new access point	Write	
accesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:locationconstraint

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

CreateAccessPointForObjectLambda	Grants permission to create an object lambda enabled accesspoint	Write	
objectlambdaaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

CreateBucket	Grants permission to create a new bucket	Write	
bucket*

s3:authType

s3:locationconstraint

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

s3:x-amz-object-ownership

CreateJob	Grants permission to create a new Amazon S3 Batch Operations job	Write		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:RequestJobPriority

s3:RequestJobOperation

aws:TagKeys

aws:RequestTag/${TagKey}

iam:PassRole

CreateMultiRegionAccessPoint	Grants permission to create a new Multi-Region Access Point	Write	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

DeleteAccessPoint	Grants permission to delete the access point named in the URI	Write	
accesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteAccessPointForObjectLambda	Grants permission to delete the object lambda enabled access point named in the URI	Write	
objectlambdaaccesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteAccessPointPolicy	Grants permission to delete the policy on a specified access point	Permissions management	
accesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteAccessPointPolicyForObjectLambda	Grants permission to delete the policy on a specified object lambda enabled access point	Permissions management	
objectlambdaaccesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteBucket	Grants permission to delete the bucket named in the URI	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteBucketPolicy	Grants permission to delete the policy on a specified bucket	Permissions management	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteBucketWebsite	Grants permission to remove the website configuration for a bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteJobTagging	Grants permission to remove tags from an existing Amazon S3 Batch Operations job	Tagging	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:ExistingJobPriority

s3:ExistingJobOperation

DeleteMultiRegionAccessPoint	Grants permission to delete the Multi-Region Access Point named in the URI	Write	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

DeleteObject	Grants permission to remove the null version of an object and insert a delete marker, which becomes the current version of the object	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteObjectTagging	Grants permission to use the tagging subresource to remove the entire tag set from the specified object	Tagging	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteObjectVersion	Grants permission to remove a specific version of an object	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

DeleteObjectVersionTagging	Grants permission to remove the entire tag set for a specific version of the object	Tagging	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

DeleteStorageLensConfiguration	Grants permission to delete an existing Amazon S3 Storage Lens configuration	Write	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DeleteStorageLensConfigurationTagging	Grants permission to remove tags from an existing Amazon S3 Storage Lens configuration	Tagging	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DescribeJob	Grants permission to retrieve the configuration parameters and status for a batch operations job	Read	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

DescribeMultiRegionAccessPointOperation	Grants permission to retrieve the configurations for a Multi-Region Access Point	Read	
multiregionaccesspointrequestarn*

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

GetAccelerateConfiguration	Grants permission to uses the accelerate subresource to return the Transfer Acceleration state of a bucket, which is either Enabled or Suspended	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPoint	Grants permission to return configuration information about the specified access point	Read		
s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointConfigurationForObjectLambda	Grants permission to retrieve the configuration of the object lambda enabled access point	Read	
objectlambdaaccesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointForObjectLambda	Grants permission to create an object lambda enabled accesspoint	Read	
objectlambdaaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointPolicy	Grants permission to returns the access point policy associated with the specified access point	Read	
accesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointPolicyForObjectLambda	Grants permission to returns the access point policy associated with the specified object lambda enabled access point	Read	
objectlambdaaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointPolicyStatus	Grants permission to return the policy status for a specific access point policy	Read	
accesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccessPointPolicyStatusForObjectLambda	Grants permission to return the policy status for a specific object lambda access point policy	Read	
objectlambdaaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAccountPublicAccessBlock	Grants permission to retrieve the PublicAccessBlock configuration for an AWS account	Read		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetAnalyticsConfiguration	Grants permission to get an analytics configuration from an Amazon S3 bucket, identified by the analytics configuration ID	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketAcl	Grants permission to use the acl subresource to return the access control list (ACL) of an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketCORS	Grants permission to return the CORS configuration information set for an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketLocation	Grants permission to return the Region that an Amazon S3 bucket resides in	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketLogging	Grants permission to return the logging status of an Amazon S3 bucket and the permissions users have to view or modify that status	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketNotification	Grants permission to get the notification configuration of an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketObjectLockConfiguration	Grants permission to get the Object Lock configuration of an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:signatureversion

GetBucketOwnershipControls	Grants permission to retrieve ownership controls on a bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketPolicy	Grants permission to return the policy of the specified bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketPolicyStatus	Grants permission to retrieve the policy status for a specific Amazon S3 bucket, which indicates whether the bucket is public	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketPublicAccessBlock	Grants permission to retrieve the PublicAccessBlock configuration for an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketRequestPayment	Grants permission to return the request payment configuration for an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketTagging	Grants permission to return the tag set associated with an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketVersioning	Grants permission to return the versioning state of an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetBucketWebsite	Grants permission to return the website configuration for an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetEncryptionConfiguration	Grants permission to return the default encryption configuration an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetIntelligentTieringConfiguration	Grants permission to get an or list all Amazon S3 Intelligent Tiering configuration in a S3 Bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetInventoryConfiguration	Grants permission to return an inventory configuration from an Amazon S3 bucket, identified by the inventory configuration ID	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetJobTagging	Grants permission to return the tag set of an existing Amazon S3 Batch Operations job	Read	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetLifecycleConfiguration	Grants permission to return the lifecycle configuration information set on an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetMetricsConfiguration	Grants permission to get a metrics configuration from an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetMultiRegionAccessPoint	Grants permission to return configuration information about the specified Multi-Region Access Point	Read	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

GetMultiRegionAccessPointPolicy	Grants permission to returns the access point policy associated with the specified Multi-Region Access Point	Read	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

GetMultiRegionAccessPointPolicyStatus	Grants permission to return the policy status for a specific Multi-Region Access Point policy	Read	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

GetMultiRegionAccessPointRoutes	Grants permission to return the route configuration for a Multi-Region Access Point	Read	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

GetObject	Grants permission to retrieve objects from Amazon S3	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectAcl	Grants permission to return the access control list (ACL) of an object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectAttributes	Grants permission to retrieve attributes related to a specific object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectLegalHold	Grants permission to get an object's current Legal Hold status	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectRetention	Grants permission to retrieve the retention settings for an object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectTagging	Grants permission to return the tag set of an object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectTorrent	Grants permission to return torrent files from an Amazon S3 bucket	Read	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectVersion	Grants permission to retrieve a specific version of an object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

GetObjectVersionAcl	Grants permission to return the access control list (ACL) of a specific object version	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

GetObjectVersionAttributes	Grants permission to retrieve attributes related to a specific version of an object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

GetObjectVersionForReplication	Grants permission to replicate both unencrypted objects and objects encrypted with SSE-S3 or SSE-KMS	Read	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetObjectVersionTagging	Grants permission to return the tag set for a specific version of the object	Read	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

GetObjectVersionTorrent	Grants permission to get Torrent files about a different version using the versionId subresource	Read	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

GetReplicationConfiguration	Grants permission to get the replication configuration information set on an Amazon S3 bucket	Read	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetStorageLensConfiguration	Grants permission to get an Amazon S3 Storage Lens configuration	Read	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetStorageLensConfigurationTagging	Grants permission to get the tag set of an existing Amazon S3 Storage Lens configuration	Read	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

GetStorageLensDashboard	Grants permission to get an Amazon S3 Storage Lens dashboard	Read	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

InitiateReplication	Grants permission to initiate the replication process by setting replication status of an object to pending	Write	
object*

s3:ResourceAccount

ListAccessPoints	Grants permission to list access points	List		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListAccessPointsForObjectLambda	Grants permission to list object lambda enabled accesspoints	List		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListAllMyBuckets	Grants permission to list all buckets owned by the authenticated sender of the request	List		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListBucket	Grants permission to list some or all of the objects in an Amazon S3 bucket (up to 1000)	List	
bucket*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:delimiter

s3:max-keys

s3:prefix

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListBucketMultipartUploads	Grants permission to list in-progress multipart uploads	List	
bucket*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListBucketVersions	Grants permission to list metadata about all the versions of objects in an Amazon S3 bucket	List	
bucket*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:delimiter

s3:max-keys

s3:prefix

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListJobs	Grants permission to list current jobs and jobs that have ended recently	List		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListMultiRegionAccessPoints	Grants permission to list Multi-Region Access Points	List		
s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

ListMultipartUploadParts	Grants permission to list the parts that have been uploaded for a specific multipart upload	List	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ListStorageLensConfigurations	Grants permission to list Amazon S3 Storage Lens configurations	List		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ObjectOwnerOverrideToBucketOwner	Grants permission to change replica ownership	Permissions management	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAccelerateConfiguration	Grants permission to use the accelerate subresource to set the Transfer Acceleration state of an existing S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAccessPointConfigurationForObjectLambda	Grants permission to set the configuration of the object lambda enabled access point	Write	
objectlambdaaccesspoint*

s3:DataAccessPointArn

s3:DataAccessPointAccount

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAccessPointPolicy	Grants permission to associate an access policy with a specified access point	Permissions management	
accesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAccessPointPolicyForObjectLambda	Grants permission to associate an access policy with a specified object lambda enabled access point	Permissions management	
objectlambdaaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAccessPointPublicAccessBlock	Grants permission to associate public access block configurations with a specified access point, while creating a access point	Permissions management			
PutAccountPublicAccessBlock	Grants permission to create or modify the PublicAccessBlock configuration for an AWS account	Permissions management		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutAnalyticsConfiguration	Grants permission to set an analytics configuration for the bucket, specified by the analytics configuration ID	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketAcl	Grants permission to set the permissions on an existing bucket using access control lists (ACLs)	Permissions management	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

PutBucketCORS	Grants permission to set the CORS configuration for an Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketLogging	Grants permission to set the logging parameters for an Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketNotification	Grants permission to receive notifications when certain events happen in an Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketObjectLockConfiguration	Grants permission to put Object Lock configuration on a specific bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:TlsVersion

s3:signatureversion

PutBucketOwnershipControls	Grants permission to add, replace or delete ownership controls on a bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketPolicy	Grants permission to add or replace a bucket policy on a bucket	Permissions management	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketPublicAccessBlock	Grants permission to create or modify the PublicAccessBlock configuration for a specific Amazon S3 bucket	Permissions management	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketRequestPayment	Grants permission to set the request payment configuration of a bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketTagging	Grants permission to add a set of tags to an existing Amazon S3 bucket	Tagging	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketVersioning	Grants permission to set the versioning state of an existing Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutBucketWebsite	Grants permission to set the configuration of the website that is specified in the website subresource	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutEncryptionConfiguration	Grants permission to set the encryption configuration for an Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutIntelligentTieringConfiguration	Grants permission to create new or update or delete an existing Amazon S3 Intelligent Tiering configuration	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutInventoryConfiguration	Grants permission to add an inventory configuration to the bucket, identified by the inventory ID	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutJobTagging	Grants permission to replace tags on an existing Amazon S3 Batch Operations job	Tagging	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:ExistingJobPriority

s3:ExistingJobOperation

aws:TagKeys

aws:RequestTag/${TagKey}

PutLifecycleConfiguration	Grants permission to create a new lifecycle configuration for the bucket or replace an existing lifecycle configuration	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutMetricsConfiguration	Grants permission to set or update a metrics configuration for the CloudWatch request metrics from an Amazon S3 bucket	Write	
bucket*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutMultiRegionAccessPointPolicy	Grants permission to associate an access policy with a specified Multi-Region Access Point	Permissions management	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

PutObject	Grants permission to add an object to a bucket	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:RequestObjectTag/<key>

s3:RequestObjectTagKeys

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-copy-source

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

s3:x-amz-metadata-directive

s3:x-amz-server-side-encryption

s3:x-amz-server-side-encryption-aws-kms-key-id

s3:x-amz-server-side-encryption-customer-algorithm

s3:x-amz-storage-class

s3:x-amz-website-redirect-location

s3:object-lock-mode

s3:object-lock-retain-until-date

s3:object-lock-remaining-retention-days

s3:object-lock-legal-hold

PutObjectAcl	Grants permission to set the access control list (ACL) permissions for new or existing objects in an S3 bucket	Permissions management	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

s3:x-amz-storage-class

PutObjectLegalHold	Grants permission to apply a Legal Hold configuration to the specified object	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:object-lock-legal-hold

PutObjectRetention	Grants permission to place an Object Retention configuration on an object	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:object-lock-mode

s3:object-lock-retain-until-date

s3:object-lock-remaining-retention-days

PutObjectTagging	Grants permission to set the supplied tag-set to an object that already exists in a bucket	Tagging	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:RequestObjectTag/<key>

s3:RequestObjectTagKeys

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutObjectVersionAcl	Grants permission to use the acl subresource to set the access control list (ACL) permissions for an object that already exists in a bucket	Permissions management	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-acl

s3:x-amz-content-sha256

s3:x-amz-grant-full-control

s3:x-amz-grant-read

s3:x-amz-grant-read-acp

s3:x-amz-grant-write

s3:x-amz-grant-write-acp

s3:x-amz-storage-class

PutObjectVersionTagging	Grants permission to set the supplied tag-set for a specific version of an object	Tagging	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:ExistingObjectTag/<key>

s3:RequestObjectTag/<key>

s3:RequestObjectTagKeys

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:versionid

s3:x-amz-content-sha256

PutReplicationConfiguration	Grants permission to create a new replication configuration or replace an existing one	Write	
bucket*

iam:PassRole

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

PutStorageLensConfiguration	Grants permission to create or update an Amazon S3 Storage Lens configuration	Write		
s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

aws:TagKeys

aws:RequestTag/${TagKey}

PutStorageLensConfigurationTagging	Grants permission to put or replace tags on an existing Amazon S3 Storage Lens configuration	Tagging	
storagelensconfiguration*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

aws:TagKeys

aws:RequestTag/${TagKey}

ReplicateDelete	Grants permission to replicate delete markers to the destination bucket	Write	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

ReplicateObject	Grants permission to replicate objects and object tags to the destination bucket	Write	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:x-amz-server-side-encryption

s3:x-amz-server-side-encryption-aws-kms-key-id

s3:x-amz-server-side-encryption-customer-algorithm

ReplicateTags	Grants permission to replicate object tags to the destination bucket	Tagging	
object*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

RestoreObject	Grants permission to restore an archived copy of an object back into Amazon S3	Write	
object*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

SubmitMultiRegionAccessPointRoutes	Grants permission to submit a route configuration update for a Multi-Region Access Point	Write	
multiregionaccesspoint*

s3:DataAccessPointAccount

s3:DataAccessPointArn

s3:AccessPointNetworkOrigin

s3:authType

s3:ResourceAccount

s3:signatureversion

s3:signatureAge

s3:TlsVersion

UpdateJobPriority	Grants permission to update the priority of an existing job	Write	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:RequestJobPriority

s3:ExistingJobPriority

s3:ExistingJobOperation

UpdateJobStatus	Grants permission to update the status for the specified job	Write	
job*

s3:authType

s3:ResourceAccount

s3:signatureAge

s3:signatureversion

s3:TlsVersion

s3:x-amz-content-sha256

s3:ExistingJobPriority

s3:ExistingJobOperation

s3:JobSuspendedCause

