namespace TI.Configuration.SQL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class add_code_to_cfg_and_settings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SQLAppConfigs", "Code", c => c.String());
            AddColumn("dbo.SQLAppConfigSettings", "Code", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.SQLAppConfigSettings", "Code");
            DropColumn("dbo.SQLAppConfigs", "Code");
        }
    }
}
