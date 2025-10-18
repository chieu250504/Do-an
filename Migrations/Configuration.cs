using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Nettruyen.Models;

namespace Nettruyen.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Nettruyen.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Nettruyen.Models.AppDbContext context)
        {
            // --- Thêm Roles ---
            var adminRole = new Role { IdRole = 1, RoleName = "Admin" };
            var userRole = new Role { IdRole = 2, RoleName = "User" };
            context.Roles.AddOrUpdate(r => r.IdRole, adminRole, userRole);

            // --- Thêm Users ---
            var user1 = new User
            {
                UserID = 1,
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "123456",
                Role = 1,
                IdRole = 1,
                CreatedAt = DateTime.Now,
                PaidSubscription = true
            };
            var user2 = new User
            {
                UserID = 2,
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "123456",
                Role = 2,
                IdRole = 2,
                CreatedAt = DateTime.Now,
                PaidSubscription = false
            };
            context.Users.AddOrUpdate(u => u.UserID, user1, user2);

            // --- Thêm Thể loại (Genres) ---
            var g1 = new Genre { GenreID = 1, Name = "Action", Description = "Hành động kịch tính" };
            var g2 = new Genre { GenreID = 2, Name = "Romance", Description = "Tình cảm lãng mạn" };
            var g3 = new Genre { GenreID = 3, Name = "Comedy", Description = "Hài hước vui nhộn" };
            context.Genres.AddOrUpdate(g => g.GenreID, g1, g2, g3);

            // --- Thêm Type (Truyện tranh, chữ, v.v.) ---
            var t1 = new Models.Type { TypeID = 1, TypeName = "Comic" };
            var t2 = new Models.Type { TypeID = 2, TypeName = "Novel" };
            context.Types.AddOrUpdate(t => t.TypeID, t1, t2);

            // --- Thêm Story ---
            var s1 = new Story
            {
                StoryID = 1,
                Title = "Vua Hải Tặc",
                Author = "Eiichiro Oda",
                Description = "Cuộc phiêu lưu của Luffy và đồng đội.",
                CoverImage = "/images/stories/onepiece.jpg",
                Status = "Đang tiến hành",
                CreatedAt = DateTime.Now,
                Ranking = 9.8f,
                TypeID = 1
            };
            var s2 = new Story
            {
                StoryID = 2,
                Title = "Thám Tử Lừng Danh Conan",
                Author = "Gosho Aoyama",
                Description = "Câu chuyện về cậu bé Conan điều tra phá án.",
                CoverImage = "/images/stories/conan.jpg",
                Status = "Đang tiến hành",
                CreatedAt = DateTime.Now,
                Ranking = 9.5f,
                TypeID = 1
            };
            context.Stories.AddOrUpdate(s => s.StoryID, s1, s2);

            // --- Gán thể loại cho truyện ---
            context.StoryGenres.AddOrUpdate(
                new StoryGenre { StoryID = 1, GenreID = 1 },
                new StoryGenre { StoryID = 1, GenreID = 3 },
                new StoryGenre { StoryID = 2, GenreID = 2 },
                new StoryGenre { StoryID = 2, GenreID = 3 }
            );

            // --- Thêm Chapter ---
            var c1 = new Chapter
            {
                ChapterID = 1,
                StoryID = 1,
                ChapterNumber = 1,
                ChapterTitle = "Khởi đầu hành trình",
                PublishedAt = DateTime.Now.AddDays(-10)
            };
            var c2 = new Chapter
            {
                ChapterID = 2,
                StoryID = 1,
                ChapterNumber = 2,
                ChapterTitle = "Đụng độ hải tặc Buggy",
                PublishedAt = DateTime.Now.AddDays(-5)
            };
            context.Chapters.AddOrUpdate(c => c.ChapterID, c1, c2);

            // --- Thêm Comment ---
            var cm1 = new Comment
            {
                CommentID = 1,
                UserID = 2,
                StoryID = 1,
                ChapterID = 1,
                Content = "Truyện hay quá!",
                CreatedAt = DateTime.Now.AddDays(-2)
            };
            context.Comments.AddOrUpdate(c => c.CommentID, cm1);

            // --- Thêm Favorite ---
            var f1 = new FavoriteStory
            {
                StoryID = 1,
                UserID = 2
            };
            context.FavoriteStories.AddOrUpdate(f1);

            context.SaveChanges();
        }
    }
}
