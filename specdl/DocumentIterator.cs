/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0
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
  private int _CountErrors;
  private int _CountSuccess;

  public DocumentIterator()
  {
    _RootProcessor = new RootDocProcessor();
    _SpecManager = new SpecFileManager();
    _CountErrors = 0;
    _CountSuccess = 0;
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
      // Console.WriteLine($"Writing: {doc.Title} => {doc.Path}");
      ReadAndGenerateSpec(doc);
    }
  }

  public int NumSuccesses => _CountSuccess;
  public int NumErrors => _CountErrors;
  public int TotalFiles => _CountErrors + _CountSuccess;

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
    ReportStats(doc, docProcessor.GetStats());
  }

  private string SourceOf(string doc)
    => REL_PATH + doc;
  
  private void ReportStats(SubDoc doc, DocStats stats)
  {
    if (stats.IsParsedOk)
    {
      _CountSuccess++;
      Console.Out.WriteLine(value: $"{doc.Path}: OK");
    }
    else
    {
      _CountErrors++;
      string errors = $"Action Table [{stats.ActionTableErrors}]  "
        + $"Resource Table [{stats.ResourceTableErrors}]  "
        + $"Condition Key Table [{stats.ConditionKeyTableErrors}]";
      
      Console.Error.WriteLine(
        value: doc.Path + ": " + errors
      );
    }
  }
}