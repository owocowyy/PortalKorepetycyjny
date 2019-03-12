namespace PortalKorepetycyjny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Day = c.DateTime(nullable: false),
                        StartHour = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Coach_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Advertisments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        CoachId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CoachId_Id)
                .Index(t => t.CoachId_Id);
            
            CreateTable(
                "dbo.StudentAdvertisments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Descryption = c.String(),
                        Title = c.String(),
                        AdvertismentDate = c.DateTime(nullable: false),
                        Creator_Id = c.String(maxLength: 128),
                        SbujectName_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.Subjects", t => t.SbujectName_Id)
                .Index(t => t.Creator_Id)
                .Index(t => t.SbujectName_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Coach_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatusName = c.String(),
                        LastActivity = c.DateTime(nullable: false),
                        CoachName_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CoachName_Id)
                .Index(t => t.CoachName_Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Status", "CoachName_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Availabilities", "Coach_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subjects", "Coach_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentAdvertisments", "SbujectName_Id", "dbo.Subjects");
            DropForeignKey("dbo.StudentAdvertisments", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Advertisments", "CoachId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Status", new[] { "CoachName_Id" });
            DropIndex("dbo.Subjects", new[] { "Coach_Id" });
            DropIndex("dbo.StudentAdvertisments", new[] { "SbujectName_Id" });
            DropIndex("dbo.StudentAdvertisments", new[] { "Creator_Id" });
            DropIndex("dbo.Advertisments", new[] { "CoachId_Id" });
            DropIndex("dbo.Availabilities", new[] { "Coach_Id" });
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.Status");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentAdvertisments");
            DropTable("dbo.Advertisments");
            DropTable("dbo.Availabilities");
        }
    }
}
