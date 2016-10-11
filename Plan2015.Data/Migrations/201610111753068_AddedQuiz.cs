namespace Plan2015.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuiz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("dbo.QuizQuestions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizPoints", "QuestionId", "dbo.QuizQuestions");
            DropForeignKey("dbo.QuizPoints", "HouseId", "dbo.Houses");
            DropIndex("dbo.QuizPoints", new[] { "HouseId" });
            DropIndex("dbo.QuizPoints", new[] { "QuestionId" });
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuizPoints");
        }
    }
}
