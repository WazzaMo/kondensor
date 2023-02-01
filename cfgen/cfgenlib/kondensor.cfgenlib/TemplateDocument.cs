/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System.Collections.Generic;
using Optional;


namespace kondensor.cfgenlib
{
    public struct TemplateDocument
    {
      public string Environment {get; private set;}
      public string Name { get; private set; }

      public Option<Header> Header;

      public Option<Metadata> Metadata;

      public List<Parameter> Parameters;

      public List<Resource> Resources;

      public Option<Mapping> Mappings;

      public Option<Outputs> Outputs;

      public TemplateDocument(
        string environment,
        string name,
        string description
      )
      {
        Environment = environment;
        Name = name;

        Header header = new Header(description);
        Header = Option.Some(header);
        Metadata = Option.None<Metadata>();
        Parameters = new List<Parameter>();
        Resources = new List<Resource>();
        Mappings = Option.None<Mapping>();
        Outputs = Option.Some( new Outputs() );
      }
    }
}

