namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisibleOnActivityPoint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityPoints", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityPoints", "Visible");
        }
    }
}
