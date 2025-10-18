using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nettruyen.Models;

namespace Nettruyen.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            
            var latestStories = db.Stories
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .Include(s => s.Chapters)
                .OrderByDescending(s => s.Chapters.Max(c => c.PublishedAt))
                .Take(20)
                .ToList();

            
            var topStories = db.Stories
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .Include(s => s.Chapters)
                .OrderByDescending(s => s.Views)
                .Take(10)
                .ToList();

            
            var model = new HomeViewModel
            {
                LatestStories = latestStories.Select(s => new StoryDisplayViewModel
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
                }).ToList(),

                TopStories = topStories.Select(s => new StoryDisplayViewModel
                {
                    StoryID = s.StoryID,
                    Title = s.Title,
                    Author = s.Author,
                    Views = s.Views, 
                    CoverImage = s.CoverImage,
                    TypeName = s.Type?.TypeName,
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
                }).ToList()
            };

            return View(model);
        }
    }

    
    public class HomeViewModel
    {
        public List<StoryDisplayViewModel> LatestStories { get; set; }
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
        public int ChapterNumber { get; set; }
        public string ChapterTitle { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
