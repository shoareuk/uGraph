using Our.Umbraco.uGraph.BackOffice.Components;
using Our.Umbraco.uGraph.BackOffice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Mapping;
using Umbraco.Core.Scoping;

namespace Our.Umbraco.uGraph.BackOffice.DataAccess
{
    public class UGraphDataAccess: IUGraphDataAccess
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly UmbracoMapper _mapper;

        public UGraphDataAccess(IScopeProvider scopeProvider, UmbracoMapper mapper)
        {
            _scopeProvider = scopeProvider;
            _mapper = mapper;
        }

        public IList<Models.DocumentType> GetAllDocTypes()
        {
            List<Models.DocumentType> docTypes = null;

            IScope scope = null;

            try
            {
                scope = _scopeProvider.CreateScope();

                var dbDocTypes = scope.Database.Fetch<UGraphSchema>();

                if(dbDocTypes != null && dbDocTypes.Any())
                {
                    foreach (var dbDocType in dbDocTypes)
                        docTypes.Add(_mapper.Map<Models.DocumentType>(dbDocType));
                }
            }
            catch(Exception ex)
            {
                docTypes = null;
            }
            finally
            {
                scope?.Complete();
                scope?.Dispose();
            }

            return docTypes;
        }

        public Models.DocumentType GetDocType(int id)
        {
            Models.DocumentType docType = null;

            IScope scope = null;

            try
            {
                scope = _scopeProvider.CreateScope();

                return GetDocType(scope, id);
            }
            catch
            {
                docType = null;
            }
            finally
            {
                scope?.Complete();
                scope?.Dispose();
            }

            return docType;
        }

        public bool AddUpdateDocType(Models.DocumentType docType)
        {
            IScope scope = null;

            try
            {
                var dbDocType = _mapper.Map<UGraphSchema>(docType);
                dbDocType.LastUpdate = DateTime.Now;

                scope = _scopeProvider.CreateScope();

                var hasDocType = GetDocType(scope, docType.Id) != null;

                if(hasDocType)
                {
                    scope.Database.Update(dbDocType);
                }
                else
                {
                    scope.Database.Insert<UGraphSchema>(dbDocType);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                scope?.Complete();
                scope?.Dispose();
            }
        }

        private Models.DocumentType GetDocType(IScope scope, int docTypeId)
        {
            if (scope == null || docTypeId < 1)
                return null;

            var dbDocType = scope.Database.Fetch<UGraphSchema>().Where(x => x.DocTypeId.Equals(docTypeId)).FirstOrDefault();

            try
            {
                if (dbDocType != null)
                    return _mapper.Map<Models.DocumentType>(dbDocType);
            }
            catch { ; }

            return null;
        }
    }
}
