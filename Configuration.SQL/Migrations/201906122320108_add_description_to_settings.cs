namespace TI.Configuration.SQL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class add_description_to_settings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SQLAppConfigSettings", "Description", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.SQLAppConfigSettings", "Description");
        }
    }
}
