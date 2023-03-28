/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


StringReader line = new StringReader( "AbortMultipartUpload	Grants permission to abort a multipart upload	Write");

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Reading SPEC...");


Spec.ReadStream(line);
