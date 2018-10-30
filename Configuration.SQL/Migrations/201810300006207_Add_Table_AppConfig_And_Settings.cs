namespace TI.Configuration.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_AppConfig_And_Settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SQLAppConfigs",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        SQLAppConfigId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.SQLAppConfigSettings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Mode = c.Int(nullable: false),
                        Value = c.String(),
                        AppConfigId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Key, t.Mode })
                .ForeignKey("dbo.SQLAppConfigs", t => t.AppConfigId, cascadeDelete: true)
                .Index(t => t.AppConfigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SQLAppConfigSettings", "AppConfigId", "dbo.SQLAppConfigs");
            DropIndex("dbo.SQLAppConfigSettings", new[] { "AppConfigId" });
            DropTable("dbo.SQLAppConfigSettings");
            DropTable("dbo.SQLAppConfigs");
        }
    }
}
