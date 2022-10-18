# AWS accounts

AWS allows you to create an organization of accounts, each with their
own infrastructure and billing costs, that can be managed by the root account.

The service responsible for managing this is AWS Organizations and this can be
seen in the Organiztions part of the AWS Management Console or controlled
using the AWS CLI with the `aws organizations ...` command.

## Terminology

### Root Account

This is the AWS account that manages and pays for all cloud
service costs.

### Member Account

A member account is an account within an organization of accounts
that is strongly partitioned from other accounts in the organization.

Member accounts can be group into OUs, Organizational Units.

## Managing Access

User access to member accounts can be controlled through IAM.
see [Organizations - Manage account access](https://docs.aws.amazon.com/organizations/latest/userguide/orgs_manage_accounts_access.html)

Default role name `OrganizationAccountAccessRole`

# References

[Accessing and administering member accounts](https://docs.aws.amazon.com/organizations/latest/userguide/orgs_manage_accounts_access.html)

[Switching roles to access member account](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_roles_use_switch-role-cli.html)

[Landing Zone and Control Tower](https://docs.aws.amazon.com/controltower/latest/userguide/customize-landing-zone.html)

