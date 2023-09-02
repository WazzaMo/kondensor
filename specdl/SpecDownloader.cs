/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed without warranty, under the GNU Public License v3.0
 */

using System.IO;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Optional;

using Spec;

using kondensor.Parser;
using kondensor.Parser.AwsHtmlParse;
using kondensor.Pipes;

using Resources;
using Actions;

public struct SpecDownloader
{
  private Option<string> _Url;

  private Option<HttpClient> _BaseClient;

  private Option<TextWriter> _Destination;

  private Option<TextReader> _Source;

  private Option<IProcessor> _Processor;

  private int _LinesRead;

  public Option<string> Url => _Url;

  public int LinesDownloaded => _LinesRead;

  public SpecDownloader()
  {
    _Url = Option.None<string>();
    _Source = Option.None<TextReader>();
    _Destination = Option.None<TextWriter>();
    _BaseClient = Option.None<HttpClient>();
    _Processor = Option.None<IProcessor>();
    _LinesRead = 0;
  }

  public void SetUrl(string url)
  {
    _Url = Option.Some(url);
    var client = new HttpClient();
    client.BaseAddress = new Uri(url);
    _BaseClient = Option.Some(client);
  }
  
  public void SetDestination(TextWriter destination)
    => _Destination = Option.Some(destination);

  public void SetProcessor(IProcessor processor)
    => _Processor = Option.Some(processor);

  /// <summary>
  /// Sets the source path for the content to be downloaded,
  /// excluding the base domain (path only). 
  /// </summary>
  /// <param name="path">Path from base URL to download</param>
  public void DownloadSource(string path)
  {
    TextReader source;
    string? content = DownloadText(path);

    if (content != null)
    {
      source = new StringReader(content);
      _Source = Option.Some(source);
    }
  }

  public void Process(string path)
  {
    Option<TextReader> _source = _Source;
    Option<TextWriter> _dest = _Destination;

    HtmlPipe htmlPipe;
    ReplayWrapPipe thePipe;

    // IPreprocessor arnPreprocessor = new ArnSpecPreprocessor();
    IPreprocessor boldPreprocessor = new ActionBoldPreprocessor();
    IPreprocessor spanPreprocessor = new ActionSpanPreprocessor();

    _Processor.Match(
      processor => {
        _source.Match(
          source => {
            _dest.Match(
              destination => {
                htmlPipe = new HtmlPipe(_source.ValueOr(Console.In), _dest.ValueOr(Console.Out));
                // htmlPipe.AddPreprocessor( arnPreprocessor );
                htmlPipe.AddPreprocessor( boldPreprocessor );
                htmlPipe.AddPreprocessor( spanPreprocessor );
                thePipe = new ReplayWrapPipe(htmlPipe);
                processor.ProcessAllLines(path, thePipe);
                processor.WriteOutput(thePipe);
              },
              () => Console.Error.WriteLine(value: $"{nameof(SpecDownloader)}: Destination not set!")
            );
          },
          () => Console.Error.WriteLine(value: $"{nameof(SpecDownloader)}: Source not set!")
        );
      },
      () => Console.Error.WriteLine(value: $"{nameof(SpecDownloader)}: Processor not set!")
    );
  }

  private string? DownloadText(string path)
  {
    string? content = null;
    HttpResponseMessage response;
    string? baseUrl = null;

    Action<HttpRequestHeaders> setHeaders = PopulateHeaders;

    _Url.MatchSome( url => baseUrl = url);
    if (baseUrl != null && _BaseClient.HasValue)
    {
      UriBuilder uriBuilder = new UriBuilder(baseUrl);
      uriBuilder.Path = path;

      _BaseClient.MatchSome( client => {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
        setHeaders(request.Headers);
        response = client.Send(request);
        var contentTask = Task.Run( () => response.Content.ReadAsStringAsync() );
        contentTask.Wait();
        content = contentTask.Result;
      });
    }
    return content;
  }

  private void PopulateHeaders(HttpRequestHeaders headers)
  {
    const string
      Accept = "Accept",
      AcceptValue = "text/html,application/xhtml+xml,application/xml;"
        +"q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7",
      AcceptEnc = "Accept-Encoding",
      AcceptEncValue = "br",
      AcceptLang = "Accept-Language",
      AcceptLangValue = "en-GB,en-US;q=0.9,en;q=0.8",
      CacheControl = "Cache-Control",
      CacheControlValue = "max-age=0",
      Connection = "Connection",
      ConnectionValue = "keep-alive",
      UserAgent = "User-Agent",
      UserAgentValue = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko)"
        +" Chrome/111.0.0.0 Safari/537.36";

    headers.Add(Accept, AcceptValue);
    headers.Add(AcceptEnc, AcceptEncValue);
    headers.Add(AcceptLang, AcceptLangValue);
    headers.Add(CacheControl, CacheControlValue);
    headers.Add(Connection, ConnectionValue);
    headers.Add(UserAgent, UserAgentValue);
  }

  private void DumpHeaders(HttpRequestMessage req)
  {
    var _hdrs = req.Headers.AsEnumerable();
    var hdrsEnum = _hdrs.GetEnumerator();
    while(hdrsEnum.MoveNext())
    {
      Console.WriteLine($"{hdrsEnum.Current.Key} = {hdrsEnum.Current.Value.First()}");
    }
  }
}