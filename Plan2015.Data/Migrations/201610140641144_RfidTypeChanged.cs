namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RfidTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scouts", "Rfid", c => c.Long(nullable: false));
            AlterColumn("dbo.TeamMembers", "Rfid", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeamMembers", "Rfid", c => c.String());
            AlterColumn("dbo.Scouts", "Rfid", c => c.String());
        }
    }
}
