namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameIntervalToAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MagicGamesIntervals", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.MagicGamesIntervals", "Interval");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MagicGamesIntervals", "Interval", c => c.Int(nullable: false));
            DropColumn("dbo.MagicGamesIntervals", "Amount");
        }
    }
}
