namespace TI.Configuration.SQL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class code_fields_required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SQLAppConfigs", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.SQLAppConfigSettings", "Code", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.SQLAppConfigSettings", "Code", c => c.String());
            AlterColumn("dbo.SQLAppConfigs", "Code", c => c.String());
        }
    }
}
