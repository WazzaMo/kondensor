/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

const string
  BASE_URL = "https://docs.aws.amazon.com",
  ROOT_DOC = "/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html",
  ACCT_MGT_DOC = "service-authorization/latest/reference/list_awsaccountmanagement.html";


SpecDownloader downloader = new SpecDownloader();
DocProcessor docProcessor = new DocProcessor();

downloader.SetDestination(Console.Out);
downloader.SetUrl(BASE_URL);
downloader.SetProcessor( docProcessor );
downloader.DownloadSource(ROOT_DOC);
downloader.Process();
Console.WriteLine("-----------------------------------");
downloader.DownloadSource( ACCT_MGT_DOC );
downloader.Process();

