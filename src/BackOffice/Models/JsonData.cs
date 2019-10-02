using System.Runtime.Serialization;

namespace Our.Umbraco.uGraph.BackOffice.Models
{
    public class JsonData<T>
    {
        public JsonData(T data)
        {
            Data = data;
        }


        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
