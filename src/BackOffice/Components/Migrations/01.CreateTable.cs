using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.uGraph.BackOffice.Components.Migrations
{
    public class CreateTable: MigrationBase
    {
        public CreateTable(IMigrationContext context): base(context) { }

        public override void Migrate()
        {
            if (!TableExists("uGraph"))
                Create.Table<UGraphSchema>().Do();
        }
    }
}
