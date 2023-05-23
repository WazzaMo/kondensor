/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.IO;

namespace test;


public static class PipeValues
{
    /// <summary>Subset of HTML</summary>
    /// 
    public static StringReader HTML => new StringReader(
@"<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-US""><head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
        <title>Actions, resources, and condition keys for AWS Account Management - Service Authorization Reference</title>
        <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />
        <meta name=""assets_root"" content=""/assets"" />
        <meta name=""target_state"" content=""list_awsaccountmanagement"" />
        <meta name=""default_state"" content=""list_awsaccountmanagement"" /><link rel=""icon"" type=""image/ico"" href=""/assets/images/favicon.ico"" /><link rel=""shortcut icon"" type=""image/ico"" href=""/assets/images/favicon.ico"" /><link rel=""canonical"" href=""https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsaccountmanagement.html"" /><meta name=""description"" content=""Lists all of the available service-specific resources, actions, and condition keys that can be used in IAM policies to control access to AWS Account Management."" /><meta name=""deployment_region"" content=""IAD"" /><meta name=""product"" content=""Service Authorization Reference"" /><meta name=""guide"" content=""Service Authorization Reference"" /><meta name=""abstract"" content=""Provides a list of the actions, resources, and condition keys supported by each AWS service that can be used in an IAM policy."" /><meta name=""guide-locale"" content=""en_us"" /><meta name=""tocs"" content=""toc-contents.json"" /><link rel=""canonical"" href=""https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsaccountmanagement.html"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/id_id/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""id-id"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/id_id/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""id"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/de_de/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""de-de"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/de_de/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""de"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""en-us"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""en"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/es_es/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""es-es"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/es_es/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""es"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/fr_fr/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""fr-fr"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/fr_fr/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""fr"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/it_it/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""it-it"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/it_it/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""it"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/ja_jp/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""ja-jp"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/ja_jp/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""ja"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/ko_kr/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""ko-kr"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/ko_kr/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""ko"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/pt_br/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""pt-br"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/pt_br/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""pt"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/zh_cn/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""zh-cn"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/zh_tw/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""zh-tw"" /><link rel=""alternative"" href=""https://docs.aws.amazon.com/service-authorization/latest/reference/list_awsaccountmanagement.html"" hreflang=""x-default"" /><meta name=""feedback-item"" content=""Service Authorization Docs"" /><meta name=""this_doc_product"" content=""Service Authorization Reference"" /><meta name=""this_doc_guide"" content=""Service Authorization Reference"" /><script defer="""" src=""/assets/r/vendor4.js?version=2021.12.02""></script><script defer="""" src=""/assets/r/vendor3.js?version=2021.12.02""></script><script defer="""" src=""/assets/r/vendor1.js?version=2021.12.02""></script><script defer="""" src=""/assets/r/awsdocs-common.js?version=2021.12.02""></script><script defer="""" src=""/assets/r/awsdocs-doc-page.js?version=2021.12.02""></script><link href=""/assets/r/vendor4.css?version=2021.12.02"" rel=""stylesheet"" /><link href=""/assets/r/awsdocs-common.css?version=2021.12.02"" rel=""stylesheet"" /><link href=""/assets/r/awsdocs-doc-page.css?version=2021.12.02"" rel=""stylesheet"" /><script async="""" id=""awsc-panorama-bundle"" type=""text/javascript"" src=""https://prod.pa.cdn.uis.awsstatic.com/panorama-nav-init.js"" data-config=""{'appEntity':'aws-documentation','region':'us-east-1','service':'iam'}""></script><meta id=""panorama-serviceSubSection"" value=""Service Authorization Reference"" /><meta id=""panorama-serviceConsolePage"" value=""Actions, resources, and condition keys for AWS Account Management"" /></head><body class=""awsdocs awsui""><div class=""awsdocs-container""><awsdocs-header></awsdocs-header><awsui-app-layout id=""app-layout"" class=""awsui-util-no-gutters"" ng-controller=""ContentController as $ctrl"" header-selector=""awsdocs-header"" navigation-hide=""false"" navigation-width=""$ctrl.navWidth"" navigation-open=""$ctrl.navOpen"" navigation-change=""$ctrl.onNavChange($event)"" tools-hide=""$ctrl.hideTools"" tools-width=""$ctrl.toolsWidth"" tools-open=""$ctrl.toolsOpen"" tools-change=""$ctrl.onToolsChange($event)""><div id=""guide-toc"" dom-region=""navigation""><awsdocs-toc></awsdocs-toc></div><div id=""main-column"" dom-region=""content"" tabindex=""-1""><awsdocs-view class=""awsdocs-view""><div id=""awsdocs-content""><head><title>Actions, resources, and condition keys for AWS Account Management - Service Authorization Reference</title><meta name=""pdf"" content=""/pdfs/service-authorization/latest/reference/service-authorization.pdf#list_awsaccountmanagement"" /><meta name=""forums"" content=""http://forums.aws.amazon.com/forum.jspa?forumID=76"" /><meta name=""feedback"" content=""https://docs.aws.amazon.com/forms/aws-doc-feedback?hidden_service_name=Service%20Authorization%20Docs&amp;topic_url=http://docs.aws.amazon.com/en_us/service-authorization/latest/reference/list_awsaccountmanagement.html"" /><meta name=""feedback-yes"" content=""feedbackyes.html?topic_url=http://docs.aws.amazon.com/en_us/service-authorization/latest/reference/list_awsaccountmanagement.html"" /><meta name=""feedback-no"" content=""feedbackno.html?topic_url=http://docs.aws.amazon.com/en_us/service-authorization/latest/reference/list_awsaccountmanagement.html"" /><script type=""application/ld+json"">
{
    ""@context"" : ""https://schema.org"",
    ""@type"" : ""BreadcrumbList"",
    ""itemListElement"" : [
      {
        ""@type"" : ""ListItem"",
        ""position"" : 1,
        ""name"" : ""AWS"",
        ""item"" : ""https://aws.amazon.com""
      },
      {
        ""@type"" : ""ListItem"",
        ""position"" : 2,
        ""name"" : ""Service Authorization Reference"",
        ""item"" : ""https://docs.aws.amazon.com/iam/index.html""
      },
      {
        ""@type"" : ""ListItem"",
        ""position"" : 3,
        ""name"" : ""Service Authorization Reference"",
        ""item"" : ""https://docs.aws.amazon.com/service-authorization/latest/reference""
      },
      {
        ""@type"" : ""ListItem"",
        ""position"" : 4,
        ""name"" : ""Reference"",
        ""item"" : ""https://docs.aws.amazon.com/service-authorization/latest/reference/reference.html""
      },
      {
        ""@type"" : ""ListItem"",
        ""position"" : 5,
        ""name"" : ""Actions, resources, and condition keys for AWS services"",
        ""item"" : ""https://docs.aws.amazon.com/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html""
      },
      {
        ""@type"" : ""ListItem"",
        ""position"" : 6,
        ""name"" : ""Actions, resources, and condition keys for AWS Account Management"",
        ""item"" : ""https://docs.aws.amazon.com/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html""
      }
    ]
}
</script></head><body><div id=""main""><div style=""display: none""><a href=""/pdfs/service-authorization/latest/reference/service-authorization.pdf#list_awsaccountmanagement"" target=""_blank"" rel=""noopener noreferrer"" title=""Open PDF""></a></div><div id=""breadcrumbs"" class=""breadcrumb""><a href=""https://aws.amazon.com"">AWS</a><a href=""/index.html"">Documentation</a><a href=""/iam/index.html"">Service Authorization Reference</a><a href=""reference.html"">Service Authorization Reference</a></div><div id=""page-toc-src""><a href=""#awsaccountmanagement-actions-as-permissions"">Actions</a><a href=""#awsaccountmanagement-resources-for-iam-policies"">Resource types</a><a href=""#awsaccountmanagement-policy-keys"">Condition keys</a></div><div id=""main-content"" class=""awsui-util-container""><div id=""main-col-body""><awsdocs-language-banner data-service=""$ctrl.pageService""></awsdocs-language-banner><h1 class=""topictitle"" id=""list_awsaccountmanagement"">Actions, resources, and condition keys for      AWS Account Management</h1><div class=""awsdocs-page-header-container""><awsdocs-page-header></awsdocs-page-header><awsdocs-filter-selector id=""awsdocs-filter-selector""></awsdocs-filter-selector></div><p>AWS Account Management (service prefix: <code class=""code"">account</code>) provides the following    service-specific resources, actions, and condition context keys for use in IAM permission    policies.</p><p>References:</p><div class=""itemizedlist"">
         
         
         
    <ul class=""itemizedlist""><li class=""listitem"">
            <p>Learn how to <a href=""https://docs.aws.amazon.com/accounts/latest/reference/accounts-welcome.html"">configure this service</a>.</p>
        </li><li class=""listitem"">
            <p>View a list of the <a href=""https://docs.aws.amazon.com/accounts/latest/reference/api-reference.html"">API operations available for this          service</a>.</p>
        </li><li class=""listitem"">

            <p>Learn how to secure this service and its resources by <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security-iam.html"">using          IAM</a> permission policies.</p>
        </li></ul></div><div class=""highlights"" id=""inline-topiclist""><h6>Topics</h6><ul><li><a href=""#awsaccountmanagement-actions-as-permissions"">Actions defined by        AWS Account Management</a></li><li><a href=""#awsaccountmanagement-resources-for-iam-policies"">Resource types defined by        AWS Account Management</a></li><li><a href=""#awsaccountmanagement-policy-keys"">Condition keys for        AWS Account Management</a></li></ul></div>
        <h2 id=""awsaccountmanagement-actions-as-permissions"">Actions defined by        AWS Account Management</h2>
        <p>You can specify the following actions in the <code class=""code"">Action</code> element of an IAM      policy statement. Use policies to grant permissions to perform an operation in AWS. When you      use an action in a policy, you usually allow or deny access to the API operation or CLI      command with the same name. However, in some cases, a single action controls access to more      than one operation. Alternatively, some operations require several different actions.</p>
        <p>The <b>Resource types</b> column of the Actions table indicates whether each      action supports resource-level permissions. If there is no value for this column, you must      specify all resources (""*"") to which the policy applies in the <code class=""code"">Resource</code> element      of your policy statement. If the column includes a resource type, then you can specify an ARN      of that type in a statement with that action. If the action has one or more required      resources, the caller must have permission to use the action with those resources. Required      resources are indicated in the table with an asterisk (*). If you limit resource access with      the <code class=""code"">Resource</code> element in an IAM policy, you must include an ARN or pattern for      each required resource type. Some actions support multiple resource types. If the resource      type is optional (not indicated as required), then you can choose to use one of the optional      resource types.</p>
        <p>The <b>Condition keys</b> column of the Actions table includes keys that you      can specify in a policy statement's <code class=""code"">Condition</code> element. For more information on      the condition keys that are associated with resources for the service, see the        <b>Condition keys</b> column of the Resource types table.</p>

        <p>For details about the columns in the following table, see <a href=""reference_policies_actions-resources-contextkeys.html#actions_table"" rel=""noopener noreferrer"" target=""_blank"">Actions table</a>.</p>
        <div class=""table-container"">
        <div class=""table-contents disable-scroll""><table id=""w43aab5b9c19c11c11""><thead>
                    <tr>
                        <th>Actions</th>
                        <th>Description</th>
                        <th>Access level</th>
                        <th>Resource types (*required)</th>
                        <th>Condition keys</th>
                        <th>Dependent actions</th>
                    </tr>
                </thead>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-CloseAccount""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html"">CloseAccount</a> [permission only]</td>
                        <td>Grants permission to close an account</td>
                        <td>Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-DeleteAlternateContact""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_DeleteAlternateContact.html"">DeleteAlternateContact</a>
                        </td>
                        <td rowspan=""3"">Grants permission to delete the alternate contacts for an account</td>
                        <td rowspan=""3"">Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_AlternateContactTypes"">account:AlternateContactTypes</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-DisableRegion""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_DisableRegion.html"">DisableRegion</a>
                        </td>
                        <td rowspan=""3"">Grants permission to disable use of a Region</td>
                        <td rowspan=""3"">Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_TargetRegion"">account:TargetRegion</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-EnableRegion""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_EnableRegion.html"">EnableRegion</a>
                        </td>
                        <td rowspan=""3"">Grants permission to enable use of a Region</td>
                        <td rowspan=""3"">Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_TargetRegion"">account:TargetRegion</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-GetAccountInformation""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html"">GetAccountInformation</a> [permission only]</td>
                        <td>Grants permission to retrieve the account information for an account</td>
                        <td>Read</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-GetAlternateContact""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_GetAlternateContact.html"">GetAlternateContact</a>
                        </td>
                        <td rowspan=""3"">Grants permission to retrieve the alternate contacts for an account</td>
                        <td rowspan=""3"">Read</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_AlternateContactTypes"">account:AlternateContactTypes</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-GetChallengeQuestions""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html"">GetChallengeQuestions</a> [permission only]</td>
                        <td>Grants permission to retrieve the challenge questions for an account</td>
                        <td>Read</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""2"">
                            <a id=""awsaccountmanagement-GetContactInformation""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_GetContactInformation.html"">GetContactInformation</a>
                        </td>
                        <td rowspan=""2"">Grants permission to retrieve the primary contact information for an account</td>
                        <td rowspan=""2"">Read</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-GetRegionOptStatus""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_GetRegionOptStatus.html"">GetRegionOptStatus</a>
                        </td>
                        <td rowspan=""3"">Grants permission to get the opt-in status of a Region</td>
                        <td rowspan=""3"">Read</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_TargetRegion"">account:TargetRegion</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""2"">
                            <a id=""awsaccountmanagement-ListRegions""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_ListRegions.html"">ListRegions</a>
                        </td>
                        <td rowspan=""2"">Grants permission to list the available Regions</td>
                        <td rowspan=""2"">List</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-PutAlternateContact""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_PutAlternateContact.html"">PutAlternateContact</a>
                        </td>
                        <td rowspan=""3"">Grants permission to modify the alternate contacts for an account</td>
                        <td rowspan=""3"">Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account_AlternateContactTypes"">account:AlternateContactTypes</a>
                            </p>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-PutChallengeQuestions""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html"">PutChallengeQuestions</a> [permission only]</td>
                        <td>Grants permission to modify the challenge questions for an account</td>
                        <td>Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td rowspan=""2"">
                            <a id=""awsaccountmanagement-PutContactInformation""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_PutContactInformation.html"">PutContactInformation</a>
                        </td>
                        <td rowspan=""2"">Grants permission to update the primary contact information for an account</td>
                        <td rowspan=""2"">Write</td>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table></div></div>
                <div class=""table-container""><div class=""table-contents disable-scroll""><table id=""w43aab5b9c19c13b5""><thead>
                    <tr>
                        <th>Resource types</th>
                        <th>ARN</th>
                        <th>Condition keys</th>
                    </tr>
                </thead>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-account""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-resources"">account</a>
                        </td>
                        <td>
                            <code class=""code"">arn:$<span>{</span>Partition}:account::$<span>{</span>Account}:account</code>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-accountInOrganization""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-resources"">accountInOrganization</a>
                        </td>
                        <td>
                            <code class=""code"">arn:$<span>{</span>Partition}:account::$<span>{</span>ManagementAccountId}:account/o-$<span>{</span>OrganizationId}/$<span>{</span>MemberAccountId}</code>
                        </td>
                        <td></td>
                    </tr>
                </table></div></div>
                <div class=""table-container""><div class=""table-contents disable-scroll""><table id=""w43aab5b9c19c15b7""><thead>
                    <tr>
                        <th>Condition keys</th>
                        <th>Description</th>
                        <th>Type</th>
                    </tr>
                </thead>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-account_AccountResourceOrgPaths""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys"">account:AccountResourceOrgPaths</a>
                        </td>
                        <td>Filters access by the resource path for an account in an organization</td>
                        <td>ArrayOfString</td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-account_AccountResourceOrgTags___TagKey_""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys"">account:AccountResourceOrgTags/${TagKey}</a>
                        </td>
                        <td>Filters access by resource tags for an account in an organization</td>
                        <td>ArrayOfString</td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-account_AlternateContactTypes""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys"">account:AlternateContactTypes</a>
                        </td>
                        <td>Filters access by alternate contact types</td>
                        <td>ArrayOfString</td>
                    </tr>
                    <tr>
                        <td>
                            <a id=""awsaccountmanagement-account_TargetRegion""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_iam_service-with-iam.html#security_iam_service-with-iam-id-based-policies-conditionkeys"">account:TargetRegion</a>
                        </td>
                        <td>Filters access by a list of Regions. Enables or disables all the Regions specified here</td>
                        <td>String</td>
                    </tr>
                </table></div></div>
     
");  
}

