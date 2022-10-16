# /bin/bash

# (c) Copyright 2022 Warwick Molloy, Kondensor
# Published under GPLv3. See license.md

numargs=$#
program=$0
account=$1

function isValidArgs() {
    ok=true
    if [ "X$account" == "X" ]; then
        echo "Account ID missing"
        ok=false
    fi
    if [ "X$region" == "X" ]; then
        echo "Region id (e.g. ap-southeast-1) missing"
        ok=false
    fi

    test $ok == "false" && echo "--------"
    `${ok}`
}

function showHelp() {
    echo "${program} of Kondensor"
    echo "This shell script configures an AWS CDK environment in a given sub account and region."
    echo ""
    echo "Usage ${program} <account-id> <region-id>"
}

function doJob() {
    echo "cdk bootstrap \
        --trust ${admin} \
        --cloudformation-execution-policies ${policy} \
        aws://${account}/${region}"

    cdk bootstrap aws://${account}/${region}  
}


if [ "X$account" == "X--help" ] || ! isValidArgs
then
    showHelp
else
    doJob
fi


