namespace TI.Configuration.SQL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Removed_Unused_Column : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SQLAppConfigs", "SQLAppConfigId");
        }

        public override void Down()
        {
            AddColumn("dbo.SQLAppConfigs", "SQLAppConfigId", c => c.Int(nullable: false));
        }
    }
}
