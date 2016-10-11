namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PunctualityStation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PunctualityStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DefaultAll = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Punctualities", "StationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Punctualities", "StationId");
            AddForeignKey("dbo.Punctualities", "StationId", "dbo.PunctualityStations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Punctualities", "StationId", "dbo.PunctualityStations");
            DropIndex("dbo.Punctualities", new[] { "StationId" });
            DropColumn("dbo.Punctualities", "StationId");
            DropTable("dbo.PunctualityStations");
        }
    }
}
