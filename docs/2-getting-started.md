# 2. Getting Started

## Steps - the plan

The instructions given here are for Ubuntu which should work
in Windows Subsystem for Linux Ubuntu or Ubuntu (native).

1. Install Node JS (to make CDK work)
2. Install AWS-CDK
3. Install AWS CLI v2
4. Install dotnet 6 or later

## Installing NodeJS

You can install NodeJS as you see fit but I will document what I like doing
as a suggestion. NodeJS releases can move quickly so using Node Version Manager, NVM,
is recommended and makes for a per-user install very easy in Linux, rather than installing
NodeJS centrally for all users.

### Install Node Version Mananger (nvm)

Using NVM - Node Version Manager - to install NodeJs in a local user directory.

[Go to this page to see how to install NVM](https://github.com/nvm-sh/nvm#installing-and-updating)

I had to install curl and then run the following command.

NOTE: this downloads and executes a BASH script and we all know that running a script
off the internet is a bit scary. It is executed under the permissions of a normal user,
so a wise idea is to create a dev user with no special privileges and run it under that user.

```sh
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.2/install.sh | bash
```

With NVM installed and .bashrc updated to add NVM to PATH, either:
1. restart BASH to get a terminal with the updated PATH; or
2. run `source ~/.bashrc` to update PATH in the current session.

### Install NodeJS

I installed NodeJS 16.17.1 (current LTS) using the following.
Best to use the major number (16 in this case) of the current LTS NodeJS version.

```bash
nvm use 16
```

Now, you should be able to confirm NodeJS is working....

```bash
node --version
```


## AWS tools

### AWS CDK

Use NPM (from NodeJS) to install CDK.
If your NodeJS was installed in a local user,
then you can install as below.

```bash
npm install -g aws-cdk
```

If, instead, your NodeJS was installed with root or administrative user
privileges, you may need root powers to install aws-cdk, as follows:

```bash
sudo npm install -g aws-cdk
```

This should make the `cdk` available to you and you can test this with:

```bash
cdk --version
```


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


# Antivirus software for Linux

In case you're worried about running code to install packages, anti-virus software is available
for Linux.

[ClamAV is a free, open source, CLI virus scanning tool - documentation here](https://www.clamav.net/downloads)