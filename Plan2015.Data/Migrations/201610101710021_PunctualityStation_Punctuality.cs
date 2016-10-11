namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PunctualityStation_Punctuality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Punctualities", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Punctualities", "Stop", c => c.DateTime(nullable: false));
            DropColumn("dbo.Punctualities", "Deadline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Punctualities", "Deadline", c => c.DateTime(nullable: false));
            DropColumn("dbo.Punctualities", "Stop");
            DropColumn("dbo.Punctualities", "Start");
        }
    }
}
