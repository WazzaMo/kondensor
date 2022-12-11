using System.Collections.Generic;
using Optional;


namespace kondensor.cfgenlib
{
    public struct TemplateDocument
    {
      public Option<Header> Header;

      public Option<Metadata> Metadata;

      public List<Parameter> Parameters;

      public Option<Mapping> Mappings;


      public TemplateDocument(
        Header header
      )
      {
        Header = Option.Some(header);
        Metadata = Option.None<Metadata>();
        Parameters = new List<Parameter>();
        Mappings = Option.None<Mapping>();
      }
    }
}

