/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.resources;

namespace kondensor.cfgenlib.api
{

  public struct Stack
  {
    private Dictionary<string, IResourceType> _Ids;

    public TemplateDocument Document {get; private set; }

    public Stack AddResource<Tr>(
      string id,
      Func<Tr,IResourceType> propSetter,
      params string[] optText
    )
    where Tr : struct, IResourceType
    {
      Tr empty = new Tr();
      empty.setId(id);
      Tr props = (Tr) propSetter(empty);
      props.AddOutput(Document, Document.Environment, Document.Name, optText);
      props.AddTag(key: "Environment", Document.Environment);
      props.AddTag(key: "Project", Document.Name);
      _Ids.Add(id, props);
      Resource res = new Resource(id, props);
      Document.Resources.Add(res);
      return this;
    }

    public Ref Ref(string id)
    {
      if (_Ids.ContainsKey(id))
        return new Ref(id);
      else
        throw new ArgumentException($"ID {id} does not exist - no such resource.");
    }

    public Stack(string environment, string name, string description)
    {
      Document = new TemplateDocument(environment, name, description );
      _Ids = new Dictionary<string, IResourceType>();
    }
  }

}
