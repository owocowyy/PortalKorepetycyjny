namespace PortalKorepetycyjny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoachReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoachId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Body = c.String(nullable: false, maxLength: 1024),
                        Coach_Id = c.String(maxLength: 128),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Coach_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Coach_Id)
                .Index(t => t.Student_Id);
            
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoachReviews", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CoachReviews", "Coach_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CoachReviews", new[] { "Student_Id" });
            DropIndex("dbo.CoachReviews", new[] { "Coach_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropTable("dbo.CoachReviews");
        }
    }
}
