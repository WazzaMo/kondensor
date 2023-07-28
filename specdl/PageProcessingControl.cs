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
  /// 
  /// </summary>
  public static void IterateThroughAllPagesAvailable()
  {
    DocumentIterator _DocIterator = new DocumentIterator();
    _DocIterator.LoadDocList();
    _DocIterator.IterateDocs();
  }
}