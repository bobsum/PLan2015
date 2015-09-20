namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSwipe : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MarkerPoints", newName: "MagicGamesMarkerPoints");
            CreateTable(
                "dbo.MagicGamesTimePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.HouseId);
            
            AddColumn("dbo.MagicGamesIntervals", "LastSwipe", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropIndex("dbo.MagicGamesTimePoints", new[] { "HouseId" });
            DropColumn("dbo.MagicGamesIntervals", "LastSwipe");
            DropTable("dbo.MagicGamesTimePoints");
            RenameTable(name: "dbo.MagicGamesMarkerPoints", newName: "MarkerPoints");
        }
    }
}
