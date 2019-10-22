using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Our.Umbraco.uGraph.BackOffice.Interfaces;
using Our.Umbraco.uGraph.BackOffice.Models;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.uGraph.BackOffice.Controllers
{
    public class uGraphApiController: UmbracoApiController
    {
        private readonly IUGraphDataAccess _dataAccess;

        public uGraphApiController(IUGraphDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public DocumentType GetDocType(int id)
        {
            if (id <= 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var dbDocType = _dataAccess.GetDocType(id);

            IContentType docType;

            try     { docType = Services.ContentTypeService.Get(id); }
            catch   { docType = null; }

            if (docType == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var dataTypes = Services.DataTypeService.GetAll();

            var documentType = new DocumentType(docType, dataTypes);

            documentType.Tabs.ForEach(x => x.Expanded = false);

            if (dbDocType == null)
                return documentType;

            documentType.Enabled = dbDocType.Enabled;

            foreach (var tab in documentType.Tabs)
            {
                var dbTab = dbDocType.Tabs.FirstOrDefault(x => x.Id == tab.Id);

                if (dbTab == null)
                    continue;

                foreach (var property in tab.Properties)
                {
                    var dbProperty = dbTab.Properties.FirstOrDefault(x => x.Id == property.Id);

                    if (dbProperty == null)
                        continue;

                    property.IsField = dbProperty.IsField;
                    property.IsArgument = dbProperty.IsArgument;
                    property.Alias = dbProperty.Alias;
                }
            }

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
                    Content = new StringContent(bool.FalseString)
                };

                return response;
            }

            var saved = _dataAccess.AddUpdateDocType(docType);

            if (saved)
                SaveGraphQLClassFile(docType);

            response = new HttpResponseMessage(saved ? HttpStatusCode.OK : HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(saved.ToString())
            };

            return response;
        }

        private void SaveGraphQLClassFile(Models.DocumentType docType)
        {

        }
    }
}
