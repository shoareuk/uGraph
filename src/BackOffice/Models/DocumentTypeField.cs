using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Our.Umbraco.uGraph.BackOffice.Models
{
    public class DocumentTypeField
    {
        public DocumentTypeField() { }

        public DocumentTypeField(int id, string alias, string label, string description, bool canToggle, bool isField, bool isArgument, string fieldName, string dataType, string qlDataType, bool nullable)
        {
            Id = id;
            Alias = alias;
            Label = label;
            Description = description;
            CanToggle = canToggle;
            IsField = isField;
            IsArgument = isArgument;
            FieldName = fieldName;
            DataType = dataType;
            QLDataType = qlDataType;
            Nullable = nullable;
        }

        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DataMember(Name = "alias")]
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "label")]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [DataMember(Name = "description")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "canToggle")]
        [JsonProperty(PropertyName = "canToggle")]
        public bool CanToggle { get; set; }

        [DataMember(Name = "isField")]
        [JsonProperty(PropertyName = "isField")]
        public bool IsField { get; set; }

        [DataMember(Name = "isArgument")]
        [JsonProperty(PropertyName = "isArgument")]
        public bool IsArgument { get; set; }

        [DataMember(Name = "fieldName")]
        [JsonProperty(PropertyName = "fieldName")]
        public string FieldName { get; set; }

        [DataMember(Name = "dataType")]
        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        [DataMember(Name = "qlDataType")]
        [JsonProperty(PropertyName = "qlDataType")]
        public string QLDataType { get; set; }

        [DataMember(Name = "Nullable")]
        [JsonProperty(PropertyName = "nullable")]
        public bool Nullable { get; set; }
    }
}
