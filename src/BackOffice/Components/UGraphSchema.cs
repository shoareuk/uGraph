using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    [TableName("uGraph")]
    [PrimaryKey("DocTypeId", AutoIncrement = false)]
    [ExplicitColumns]
    public class UGraphSchema
    {
        [Column("DocTypeId")]
        public int DocTypeId { get; set; }

        [Column("Alias")]
        public string Alias { get; set; }

        [Column("Obselete")]
        public string Name { get; set; }

        [Column("Obselete")]
        public bool Enabled { get; set; }

        [Column("Obselete")]
        public bool Obselete { get; set; }

        [Column("Tabs")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Tabs { get; set; }

        [Column("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
