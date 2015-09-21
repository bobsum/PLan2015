namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameLesson2Activities : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Lessons", newName: "Activities");
            RenameTable(name: "dbo.LessonPoints", newName: "ActivityPoints");
            RenameColumn(table: "dbo.ActivityPoints", name: "LessonId", newName: "ActivityId");
            RenameIndex(table: "dbo.ActivityPoints", name: "IX_LessonId", newName: "IX_ActivityId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ActivityPoints", name: "IX_ActivityId", newName: "IX_LessonId");
            RenameColumn(table: "dbo.ActivityPoints", name: "ActivityId", newName: "LessonId");
            RenameTable(name: "dbo.ActivityPoints", newName: "LessonPoints");
            RenameTable(name: "dbo.Activities", newName: "Lessons");
        }
    }
}
