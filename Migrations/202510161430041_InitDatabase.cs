namespace Nettruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        StoryID = c.Int(nullable: false),
                        ChapterID = c.Int(nullable: false),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Chapters", t => t.ChapterID, cascadeDelete: true)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.StoryID)
                .Index(t => t.ChapterID);
            
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        ChapterID = c.Int(nullable: false, identity: true),
                        StoryID = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        ChapterTitle = c.String(maxLength: 255),
                        PublishedAt = c.DateTime(nullable: false),
                        ChapterText_TextID = c.Int(),
                    })
                .PrimaryKey(t => t.ChapterID)
                .ForeignKey("dbo.ChapterTexts", t => t.ChapterText_TextID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .Index(t => t.StoryID)
                .Index(t => t.ChapterText_TextID);
            
            CreateTable(
                "dbo.ChapterImages",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ChapterID = c.Int(nullable: false),
                        ImageURL = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Chapters", t => t.ChapterID)
                .Index(t => t.ChapterID);
            
            CreateTable(
                "dbo.ChapterTexts",
                c => new
                    {
                        TextID = c.Int(nullable: false, identity: true),
                        ChapterID = c.Int(nullable: false),
                        ContentText = c.String(),
                    })
                .PrimaryKey(t => t.TextID)
                .ForeignKey("dbo.Chapters", t => t.ChapterID)
                .Index(t => t.ChapterID);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Author = c.String(maxLength: 255),
                        Description = c.String(),
                        CoverImage = c.String(maxLength: 500),
                        Status = c.String(maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                        Ranking = c.Single(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoryID)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.FavoriteStories",
                c => new
                    {
                        StoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryID, t.UserID })
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.StoryID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        PasswordHash = c.String(maxLength: 255),
                        Role = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        PaidSubscription = c.Boolean(nullable: false),
                        IdRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.IdRole, cascadeDelete: true)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        IdRole = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdRole);
            
            CreateTable(
                "dbo.StoryGenres",
                c => new
                    {
                        StoryID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryID, t.GenreID })
                .ForeignKey("dbo.Genres", t => t.GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .Index(t => t.StoryID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.TypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Comments", "ChapterID", "dbo.Chapters");
            DropForeignKey("dbo.Chapters", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Stories", "TypeID", "dbo.Types");
            DropForeignKey("dbo.StoryGenres", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.StoryGenres", "GenreID", "dbo.Genres");
            DropForeignKey("dbo.Users", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.FavoriteStories", "UserID", "dbo.Users");
            DropForeignKey("dbo.FavoriteStories", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Chapters", "ChapterText_TextID", "dbo.ChapterTexts");
            DropForeignKey("dbo.ChapterTexts", "ChapterID", "dbo.Chapters");
            DropForeignKey("dbo.ChapterImages", "ChapterID", "dbo.Chapters");
            DropIndex("dbo.StoryGenres", new[] { "GenreID" });
            DropIndex("dbo.StoryGenres", new[] { "StoryID" });
            DropIndex("dbo.Users", new[] { "IdRole" });
            DropIndex("dbo.FavoriteStories", new[] { "UserID" });
            DropIndex("dbo.FavoriteStories", new[] { "StoryID" });
            DropIndex("dbo.Stories", new[] { "TypeID" });
            DropIndex("dbo.ChapterTexts", new[] { "ChapterID" });
            DropIndex("dbo.ChapterImages", new[] { "ChapterID" });
            DropIndex("dbo.Chapters", new[] { "ChapterText_TextID" });
            DropIndex("dbo.Chapters", new[] { "StoryID" });
            DropIndex("dbo.Comments", new[] { "ChapterID" });
            DropIndex("dbo.Comments", new[] { "StoryID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropTable("dbo.Types");
            DropTable("dbo.Genres");
            DropTable("dbo.StoryGenres");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.FavoriteStories");
            DropTable("dbo.Stories");
            DropTable("dbo.ChapterTexts");
            DropTable("dbo.ChapterImages");
            DropTable("dbo.Chapters");
            DropTable("dbo.Comments");
        }
    }
}
