namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogToTurnoutPoint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TurnoutPoints", "Log", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TurnoutPoints", "Log");
        }
    }
}
