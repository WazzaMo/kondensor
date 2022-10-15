# 5 Existing tools and services and their relevance

Some existing tools and AWS features can be compared to Kondensor
 because they create a high-level
way of thinking about cloud services and let's look at them because
some help Kondensor and some do not.

# Potential use in or with Kondensor

These services or tools align with Kondensor in that they can offer
patterns of cloud services for use and deployment.
This means there is potential to use these as a source of patterns
and, possibly, access controls.

## AWS Service Catalog

Service catalog offers hundreds of packaged services as cloudformation templates.
These can a be used as a collection of possible outcomes to use in an
overall design.

The catalog is organised as products. Product templates
can be added to the catalog and versioned.

The products can be deployed into portfolios and users are granted access
on a portfolio basis for use.

AWS has a [Getting Started library in Sydney region](https://ap-southeast-2.console.aws.amazon.com/servicecatalog/home?region=ap-southeast-2#getting-started-library)
 of products:
- .NET Core CI/CD (Elastic Beanstalk), source AWS Quick Starts
- .NET Framework CI/CD for Amazon ECS, source AWS Quick Starts
- AI Powered speech analytics for Amazon Connect, source AWS Solutions

# Irrelevant for Kondensor

The services or tools listed here are seen to be parallel, possibly complementary,
but not aligned with Kondensor. 

## Codestar

Codestar has some similarity to Kondensor but appears to be too limited
and does not help Kondensor achieve its goals.

[AWS Codestar](https://aws.amazon.com/codestar/features/)
 offers templates for limited compute services
and for a limited selection of programmings languages:

- languages: Java, JavaScript, Python, Ruby and PHP
- compute: EC2, Lambda and Elastic Beanstalk

## AWS CloudFormation designer

The CloudFormation designer could be used to design
templates for use in patterns, so it can be complementary.
It does not directly help Kondensor's mission other than
to possibly help pattern developers author new templates.

[AWS CloudFormation designer](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/working-with-templates-cfn-designer-why.html)
assists the user design and model AWS infrastructure
but this **assumes you know what infrastructure you want.**

The existing AWS tools of Codestar and CF Designer are helpful
but either limit the programming environment or make the user
think at a lower level of the infrastructure.
Kondensor is about the system needs.

