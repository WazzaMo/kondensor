/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

// using Spec;

using System;


// 'https://docs.aws.amazon.com/service-authorization/latest/reference/list_amazonapigateway.html'

// https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsapplicationdiscoveryservice.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_amazoncloudwatchapplicationinsights.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_amazoncomprehendmedical.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_awscomputeoptimizer.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsmarketplace.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_amazonmessagedeliveryservice.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsopsworksconfigurationmanagement.html
// https://docs.aws.amazon.com/service-authorization/latest/reference/list_amazonsessionmanagermessagegatewayservice.html

const string
  PROBLEM_PAGE = "list_awsapplicationdiscoveryservice.html";
PageProcessingControl.ProcessSpecificPage(PROBLEM_PAGE, Console.Out);

// return PageProcessingControl.IterateThroughAllPagesAvailable();

