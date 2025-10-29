using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nettruyen.Models;
using PagedList;

namespace Nettruyen.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index(int? page)
        {
            int pageSize = 16;                  
            int pageNumber = page ?? 1;        

            
            var latestStoriesQuery = db.Stories
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .Include(s => s.Chapters)
                .OrderByDescending(s => s.Chapters.Max(c => c.PublishedAt));

            
            var latestStories = latestStoriesQuery
                .ToList() 
                .Select(s => new StoryDisplayViewModel
                {
                    StoryID = s.StoryID,
                    Title = s.Title,
                    Author = s.Author,
                    CoverImage = s.CoverImage,
                    TypeName = s.Type?.TypeName,
                    Views = s.Views,
                    Genres = s.StoryGenres.Select(g => g.Genre.Name).ToList(),
                    LatestChapters = s.Chapters
                        .OrderByDescending(c => c.PublishedAt)
                        .Take(3)
                        .Select(c => new ChapterDisplayViewModel
                        {
                            ChapterNumber = c.ChapterNumber,
                            ChapterTitle = c.ChapterTitle,
                            PublishedAt = c.PublishedAt,
                            ChapterID = c.ChapterID,
                        }).ToList()
                })
                .ToPagedList(pageNumber, pageSize);

            //  Top truyện theo lượt xem
            var topStories = db.Stories
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .Include(s => s.Chapters)
                .OrderByDescending(s => s.Views)
                .Take(10)
                .ToList()
                .Select(s => new StoryDisplayViewModel
                {
                    StoryID = s.StoryID,
                    Title = s.Title,
                    Author = s.Author,
                    CoverImage = s.CoverImage,
                    TypeName = s.Type?.TypeName,
                    Views = s.Views,
                    Genres = s.StoryGenres.Select(g => g.Genre.Name).ToList(),
                    LatestChapters = s.Chapters
                        .OrderByDescending(c => c.PublishedAt)
                        .Take(3)
                        .Select(c => new ChapterDisplayViewModel
                        {
                            ChapterNumber = c.ChapterNumber,
                            ChapterTitle = c.ChapterTitle,
                            PublishedAt = c.PublishedAt
                        }).ToList()
                })
                .ToList();

            
            var model = new HomeViewModel
            {
                LatestStories = latestStories,
                TopStories = topStories
            };

            return View(model);
        }
    }

    
    public static class TimeHelper
    {
        public static string GetRelativeTime(DateTime publishedAt)
        {
            var now = DateTime.Now;
            var diff = now - publishedAt;

            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";

            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";

            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";

            if (diff.TotalDays < 30)
                return $"{(int)(diff.TotalDays / 7)} tuần trước";

            return publishedAt.ToString("dd/MM/yyyy");
        }
    }

   
    public class HomeViewModel
    {
        public IPagedList<StoryDisplayViewModel> LatestStories { get; set; }
        public List<StoryDisplayViewModel> TopStories { get; set; }
    }

    
    public class StoryDisplayViewModel
    {
        public int StoryID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string TypeName { get; set; }
        public int Views { get; set; }
        public List<string> Genres { get; set; }
        public List<ChapterDisplayViewModel> LatestChapters { get; set; }
    }

    
    public class ChapterDisplayViewModel
    {
        public int ChapterID { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterTitle { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
