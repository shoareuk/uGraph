using GraphQL.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Umbraco.Core.Models;
using Umbraco.Web.Models.ContentEditing;

namespace Our.Umbraco.uGraph.BackOffice.Models
{
    public class DocumentType
    {
        public DocumentType(IContentType docType, IEnumerable<IDataType> dataTypes)
        {
            if (docType == null)
                return;

            Id = docType.Id;
            Alias = docType.Alias;
            Name = docType.Name;
            
            Tabs = new List<Tab<DocumentTypeField>>();

            if (docType.CompositionPropertyGroups?.Count() > 0)
            {
                var tabGeneral = new Tab<DocumentTypeField>();

                tabGeneral.Id = 1;
                tabGeneral.Alias = "general";
                tabGeneral.Label = "General";
                tabGeneral.IsActive = true;
                tabGeneral.Expanded = true;

                tabGeneral.Properties = new List<DocumentTypeField>
                {
                    new DocumentTypeField(1, "id", "Id", "Umbraco CMS Node Id", false, true, true, "Id", typeof(int).Name, typeof(IntGraphType).Name),
                    new DocumentTypeField(2, "url", "Url", "Umbraco CMS Node Url", false, true, true, "Url", typeof(string).Name, typeof(StringGraphType).Name)
                };

                Tabs.Add(tabGeneral);

                var compositionGroups = docType.CompositionPropertyGroups.ToList();

                for (int i = 0; i < compositionGroups.Count; i++)
                {
                    var propertyGroup = compositionGroups[i];

                    if (propertyGroup?.PropertyTypes?.Count > 0)
                    {
                        var tab = new Tab<DocumentTypeField>();

                        tab.Id = propertyGroup.Id;
                        tab.Alias = propertyGroup.Name;
                        tab.Label = propertyGroup.Name;
                        tab.IsActive = false;
                        tab.Expanded = false;

                        var properties = new List<DocumentTypeField>();

                        foreach (var propertyType in propertyGroup.PropertyTypes)
                        {
                            var field = new DocumentTypeField
                            {
                                Id = propertyType.Id,
                                Alias = propertyType.Alias,
                                Label = propertyType.Name,
                                Description = propertyType.Description,
                                CanToggle = true,
                                IsField = true,
                                IsArgument = false,
                                FieldName = propertyType.Alias,
                                DataType = Helpers.DataTypeHelpers.DetermineSystemDataType(propertyType.DataTypeId, dataTypes),
                                QLDataType = Helpers.DataTypeHelpers.DetermineGraphQLDataType(propertyType.DataTypeId, dataTypes)
                            };

                            properties.Add(field);
                        }

                        tab.Properties = properties;

                        Tabs.Add(tab);
                    }
                }
            }
        }

        [DataMember(Name = "tabs")]
        [JsonProperty(PropertyName = "tabs")]
        public List<Tab<DocumentTypeField>> Tabs { get; set; }

        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DataMember(Name = "alias")]
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DataMember(Name = "enabled")]
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [DataMember(Name = "obselete")]
        [JsonProperty(PropertyName = "obselete")]
        public bool Obselete { get; set; }

        [DataMember(Name="maxTabs")]
        [JsonProperty(PropertyName = "maxTabs")]
        public int MaxTabs => Tabs.Count;
    }
}
