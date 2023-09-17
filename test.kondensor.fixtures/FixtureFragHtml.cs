/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Lesser Public License v 3.0
 */


using System.IO;

namespace test.kondensor.fixtures;

/// <summary>
/// Fixture providing HTML data a TextReader (StringReader) object for
/// test input for fragmented HTML handling test cases. The text has been
/// modified but the structure of the HTML is important for conducting
/// the tests.
/// </summary>
public static class FixtureFragHtml
{
  public static StringReader HtmlToFrag()
    => new StringReader(
s:@"
<table id=""actionFixture""><thead>
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
        <td rowspan=""2"" tabindex=""-1"" id=""amazonplaygroundmanagement-AddCertificateToPlayGround"">
              
            <a href=""https://docs.aws.amazon.com/playground/latest/api/PG_funtimes.html"">AddCertificateToPlayGround</a>
        </td>
        <td rowspan=""2"" tabindex=""-1"">Grants permission to add certificates for mutual TLS authentication to a domain name. This is an additional authorization control for managing the PlayGroundName resource due to the sensitive nature of mTLS</td>
        <td rowspan=""2"" tabindex=""-1"">Permissions management</td>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-PlayGroundName"">PlayGroundName</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1""><p><a href=""#amazonplaygroundmanagement-PlayGroundNames"">PlayGroundNames</a></p></td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td rowspan=""24"" tabindex=""-1"" id=""amazonplaygroundmanagement-DELETE"">
              
            <a href=""https://docs.aws.amazon.com/playground/latest/api/PG_funtimes.html"">DELETE</a>
        </td>
        <td rowspan=""24"" tabindex=""-1"">Grants permission to delete a particular resource</td>
        <td rowspan=""24"" tabindex=""-1"">Write</td>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-ApiKey"">ApiKey</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Authorizer"">Authorizer</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-BasePathMapping"">BasePathMapping</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-ClientCertificate"">ClientCertificate</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Deployment"">Deployment</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-DocumentationPart"">DocumentationPart</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-DocumentationVersion"">DocumentationVersion</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-PlayGroundName"">PlayGroundName</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-GatewayResponse"">GatewayResponse</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Integration"">Integration</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-IntegrationResponse"">IntegrationResponse</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Method"">Method</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-MethodResponse"">MethodResponse</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Model"">Model</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-RequestValidator"">RequestValidator</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Resource"">Resource</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-RestApi"">RestApi</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Stage"">Stage</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Tags"">Tags</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-Template"">Template</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-UsagePlan"">UsagePlan</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-UsagePlanKey"">UsagePlanKey</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-VpcLink"">VpcLink</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
        <td tabindex=""-1""></td>
    </tr>
    <tr>
        <td tabindex=""-1""></td>
        <td tabindex=""-1"">
            <p>
                <a href=""#amazonplaygroundmanagement-aws_RequestTag___TagKey_"">aws:RequestTag/${TagKey}</a>
            </p>
            <p>
                <a href=""#amazonplaygroundmanagement-aws_TagKeys"">aws:TagKeys</a>
            </p>
        </td>
        <td tabindex=""-1""></td>
    </tr>
  </table>
<!-- 
  Resources section
  -->
<table id=""resourceFixture""><thead>
      <tr>
          <th>Resource types</th>
          <th>ARN</th>
          <th>Condition keys</th>
      </tr>
  </thead>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Account"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">Account</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/account</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-ApiKey"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_ApiKey.html"">ApiKey</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/apikeys/$<span>{</span>ApiKeyId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-ApiKeys"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_ApiKey.html"">ApiKeys</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/apikeys</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Authorizer"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Authorizer.html"">Authorizer</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/authorizers/$<span>{</span>AuthorizerId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerType"">playground:Request/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerUri"">playground:Request/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AuthorizerType"">playground:Resource/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AuthorizerUri"">playground:Resource/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Authorizers"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Authorizer.html"">Authorizers</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/authorizers</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerType"">playground:Request/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerUri"">playground:Request/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-BasePathMapping"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_BasePathMapping.html"">BasePathMapping</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/domainnames/$<span>{</span>PlayGroundName}/basepathmappings/$<span>{</span>BasePath}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-BasePathMappings"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_BasePathMapping.html"">BasePathMappings</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/domainnames/$<span>{</span>PlayGroundName}/basepathmappings</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-ClientCertificate"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_ClientCertificate.html"">ClientCertificate</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/clientcertificates/$<span>{</span>ClientCertificateId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-ClientCertificates"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_ClientCertificate.html"">ClientCertificates</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/clientcertificates</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Deployment"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Deployment.html"">Deployment</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/deployments/$<span>{</span>DeploymentId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Deployments"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Deployment.html"">Deployments</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/deployments</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_StageName"">playground:Request/StageName</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-DocumentationPart"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_DocumentationPart.html"">DocumentationPart</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/documentation/parts/$<span>{</span>DocumentationPartId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-DocumentationParts"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_DocumentationPart.html"">DocumentationParts</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/documentation/parts</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-DocumentationVersion"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_DocumentationVersion.html"">DocumentationVersion</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/documentation/versions/$<span>{</span>DocumentationVersionId}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-DocumentationVersions"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_DocumentationVersion.html"">DocumentationVersions</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/documentation/versions</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-PlayGroundName"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_PlayGroundName.html"">PlayGroundName</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/domainnames/$<span>{</span>PlayGroundName}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_EndpointType"">playground:Request/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_MtlsTrustStoreUri"">playground:Request/MtlsTrustStoreUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_MtlsTrustStoreVersion"">playground:Request/MtlsTrustStoreVersion</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_SecurityPolicy"">playground:Request/SecurityPolicy</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_EndpointType"">playground:Resource/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_MtlsTrustStoreUri"">playground:Resource/MtlsTrustStoreUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_MtlsTrustStoreVersion"">playground:Resource/MtlsTrustStoreVersion</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_SecurityPolicy"">playground:Resource/SecurityPolicy</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-PlayGroundNames"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_PlayGroundName.html"">PlayGroundNames</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/domainnames</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_EndpointType"">playground:Request/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_MtlsTrustStoreUri"">playground:Request/MtlsTrustStoreUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_MtlsTrustStoreVersion"">playground:Request/MtlsTrustStoreVersion</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_SecurityPolicy"">playground:Request/SecurityPolicy</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-GatewayResponse"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_GatewayResponse.html"">GatewayResponse</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/gatewayresponses/$<span>{</span>ResponseType}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-GatewayResponses"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_GatewayResponse.html"">GatewayResponses</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/gatewayresponses</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Integration"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Integration.html"">Integration</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources/$<span>{</span>ResourceId}/methods/$<span>{</span>HttpMethodType}/integration</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-IntegrationResponse"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_IntegrationResponse.html"">IntegrationResponse</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources/$<span>{</span>ResourceId}/methods/$<span>{</span>HttpMethodType}/integration/responses/$<span>{</span>StatusCode}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Method"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Method.html"">Method</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources/$<span>{</span>ResourceId}/methods/$<span>{</span>HttpMethodType}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_ApiKeyRequired"">playground:Request/ApiKeyRequired</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_RouteAuthorizationType"">playground:Request/RouteAuthorizationType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_ApiKeyRequired"">playground:Resource/ApiKeyRequired</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_RouteAuthorizationType"">playground:Resource/RouteAuthorizationType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-MethodResponse"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_MethodResponse.html"">MethodResponse</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources/$<span>{</span>ResourceId}/methods/$<span>{</span>HttpMethodType}/responses/$<span>{</span>StatusCode}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Model"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Model.html"">Model</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/models/$<span>{</span>ModelName}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Models"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Model.html"">Models</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/models</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-RequestValidator"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_RequestValidator.html"">RequestValidator</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/requestvalidators/$<span>{</span>RequestValidatorId}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-RequestValidators"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_RequestValidator.html"">RequestValidators</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/requestvalidators</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Resource"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Resource.html"">Resource</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources/$<span>{</span>ResourceId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Resources"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Resource.html"">Resources</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/resources</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-RestApi"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_RestApi.html"">RestApi</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_ApiKeyRequired"">playground:Request/ApiKeyRequired</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_ApiName"">playground:Request/ApiName</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerType"">playground:Request/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerUri"">playground:Request/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_DisableExecuteApiEndpoint"">playground:Request/DisableExecuteApiEndpoint</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_EndpointType"">playground:Request/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_RouteAuthorizationType"">playground:Request/RouteAuthorizationType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_ApiKeyRequired"">playground:Resource/ApiKeyRequired</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_ApiName"">playground:Resource/ApiName</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AuthorizerType"">playground:Resource/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AuthorizerUri"">playground:Resource/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_DisableExecuteApiEndpoint"">playground:Resource/DisableExecuteApiEndpoint</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_EndpointType"">playground:Resource/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_RouteAuthorizationType"">playground:Resource/RouteAuthorizationType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-RestApis"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_RestApi.html"">RestApis</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_ApiKeyRequired"">playground:Request/ApiKeyRequired</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_ApiName"">playground:Request/ApiName</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerType"">playground:Request/AuthorizerType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AuthorizerUri"">playground:Request/AuthorizerUri</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_DisableExecuteApiEndpoint"">playground:Request/DisableExecuteApiEndpoint</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_EndpointType"">playground:Request/EndpointType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_RouteAuthorizationType"">playground:Request/RouteAuthorizationType</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Sdk"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">Sdk</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/stages/$<span>{</span>StageName}/sdks/$<span>{</span>SdkType}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Stage"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Stage.html"">Stage</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/stages/$<span>{</span>StageName}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AccessLoggingDestination"">playground:Request/AccessLoggingDestination</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AccessLoggingFormat"">playground:Request/AccessLoggingFormat</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AccessLoggingDestination"">playground:Resource/AccessLoggingDestination</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Resource_AccessLoggingFormat"">playground:Resource/AccessLoggingFormat</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Stages"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_Stage.html"">Stages</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/$<span>{</span>RestApiId}/stages</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AccessLoggingDestination"">playground:Request/AccessLoggingDestination</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-playground_Request_AccessLoggingFormat"">playground:Request/AccessLoggingFormat</a>
              </p>
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Template"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">Template</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/restapis/models/$<span>{</span>ModelName}/template</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-UsagePlan"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_UsagePlan.html"">UsagePlan</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/usageplans/$<span>{</span>UsagePlanId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-UsagePlans"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_UsagePlan.html"">UsagePlans</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/usageplans</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-UsagePlanKey"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_UsagePlanKey.html"">UsagePlanKey</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/usageplans/$<span>{</span>UsagePlanId}/keys/$<span>{</span>Id}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-UsagePlanKeys"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_UsagePlanKey.html"">UsagePlanKeys</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/usageplans/$<span>{</span>UsagePlanId}/keys</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-VpcLink"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_VpcLink.html"">VpcLink</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/vpclinks/$<span>{</span>VpcLinkId}</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-VpcLinks"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/api/API_VpcLink.html"">VpcLinks</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/vpclinks</code>
          </td>
          <td tabindex=""-1"">
              <p>
                  <a href=""#amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">aws:ResourceTag/${TagKey}</a>
              </p>
          </td>
      </tr>
      <tr>
          <td tabindex=""-1"" id=""amazonplaygroundmanagement-Tags"">
                
              <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/playground-tagging.html"">Tags</a>
          </td>
          <td tabindex=""-1"">
              <code class=""code"">arn:$<span>{</span>Partition}:playground:$<span>{</span>Region}::/tags/$<span>{</span>UrlEncodedResourceARN}</code>
          </td>
          <td tabindex=""-1""></td>
      </tr>
  </table>

<!--
  Condition keys table.
  -->

<table id=""conditionsFixture""><thead>
        <tr>
            <th>Condition keys</th>
            <th>Description</th>
            <th>Type</th>
        </tr>
    </thead>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_AccessLoggingDestination"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/AccessLoggingDestination</a>
            </td>
            <td tabindex=""-1"">Filters access by access log destination. Available during the CreateStage and UpdateStage operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_AccessLoggingFormat"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/AccessLoggingFormat</a>
            </td>
            <td tabindex=""-1"">Filters access by access log format. Available during the CreateStage and UpdateStage operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_ApiKeyRequired"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/ApiKeyRequired</a>
            </td>
            <td tabindex=""-1"">Filters access by whether an API key is required or not. Available during the CreateMethod and PutMethod operations. Also available as a collection during import and reimport</td>
            <td tabindex=""-1"">ArrayOfBool</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_ApiName"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/ApiName</a>
            </td>
            <td tabindex=""-1"">Filters access by API name. Available during the CreateRestApi and UpdateRestApi operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_AuthorizerType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/AuthorizerType</a>
            </td>
            <td tabindex=""-1"">Filters access by type of authorizer in the request, for example TOKEN, REQUEST, JWT. Available during CreateAuthorizer and UpdateAuthorizer. Also available during import and reimport as an ArrayOfString</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_AuthorizerUri"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/AuthorizerUri</a>
            </td>
            <td tabindex=""-1"">Filters access by URI of a Lambda authorizer function. Available during CreateAuthorizer and UpdateAuthorizer. Also available during import and reimport as an ArrayOfString</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_DisableExecuteApiEndpoint"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/DisableExecuteApiEndpoint</a>
            </td>
            <td tabindex=""-1"">Filters access by status of the default execute-api endpoint. Available during the CreateRestApi and DeleteRestApi operations</td>
            <td tabindex=""-1"">Bool</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_EndpointType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/EndpointType</a>
            </td>
            <td tabindex=""-1"">Filters access by endpoint type. Available during the CreatePlayGroundName, UpdatePlayGroundName, CreateRestApi, and UpdateRestApi operations</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_MtlsTrustStoreUri"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/MtlsTrustStoreUri</a>
            </td>
            <td tabindex=""-1"">Filters access by URI of the truststore used for mutual TLS authentication. Available during the CreatePlayGroundName and UpdatePlayGroundName operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_MtlsTrustStoreVersion"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/MtlsTrustStoreVersion</a>
            </td>
            <td tabindex=""-1"">Filters access by version of the truststore used for mutual TLS authentication. Available during the CreatePlayGroundName and UpdatePlayGroundName operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_RouteAuthorizationType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/RouteAuthorizationType</a>
            </td>
            <td tabindex=""-1"">Filters access by authorization type, for example NONE, AWS_IAM, CUSTOM, JWT, COGNITO_USER_POOLS. Available during the CreateMethod and PutMethod operations Also available as a collection during import</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_SecurityPolicy"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/SecurityPolicy</a>
            </td>
            <td tabindex=""-1"">Filters access by TLS version. Available during the CreatePlayGround and UpdatePlayGround operations</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Request_StageName"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Request/StageName</a>
            </td>
            <td tabindex=""-1"">Filters access by stage name of the deployment that you attempt to create. Available during the CreateDeployment operation</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_AccessLoggingDestination"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/AccessLoggingDestination</a>
            </td>
            <td tabindex=""-1"">Filters access by access log destination of the current Stage resource. Available during the UpdateStage and DeleteStage operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_AccessLoggingFormat"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/AccessLoggingFormat</a>
            </td>
            <td tabindex=""-1"">Filters access by access log format of the current Stage resource. Available during the UpdateStage and DeleteStage operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_ApiKeyRequired"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/ApiKeyRequired</a>
            </td>
            <td tabindex=""-1"">Filters access by whether an API key is required or not for the existing Method resource. Available during the PutMethod and DeleteMethod operations. Also available as a collection during reimport</td>
            <td tabindex=""-1"">ArrayOfBool</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_ApiName"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/ApiName</a>
            </td>
            <td tabindex=""-1"">Filters access by API name of the existing RestApi resource. Available during UpdateRestApi and DeleteRestApi operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_AuthorizerType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/AuthorizerType</a>
            </td>
            <td tabindex=""-1"">Filters access by the current type of authorizer, for example TOKEN, REQUEST, JWT. Available during UpdateAuthorizer and DeleteAuthorizer operations. Also available during reimport as an ArrayOfString</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_AuthorizerUri"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/AuthorizerUri</a>
            </td>
            <td tabindex=""-1"">Filters access by URI of a Lambda authorizer function. Available during UpdateAuthorizer and DeleteAuthorizer operations. Also available during reimport as an ArrayOfString</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_DisableExecuteApiEndpoint"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/DisableExecuteApiEndpoint</a>
            </td>
            <td tabindex=""-1"">Filters access by status of the default execute-api endpoint of the current RestApi resource. Available during UpdateRestApi and DeleteRestApi operations</td>
            <td tabindex=""-1"">Bool</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_EndpointType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/EndpointType</a>
            </td>
            <td tabindex=""-1"">Filters access by endpoint type. Available during the UpdatePlayGroundName, DeletePlayGroundName, UpdateRestApi, and DeleteRestApi operations</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_MtlsTrustStoreUri"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/MtlsTrustStoreUri</a>
            </td>
            <td tabindex=""-1"">Filters access by URI of the truststore used for mutual TLS authentication. Available during UpdatePlayGroundName and DeletePlayGroundName operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_MtlsTrustStoreVersion"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/MtlsTrustStoreVersion</a>
            </td>
            <td tabindex=""-1"">Filters access by version of the truststore used for mutual TLS authentication. Available during UpdatePlayGroundName and DeletePlayGroundName operations</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_RouteAuthorizationType"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/RouteAuthorizationType</a>
            </td>
            <td tabindex=""-1"">Filters access by authorization type of the existing Method resource, for example NONE, AWS_IAM, CUSTOM, JWT, COGNITO_USER_POOLS. Available during the PutMethod and DeleteMethod operations. Also available as a collection during reimport</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-playground_Resource_SecurityPolicy"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/security_iam_service-with-iam.html"">playground:Resource/SecurityPolicy</a>
            </td>
            <td tabindex=""-1"">Filters access by TLS version. Available during UpdatePlayGround and DeletePlayGround operations</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-aws_RequestTag___TagKey_"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/playground-tagging.html"">aws:RequestTag/${TagKey}</a>
            </td>
            <td tabindex=""-1"">Filters access by the tag key-value pairs in the request</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-aws_ResourceTag___TagKey_"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/playground-tagging.html"">aws:ResourceTag/${TagKey}</a>
            </td>
            <td tabindex=""-1"">Filters access by the tags attached to the resource</td>
            <td tabindex=""-1"">String</td>
        </tr>
        <tr>
            <td tabindex=""-1"" id=""amazonplaygroundmanagement-aws_TagKeys"">
                  
                <a href=""https://docs.aws.amazon.com/playground/latest/developerguide/playground-tagging.html"">aws:TagKeys</a>
            </td>
            <td tabindex=""-1"">Filters access by the tag keys in the request</td>
            <td tabindex=""-1"">ArrayOfString</td>
        </tr>
    </table>
"
);

}