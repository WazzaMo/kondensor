using System.Collections.Generic;
using Optional;


namespace kondensor.cfgenlib
{
    public struct TemplateDocument
    {
      public Option<Header> Header;

      public Option<Metadata> Metadata;

      public List<Parameter> Parameters;

      public List<Resource> Resources;

      public Option<Mapping> Mappings;

      public Option<Outputs> Outputs;

      public TemplateDocument(
        Header header
      )
      {
        Header = Option.Some(header);
        Metadata = Option.None<Metadata>();
        Parameters = new List<Parameter>();
        Resources = new List<Resource>();
        Mappings = Option.None<Mapping>();
        Outputs = Option.Some( new Outputs() );
      }
    }
}

