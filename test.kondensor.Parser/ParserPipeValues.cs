/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */

using System.IO;

namespace test.kondensor.Parser;


public static class ParserPipeValues
{
    public static StringReader REPEAT => new StringReader(
      @"<td>
            <p>
                <a href=""#awselementalmediapackagevod-packaging-groups"">packaging-groups*</a>
            </p>
        </td>
        <td></td>
        <td>
            <p>
                iam:CreateServiceLinkedRole
            </p>
        </td>"
    );

    public static StringReader TABLE_ONLY_PAGE => new StringReader(
        s: @"
<html>
  <table>
  <div>test text</div>
  </table>
</html>"
);

    public static StringReader PARA_ONLY_PAGE => new StringReader(
        @"
<html>
  <p>
    test text
  </p>
</html>"
);

    public static StringReader ONE_ROW_DATA => new StringReader
(
@"  <table id=""w43aab5b9c19c11c11"">
            <thead>
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
        </table>"
);

  public static StringReader ONE_ROW_DATA_WITH_ODD_ATTIBS => new StringReader
(
@"  <table id=""w43aab5b9c19c11c11"">
            <thead>
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
                <td tabindex=""1"">tabindex=1</td>
                <td borked=""aigoo"" rowspan=""1"">borked and rowspan=1</td>
                <td rowspan=""2"" tabindex=""3"">rowspan=2 and tabindex</td>
                <td>Empty</td>
                <td rowspan=""text"" later=""stuff"">rowspan=text and later</td>
                <td nonsense=""always"">nonsense</td>
            </tr>
        </table>"
);


  public static StringReader MULT_ROW_DATA => new StringReader
(
@"  <table id=""w43aab5b9c19c11c11"">
            <thead>
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
        </table>
" 
);

    public static StringReader MULT_ROW_DATA_WITH_REPEAT => new StringReader
(
@"  <table id=""w43aab5b9c19c11c11"">
            <thead>
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
        </table>
" 
);



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
                        <td>1
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
                        240</td>
                        241<td rowspan=""3"">Grants permission to delete the alternate contacts for an account</td>242
                        243<td rowspan=""3"">Write</td>244
                        245<td>2
                            246<p>
                                247<a href=""#awsaccountmanagement-account"">account</a>248
                            249</p>
                        250</td>
                        251<td></td>252
                        253<td></td>254
                    255</tr>
                    256<tr>
                        257<td>3
                            258<p>3
                                259<a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>260
                            261</p>
                        262</td>
                        263<td></td>264
                        265<td></td>266
                    267</tr>
                    268<tr>
                        269<td>4</td>270
                        271<td>
                            272<p>
                                273<a href=""#awsaccountmanagement-account_AlternateContactTypes"">account:AlternateContactTypes</a>274
                            275</p>
                        276</td>
                        277<td></td>278
                    279</tr>
                    280<tr>
                        281<td rowspan=""3"">
                            282<a id=""awsaccountmanagement-DisableRegion""></a>283
                            284<a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_DisableRegion.html"">DisableRegion</a>285
                        286</td>
                        287<td rowspan=""3"">Grants permission to disable use of a Region</td>288
                        289<td rowspan=""3"">Write</td>290
                        291<td>5
                            292<p>
                                293<a href=""#awsaccountmanagement-account"">account</a>294
                            295</p>
                        296</td>
                        297<td></td>298
                        299<td></td>300
                    301</tr>
                    302<tr>
                        303<td>6
                            304<p>
                                305<a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>306
                            307</p>
                        308</td>
                        309<td></td>310
                        311<td></td>312
                    313</tr>
                    314<tr>
                        315<td>7</td>316
                        317<td>
                            318<p>
                                319<a href=""#awsaccountmanagement-account_TargetRegion"">account:TargetRegion</a>320
                            321</p>
                        322</td>
                        323<td></td>324
                    325</tr>
                    326<tr>
                        327<td rowspan=""3"">
                            328<a id=""awsaccountmanagement-EnableRegion""></a>329
                            330<a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_EnableRegion.html"">EnableRegion</a>331
                        332</td>
                        333<td rowspan=""3"">Grants permission to enable use of a Region</td>334
                        335<td rowspan=""3"">Write</td>336
                        337<td>8
                            338<p>
                                339<a href=""#awsaccountmanagement-account"">account</a>340
                            341</p>
                        342</td>
                        343<td></td>344
                        345<td></td>346
                    347</tr>
                    348<tr>
                        349<td>9
                            350<p>
                                351<a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>352
                            353</p>
                        354</td>
                        355<td></td>356
                        357<td></td>358
                    359</tr>
                    360<tr>
                        361<td>10</td>362
                        363<td>
                            364<p>
                                365<a href=""#awsaccountmanagement-account_TargetRegion"">account:TargetRegion</a>366
                            367</p>
                        368</td>
                        369<td></td>370
                    371</tr>
                    372<tr>
                        373<td>
                            374<a id=""awsaccountmanagement-GetAccountInformation""></a>375
                            376<a href=""https://docs.aws.amazon.com/accounts/latest/reference/security_account-permissions-ref.html"">GetAccountInformation</a>377 [permission only]</td>378
                        379<td>Grants permission to retrieve the account information for an account</td>380
                        381<td>Read</td>382
                        383<td>11
                            384<p>
                                385<a href=""#awsaccountmanagement-account"">account</a>386
                            387</p>
                        388</td>
                        389<td></td>390
                        391<td></td>392
                    393</tr>
                    394<tr>
                        <td rowspan=""3"">
                            <a id=""awsaccountmanagement-GetAlternateContact""></a>
                            <a href=""https://docs.aws.amazon.com/accounts/latest/reference/API_GetAlternateContact.html"">GetAlternateContact</a>
                        </td>
                        <td rowspan=""3"">Grants permission to retrieve the alternate contacts for an account</td>
                        <td rowspan=""3"">Read</td>
                        <td>12
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>13
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>14</td>
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
                        <td>15
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
                        <td>16
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>17
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
                        <td>18
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>19
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>20
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
                        <td>21
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>22
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
                        <td>23
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>24
                            <p>
                                <a href=""#awsaccountmanagement-accountInOrganization"">accountInOrganization</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>25</td>
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
                        <td>26
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
                        <td>27
                            <p>
                                <a href=""#awsaccountmanagement-account"">account</a>
                            </p>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>28
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

