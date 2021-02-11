namespace TI.Configuration.SQL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class added_keys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SQLAppConfigSettings", "AppConfigId", "dbo.SQLAppConfigs");
            DropIndex("dbo.SQLAppConfigSettings", new[] { "AppConfigId" });
            DropPrimaryKey("dbo.SQLAppConfigs");
            DropPrimaryKey("dbo.SQLAppConfigSettings");
            AddColumn("dbo.SQLAppConfigs", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SQLAppConfigSettings", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SQLAppConfigSettings", "Name", c => c.String());
            AlterColumn("dbo.SQLAppConfigs", "Name", c => c.String());
            AlterColumn("dbo.SQLAppConfigSettings", "AppConfigId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SQLAppConfigs", "Id");
            AddPrimaryKey("dbo.SQLAppConfigSettings", "Id");
            CreateIndex("dbo.SQLAppConfigSettings", "AppConfigId");
            AddForeignKey("dbo.SQLAppConfigSettings", "AppConfigId", "dbo.SQLAppConfigs", "Id", cascadeDelete: true);
            DropColumn("dbo.SQLAppConfigSettings", "Key");
        }

        public override void Down()
        {
            AddColumn("dbo.SQLAppConfigSettings", "Key", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.SQLAppConfigSettings", "AppConfigId", "dbo.SQLAppConfigs");
            DropIndex("dbo.SQLAppConfigSettings", new[] { "AppConfigId" });
            DropPrimaryKey("dbo.SQLAppConfigSettings");
            DropPrimaryKey("dbo.SQLAppConfigs");
            AlterColumn("dbo.SQLAppConfigSettings", "AppConfigId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SQLAppConfigs", "Name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.SQLAppConfigSettings", "Name");
            DropColumn("dbo.SQLAppConfigSettings", "Id");
            DropColumn("dbo.SQLAppConfigs", "Id");
            AddPrimaryKey("dbo.SQLAppConfigSettings", new[] { "Key", "Mode" });
            AddPrimaryKey("dbo.SQLAppConfigs", "Name");
            CreateIndex("dbo.SQLAppConfigSettings", "AppConfigId");
            AddForeignKey("dbo.SQLAppConfigSettings", "AppConfigId", "dbo.SQLAppConfigs", "Name", cascadeDelete: true);
        }
    }
}
