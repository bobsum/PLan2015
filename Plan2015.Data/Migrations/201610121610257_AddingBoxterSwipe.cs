namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBoxterSwipe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoxterSwipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SwipeId = c.Int(nullable: false),
                        ScoutId = c.Int(nullable: false),
                        BoxId = c.String(),
                        BoxIdFriendly = c.String(),
                        AppMode = c.String(),
                        AppResponse = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scouts", t => t.ScoutId, cascadeDelete: true)
                .Index(t => t.ScoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoxterSwipes", "ScoutId", "dbo.Scouts");
            DropIndex("dbo.BoxterSwipes", new[] { "ScoutId" });
            DropTable("dbo.BoxterSwipes");
        }
    }
}
