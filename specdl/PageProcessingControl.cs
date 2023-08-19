/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.IO;

using Spec;

/// <summary>
/// Operations for handling iterating all available files or processing
/// a single, specific file are here.
/// </summary>
public static class PageProcessingControl
{
  const string
    BASE_URL = "https://docs.aws.amazon.com",
    PATH = "service-authorization/latest/reference/";

  public static void ProcessSpecificPage(string pageFile, TextWriter writer)
  {
    SpecDownloader downloader = new SpecDownloader();
    DocProcessor docProcessor = new DocProcessor();

    downloader.SetDestination(writer);
    downloader.SetUrl(BASE_URL);
    downloader.SetProcessor( docProcessor );

    downloader.DownloadSource( PATH + pageFile );
    downloader.Process(BASE_URL + "/" + PATH + pageFile);
  }

  /// <summary>
  /// Process all the pages available to create specification files
  /// and return the appropriate status code.
  /// </summary>
  public static int IterateThroughAllPagesAvailable()
  {
    DocumentIterator _DocIterator = new DocumentIterator();
    _DocIterator.LoadDocList();
    _DocIterator.IterateDocs();

    Console.Out.WriteLine(value: $"Number files processed: {_DocIterator.TotalFiles}");

    int returnCode = _DocIterator.NumErrors > 0
      ? -1
      : 0;
    
    return returnCode;
  }
}