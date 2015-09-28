namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.ActivityId)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SchoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.MagicGamesMarkerPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MarkerName = c.String(),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.MagicGamesTimePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HouseId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rfid = c.String(),
                        Name = c.String(),
                        HouseId = c.Int(nullable: false),
                        Info = c.String(),
                        Home = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.MagicGamesIntervals",
                c => new
                    {
                        ScoutId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        LastSwipe = c.DateTime(),
                    })
                .PrimaryKey(t => t.ScoutId)
                .ForeignKey("dbo.Scouts", t => t.ScoutId)
                .Index(t => t.ScoutId);
            
            CreateTable(
                "dbo.Punctualities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        All = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PunctualitySwipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PunctualityId = c.Int(nullable: false),
                        ScoutId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Punctualities", t => t.PunctualityId, cascadeDelete: true)
                .ForeignKey("dbo.Scouts", t => t.ScoutId, cascadeDelete: true)
                .Index(t => t.PunctualityId)
                .Index(t => t.ScoutId);
            
            CreateTable(
                "dbo.PunctualityPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PunctualityId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("dbo.Punctualities", t => t.PunctualityId, cascadeDelete: true)
                .Index(t => t.PunctualityId)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rfid = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TurnoutPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        TeamMemberId = c.Int(),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMemberId)
                .Index(t => t.TeamMemberId)
                .Index(t => t.HouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers");
            DropForeignKey("dbo.TurnoutPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.PunctualityPoints", "PunctualityId", "dbo.Punctualities");
            DropForeignKey("dbo.PunctualityPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.PunctualitySwipes", "ScoutId", "dbo.Scouts");
            DropForeignKey("dbo.PunctualitySwipes", "PunctualityId", "dbo.Punctualities");
            DropForeignKey("dbo.ActivityPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.MagicGamesIntervals", "ScoutId", "dbo.Scouts");
            DropForeignKey("dbo.Scouts", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.Houses", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.MagicGamesTimePoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.MagicGamesMarkerPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.ActivityPoints", "ActivityId", "dbo.Activities");
            DropIndex("dbo.TurnoutPoints", new[] { "HouseId" });
            DropIndex("dbo.TurnoutPoints", new[] { "TeamMemberId" });
            DropIndex("dbo.PunctualityPoints", new[] { "HouseId" });
            DropIndex("dbo.PunctualityPoints", new[] { "PunctualityId" });
            DropIndex("dbo.PunctualitySwipes", new[] { "ScoutId" });
            DropIndex("dbo.PunctualitySwipes", new[] { "PunctualityId" });
            DropIndex("dbo.MagicGamesIntervals", new[] { "ScoutId" });
            DropIndex("dbo.Scouts", new[] { "HouseId" });
            DropIndex("dbo.MagicGamesTimePoints", new[] { "HouseId" });
            DropIndex("dbo.MagicGamesMarkerPoints", new[] { "HouseId" });
            DropIndex("dbo.Houses", new[] { "SchoolId" });
            DropIndex("dbo.ActivityPoints", new[] { "HouseId" });
            DropIndex("dbo.ActivityPoints", new[] { "ActivityId" });
            DropTable("dbo.TurnoutPoints");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.PunctualityPoints");
            DropTable("dbo.PunctualitySwipes");
            DropTable("dbo.Punctualities");
            DropTable("dbo.MagicGamesIntervals");
            DropTable("dbo.Scouts");
            DropTable("dbo.Schools");
            DropTable("dbo.MagicGamesTimePoints");
            DropTable("dbo.MagicGamesMarkerPoints");
            DropTable("dbo.Houses");
            DropTable("dbo.ActivityPoints");
            DropTable("dbo.Activities");
        }
    }
}
