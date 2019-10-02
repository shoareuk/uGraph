using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    public class UGraphTable : MigrationBase
    {
        public UGraphTable(IMigrationContext context): base(context) { }

        public override void Migrate()
        {
            Logger.Debug<UGraphTable>("Running migration {MigrationStep}", "UGraphTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("uGraph") == false)
            {
                Create.Table<UGraphSchema>().Do();
            }
            else
            {
                Logger.Debug<UGraphTable>("The database table {DbTable} already exists, skipping", "uGraph");
            }
        }
    }
}
