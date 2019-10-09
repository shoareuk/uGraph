using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Our.Umbraco.uGraph.BackOffice.Models;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.uGraph.BackOffice.Controllers
{
    public class uGraphApiController: UmbracoApiController
    {
        [HttpGet]
        public DocumentType GetDocType(int id)
        {
            if (id <= 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            IContentType docType;

            try     { docType = Services.ContentTypeService.Get(id); }
            catch   { docType = null; }

            if (docType == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var dataTypes = Services.DataTypeService.GetAll();

            var documentType = new DocumentType(docType, dataTypes);

            documentType.Tabs.ForEach(x => x.Expanded = false); 

            return documentType;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveDocType(int id)
        {
            DocumentType docType;

            try
            {
                var json = await Request.Content.ReadAsStringAsync();

                docType = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentType>(json);
            }
            catch   { docType = null; }

            HttpResponseMessage response;

            if(docType == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("0")
                };

                return response;
            }

            var stop = "STOP";

            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("1")
            };

            return response;
        }
    }
}
