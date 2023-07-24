/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Spec;

using System.IO;



const string
  BASE_URL = "https://docs.aws.amazon.com",
  // ROOT_DOC = "/service-authorization/latest/reference/reference_policies_actions-resources-contextkeys.html",
  ACCT_MGT_DOC = "service-authorization/latest/reference/list_alexaforbusiness.html";


SpecDownloader downloader = new SpecDownloader();
DocProcessor docProcessor = new DocProcessor();

downloader.SetDestination(Console.Out);
downloader.SetUrl(BASE_URL);
downloader.SetProcessor( docProcessor );

downloader.DownloadSource( ACCT_MGT_DOC );
downloader.Process(BASE_URL + "/" + ACCT_MGT_DOC);

/*

string currentDir = Directory.GetCurrentDirectory();

Console.WriteLine(value: $"SPECDL running in: {currentDir}");

DocumentIterator _DocIterator = new DocumentIterator();
_DocIterator.LoadDocList();
Console.WriteLine("Docs...");
_DocIterator.IterateDocs();
*/