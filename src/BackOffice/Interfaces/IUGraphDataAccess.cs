using System.Collections.Generic;

namespace Our.Umbraco.uGraph.BackOffice.Interfaces
{
    public interface IUGraphDataAccess
    {
        IList<Models.DocumentType> GetAllDocTypes();

        Models.DocumentType GetDocType(int id);

        bool AddUpdateDocType(Models.DocumentType docType);
    }
}
