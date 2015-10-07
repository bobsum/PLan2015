namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDiscarded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers");
            DropIndex("dbo.TurnoutPoints", new[] { "TeamMemberId" });
            AddColumn("dbo.TurnoutPoints", "Discarded", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TurnoutPoints", "TeamMemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.TurnoutPoints", "TeamMemberId");
            AddForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers");
            DropIndex("dbo.TurnoutPoints", new[] { "TeamMemberId" });
            AlterColumn("dbo.TurnoutPoints", "TeamMemberId", c => c.Int());
            DropColumn("dbo.TurnoutPoints", "Discarded");
            CreateIndex("dbo.TurnoutPoints", "TeamMemberId");
            AddForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers", "Id");
        }
    }
}
