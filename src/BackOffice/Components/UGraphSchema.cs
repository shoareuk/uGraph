using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    [TableName("uGraph")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class UGraphSchema
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("DocTypeId")]
        public int DocTypeId { get; set; }

        [Column("Properties")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Properties { get; set; }

        [Column("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
