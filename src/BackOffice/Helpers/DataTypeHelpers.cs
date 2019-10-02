using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Our.Umbraco.uGraph.BackOffice.Helpers
{
    public static class DataTypeHelpers
    {
        public static string DetermineSystemDataType(int umbDataTypeId, IEnumerable<IDataType> dataTypes)
        {
            if (umbDataTypeId == 0 || dataTypes == null || !dataTypes.Any())
                return string.Empty;

            var dataType = (from dt in dataTypes
                            where dt.Id == umbDataTypeId
                            select dt).FirstOrDefault();

            if (dataType == null)
                return string.Empty;

            switch (dataType.DatabaseType)
            {
                case ValueStorageType.Ntext:
                case ValueStorageType.Nvarchar:
                    return typeof(string).Name;

                case ValueStorageType.Integer:
                    return typeof(int).Name;

                case ValueStorageType.Date:
                    return typeof(DateTime).Name;

                case ValueStorageType.Decimal:
                    return typeof(float).Name;
            }

            return string.Empty;
        }

        public static string DetermineGraphQLDataType(int umbDataTypeId, IEnumerable<IDataType> dataTypes)
        {
            if (umbDataTypeId == 0 || dataTypes == null || !dataTypes.Any())
                return string.Empty;

            var dataType = (from dt in dataTypes
                           where dt.Id == umbDataTypeId
                           select dt).FirstOrDefault();

            if (dataType == null)
                return string.Empty;

            switch (dataType.DatabaseType)
            {
                case ValueStorageType.Ntext:
                case ValueStorageType.Nvarchar:
                    return typeof(StringGraphType).Name;

                case ValueStorageType.Integer:
                    return typeof(IntGraphType).Name;

                case ValueStorageType.Date:
                    return typeof(DateGraphType).Name;

                case ValueStorageType.Decimal:
                    return typeof(FloatGraphType).Name;
            }

            return string.Empty;
        }
    }
}
