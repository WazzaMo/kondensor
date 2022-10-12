# 2. Getting Started

## AWS tools

### CDK Needs a working NodeJS

I recommend using NVM - Node Version Manager - to install
NodeJs in a local user directory.

[Go to this page to see how to install NVM](https://github.com/nvm-sh/nvm#installing-and-updating)

### AWS CLI v2

Best to install AWS CLI v2
can be installed by following
the [instructions here](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html#getting-started-install-instructions).

On x86 64 bit Linux, the steps are:
```sh
$ curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
$ unzip awscliv2.zip
$ sudo ./aws/install
```

## Setting up dotnet

On Ubuntu, use dotnet6 (or more recent) by running

`sudo apt install dotnet6`

Then install the AWS CDK library...

`dotnet add package Amazon.CDK.Lib`

## Getting ready to work in AWS

CDK needs to be [bootstrapped.](https://docs.aws.amazon.com/cdk/v2/guide/bootstrapping.html)

Create a bootstrap template using:

```sh
cdk bootstrap --show-template > bootstrap-template.yaml
```

And can then use AWS CLI to do the bootstrap.

```sh
aws cloudformation create-stack \
  --stack-name CDKToolkit \
  --template-body file://bootstrap-template.yaml
```
