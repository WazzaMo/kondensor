# Welcome to Kondensor

Kondense AWS cloud services into tangible systems!
Build AWS cloud-native apps faster.

This project is made available to you under the GPL v3 for application code and tools
and the Lesser GPL (LGPL) for libraries so you
can do whatever you want with it but must also make any changes
you make and build into another project, available to those users.

## Status

Very much still in development.

## Tools

### Specdl

Short for "specification downloader," specdl downloads the HTML AWS documentation
for Actions, Resources and Condition Keys and turns it into YAML code
from which other library code can be generated. The intention is to make
it easier to keep current with AWS' ongoing changes to their services
and how security and access policies can and should be specified for their
web services.


## Libraries in this mono-repo

All libraries have Xunit unit tests and these both help check the code
works over time but also document the supported use cases.

### kondensor.Pipes

A flexible text to symbol, text streaming data type that allows
custom lexical analysers to be built. Examples and usage in this
mono-repo are for HTML scraping to generate code.

### kondensor.Parser

A parser engine for building simple compilers or transpilers.
It allows a parser and productions to be specified in a fluid API
style and, through the use of annotated matches, makes it easy
to collect the parsed information. This makes linking the compiler
back-end to the front-end, the parser, fairly simple.



## .NET Libraries Used

[Optional - a nuget package providing functional program Option class](https://github.com/nlkl/Optional)

