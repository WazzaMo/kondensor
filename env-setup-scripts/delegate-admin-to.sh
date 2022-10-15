#/bin/bash

# (c) Copyright 2022 Warwick Molloy, Kondensor
# Published under GPLv3. See license.md

program=$0
accountid=$1
profile=$2
region=$3

aws servicecatalog enable-aws-organizations-access --profile ${profile} --region ${region} 

aws organizations register-delegated-administrator --account-id ${accountid} \
    --service-principal cloudformation --profile delegate --cli-connect-timeout 300 --region ${region} --debug

