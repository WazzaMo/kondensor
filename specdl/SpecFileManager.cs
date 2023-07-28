/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.IO;
using System.Text.RegularExpressions;

using Spec;
using RootDoc;


public struct SpecFileManager
{
  const char
    WORD_SEP = '_',
    WORD_DELIM_TO_REPLACE = ' ';
  const string
    PATH_SPEP = "/",
    SPEC_EXTENSION = "yaml",
    SPEC_DIRECTORY = "downloaded_specs";

  private string _DownloadsPath;

  public SpecFileManager()
  {
    _DownloadsPath = GetFullPathSpecDownloads(SpecsDownloadDirectory);
    EnsureSpecsDirectoryReady();
  }

  public StreamWriter GetOutputStreamFor(SubDoc doc)
  {
    string filename = TitleToFileName(doc);
    string filePath = _DownloadsPath + PATH_SPEP + filename;
    
    StreamWriter writer = new StreamWriter(filePath);
    return writer;
  }

  private static string SpecsDownloadDirectory => "/"+SPEC_DIRECTORY;

  private static string GetFullPathSpecDownloads(string specDownloadPath)
  {
    Regex regex = new Regex(pattern: @"/[^\/]+$");
    string currentPath = Directory.GetCurrentDirectory();
    string fullPath = regex.Replace(currentPath, specDownloadPath);
    return fullPath;
  }
  
  private void EnsureSpecsDirectoryReady()
  {
    if (! Directory.Exists(_DownloadsPath))
    {
      Directory.CreateDirectory(_DownloadsPath);
    }
  }

  private string TitleToFileName(SubDoc doc)
  {
    string filename = doc.Title.Replace(WORD_DELIM_TO_REPLACE, WORD_SEP);
    filename = filename + "." + SPEC_EXTENSION;
    return filename;
  }
}