namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameEvent2Lesson : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Events", newName: "Lessons");
            RenameTable(name: "dbo.EventPoints", newName: "LessonPoints");
            RenameColumn(table: "dbo.LessonPoints", name: "EventId", newName: "LessonId");
            RenameIndex(table: "dbo.LessonPoints", name: "IX_EventId", newName: "IX_LessonId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LessonPoints", name: "IX_LessonId", newName: "IX_EventId");
            RenameColumn(table: "dbo.LessonPoints", name: "LessonId", newName: "EventId");
            RenameTable(name: "dbo.LessonPoints", newName: "EventPoints");
            RenameTable(name: "dbo.Lessons", newName: "Events");
        }
    }
}
