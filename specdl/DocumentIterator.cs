/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using Spec;
using RootDoc;

public ref struct DocumentIterator
{
  const string
    BASE_URL = "https://docs.aws.amazon.com/service-authorization/latest/reference",
    ROOT_DOC = "reference_policies_actions-resources-contextkeys.html";

  private RootDocList _Root;
  private SpecDownloader _Downloader;
  private RootDocProcessor _Processor;

  public DocumentIterator()
  {
    _Downloader = new SpecDownloader();
    _Processor = new RootDocProcessor();
    _Root = new RootDocList();
  }

  public void LoadDocList()
  {
    _Downloader.SetDestination(Console.Out);
    _Downloader.SetUrl(BASE_URL);
    _Downloader.SetProcessor(_Processor);
    _Downloader.DownloadSource(ROOT_DOC);
    _Downloader.Process(BASE_URL + "/" + ROOT_DOC);
  }
}