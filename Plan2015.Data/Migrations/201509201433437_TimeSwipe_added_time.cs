namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSwipe_added_time : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MagicGamesTimePoints", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MagicGamesTimePoints", "Time");
        }
    }
}
