/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

// using Spec;

using System;



// const string
//   ALEXA_FOR_BUSINESS = "list_alexaforbusiness.html";

//PageProcessingControl.ProcessSpecificPage(ALEXA_FOR_BUSINESS, Console.Out);

PageProcessingControl.IterateThroughAllPagesAvailable();

// SpecDownloader downloader = new SpecDownloader();
// DocProcessor docProcessor = new DocProcessor();

// downloader.SetDestination(Console.Out);
// downloader.SetUrl(BASE_URL);
// downloader.SetProcessor( docProcessor );

// downloader.DownloadSource( ACCT_MGT_DOC );
// downloader.Process(BASE_URL + "/" + ACCT_MGT_DOC);

/*

string currentDir = Directory.GetCurrentDirectory();

Console.WriteLine(value: $"SPECDL running in: {currentDir}");

DocumentIterator _DocIterator = new DocumentIterator();
_DocIterator.LoadDocList();
Console.WriteLine("Docs...");
_DocIterator.IterateDocs();
*/