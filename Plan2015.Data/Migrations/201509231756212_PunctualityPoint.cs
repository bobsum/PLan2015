namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PunctualityPoint : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PunctualityPoints", "PunctualityId", "dbo.Punctualities");
            DropForeignKey("dbo.PunctualityPoints", "HouseId", "dbo.Houses");
            DropIndex("dbo.PunctualityPoints", new[] { "HouseId" });
            DropIndex("dbo.PunctualityPoints", new[] { "PunctualityId" });
            DropTable("dbo.PunctualityPoints");
        }
    }
}
