namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTurnoutPoint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers");
            DropIndex("dbo.TurnoutPoints", new[] { "TeamMemberId" });
            AddColumn("dbo.TurnoutPoints", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.TurnoutPoints", "TeamMemberId");
            DropColumn("dbo.TurnoutPoints", "Log");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TurnoutPoints", "Log", c => c.String());
            AddColumn("dbo.TurnoutPoints", "TeamMemberId", c => c.Int(nullable: false));
            DropColumn("dbo.TurnoutPoints", "Time");
            CreateIndex("dbo.TurnoutPoints", "TeamMemberId");
            AddForeignKey("dbo.TurnoutPoints", "TeamMemberId", "dbo.TeamMembers", "Id", cascadeDelete: true);
        }
    }
}
