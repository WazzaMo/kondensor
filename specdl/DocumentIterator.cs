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
    DOMAIN = "https://docs.aws.amazon.com",
    REL_PATH = "service-authorization/latest/reference/",
    ROOT_DOC = "reference_policies_actions-resources-contextkeys.html";

  private RootDocProcessor _RootProcessor;
  private SpecFileManager _SpecManager;

  public DocumentIterator()
  {
    _RootProcessor = new RootDocProcessor();
    _SpecManager = new SpecFileManager();
  }

  public void LoadDocList()
  {
    SpecDownloader _Downloader;
    _Downloader = new SpecDownloader();
    _Downloader.SetDestination(Console.Out);
    _Downloader.SetUrl(DOMAIN);
    _Downloader.SetProcessor(_RootProcessor);
    _Downloader.DownloadSource( SourceOf(ROOT_DOC) );
    _Downloader.Process(DOMAIN + "/" + SourceOf(ROOT_DOC));
  }

  public void IterateDocs()
  {
    var iterator = _RootProcessor.GetEnumerator();
    SubDoc doc;
    while(iterator.MoveNext())
    {
      doc = iterator.Current;
      Console.WriteLine($"Writing: {doc.Title} => {doc.Path}");
      ReadAndGenerateSpec(doc);
    }
  }

  private void ReadAndGenerateSpec(SubDoc doc)
  {
    DocProcessor docProcessor;
    SpecDownloader downloader;
    StreamWriter fileWriter = _SpecManager.GetOutputStreamFor(doc);

    downloader = new SpecDownloader();
    docProcessor = new DocProcessor();
    downloader.SetDestination( fileWriter );
    downloader.SetUrl(DOMAIN);
    downloader.SetProcessor(docProcessor);
    downloader.DownloadSource( SourceOf(doc.Path) );
    downloader.Process( DOMAIN + '/' + SourceOf(doc.Path));
    fileWriter.Close();
  }

  private string SourceOf(string doc)
    => REL_PATH + doc;
}