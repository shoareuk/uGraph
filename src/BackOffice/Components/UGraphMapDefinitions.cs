using System.Collections.Generic;
using Umbraco.Core.Mapping;
using Umbraco.Web.Models.ContentEditing;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    public class UGraphMapDefinitions: IMapDefinition
    {
        public void DefineMaps(UmbracoMapper mapper)
        {
            mapper.Define<UGraphSchema, Models.DocumentType>((source, target) => new Models.DocumentType(), (source, target, context) =>
            {
                target.Id = source.DocTypeId;
                target.Alias = source.Alias;
                target.Name = source.Name;
                target.Enabled = source.Enabled;
                target.Obselete = source.Obselete;
                target.Tabs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tab<Models.DocumentTypeField>>>(source.Tabs);
            });

            mapper.Define<Models.DocumentType, UGraphSchema>((source, target) => new UGraphSchema(), (source, target, context) =>
            {
                target.DocTypeId = source.Id;
                target.Alias = source.Alias;
                target.Name = source.Name;
                target.Enabled = source.Enabled;
                target.Obselete = source.Obselete;
                target.Tabs = Newtonsoft.Json.JsonConvert.SerializeObject(source.Tabs);
            });
        }
    }
}
