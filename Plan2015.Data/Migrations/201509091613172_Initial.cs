namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.MarkerPoints",
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
            DropForeignKey("dbo.MarkerPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.EventPoints", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.Scouts", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.Houses", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.EventPoints", "EventId", "dbo.Events");
            DropIndex("dbo.TurnoutPoints", new[] { "HouseId" });
            DropIndex("dbo.TurnoutPoints", new[] { "TeamMemberId" });
            DropIndex("dbo.MarkerPoints", new[] { "HouseId" });
            DropIndex("dbo.Scouts", new[] { "HouseId" });
            DropIndex("dbo.Houses", new[] { "SchoolId" });
            DropIndex("dbo.EventPoints", new[] { "HouseId" });
            DropIndex("dbo.EventPoints", new[] { "EventId" });
            DropTable("dbo.TurnoutPoints");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.MarkerPoints");
            DropTable("dbo.Scouts");
            DropTable("dbo.Schools");
            DropTable("dbo.Houses");
            DropTable("dbo.Events");
            DropTable("dbo.EventPoints");
        }
    }
}
