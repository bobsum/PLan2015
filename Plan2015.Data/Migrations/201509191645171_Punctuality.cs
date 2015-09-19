namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Punctuality : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PunctualitySwipes", "ScoutId", "dbo.Scouts");
            DropForeignKey("dbo.PunctualitySwipes", "PunctualityId", "dbo.Punctualities");
            DropIndex("dbo.PunctualitySwipes", new[] { "ScoutId" });
            DropIndex("dbo.PunctualitySwipes", new[] { "PunctualityId" });
            DropTable("dbo.PunctualitySwipes");
            DropTable("dbo.Punctualities");
        }
    }
}
