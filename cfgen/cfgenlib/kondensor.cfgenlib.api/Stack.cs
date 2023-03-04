/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */

using System;
using System.Collections.Generic;

using kondensor.cfgenlib.primitives;
using kondensor.cfgenlib.resources;
using kondensor.cfgenlib.outputs;
using kondensor.cfgenlib.policy;

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
      props.AssertRequiredPropertiesSet();
      props.AddOutput(Document, Document.Environment, Document.Name, optText);
      props.AddTag(key: "Environment", Document.Environment);
      props.AddTag(key: "Project", Document.Name);
      _Ids.Add(id, props);
      Resource res = new Resource(id, props);
      Document.Resources.Add(res);
      return this;
    }

    public Stack AddResourceAndGetRef<Tr>(
      string id,
      out Ref reference,
      Func<Tr,IResourceType> propSetter,
      params string[] optText
    )
    where Tr : struct, IResourceType
    {
      AddResource<Tr>(id, propSetter, optText);
      reference = new Ref(id);
      return this;
    }

    public Tr AddChild<Tr>(
      string id,
      Func<Tr,Tr> propSetter
    ) where Tr : struct, IResourceType
    {
      Tr empty = new Tr();
      empty.setId(id);
      Tr props = propSetter(empty);
      return props;
    }

    /// <summary>
    /// Declare an import variable for use in the stack program.
    /// </summary>
    /// <param name="id">Original ID that export was based on in original stack.</param>
    /// <param name="import">Import variable to be returned.</param>
    /// <typeparam name="Tr">Resource type</typeparam>
    /// <returns>Same fluid stack.</returns>
    public Stack AddImport<Tr>(string id, out Import import)
      where Tr : struct, IResourceType
    {
      Tr empty = new Tr();
      empty.setId(id);
      ExportData export = new ExportData(Document.Environment, empty);
      import = new Import(export);
      return this;
    }

    public Stack Ref(string id, out Ref reference)
    {
      if (!_Ids.ContainsKey(id))
        throw new ArgumentException($"ID {id} does not exist - no such resource.");
      reference = new Ref(id);
      return this;
    }

    public Stack(string environment, string name, string description)
    {
      Document = new TemplateDocument(environment, name, description );
      _Ids = new Dictionary<string, IResourceType>();
    }
  }

}
