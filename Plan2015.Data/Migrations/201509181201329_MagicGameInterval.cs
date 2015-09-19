namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MagicGameInterval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MagicGamesIntervals",
                c => new
                    {
                        ScoutId = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoutId)
                .ForeignKey("dbo.Scouts", t => t.ScoutId)
                .Index(t => t.ScoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MagicGamesIntervals", "ScoutId", "dbo.Scouts");
            DropIndex("dbo.MagicGamesIntervals", new[] { "ScoutId" });
            DropTable("dbo.MagicGamesIntervals");
        }
    }
}
