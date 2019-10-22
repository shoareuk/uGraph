using Umbraco.Core.Migrations;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    public class UGraphMigrationPlan: MigrationPlan
    {
        public UGraphMigrationPlan(): base("uGraph")
        {
            From(string.Empty).To<Migrations.CreateTable>("init");
            From("uGraph-db").To<Migrations.CreateTable>("init");
        }
    }
}
