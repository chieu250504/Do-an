using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nettruyen.Models;
using PagedList;

namespace Nettruyen.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: /Search?keyword=ten_truyen&page=1
        public ActionResult Index(string keyword, int? page)
        {
            int pageSize = 16;
            int pageNumber = page ?? 1;

            // ✅ Nếu không nhập gì → trả về view trống
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ViewBag.Keyword = "";
                return View(new List<StoryDisplayViewModel>().ToPagedList(1, 1));
            }

            // 🔍 Tìm truyện có tên hoặc tác giả chứa từ khóa (không phân biệt hoa thường)
            var query = db.Stories
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .Include(s => s.Chapters)
                .Where(s => s.Title.Contains(keyword) || s.Author.Contains(keyword))
                .OrderByDescending(s => s.Chapters.Max(c => c.PublishedAt));

            // ⚙️ Chuyển sang ViewModel
            var stories = query
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
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Keyword = keyword;

            return View(stories);
        }
        // API tìm kiếm động
        [HttpGet]
        public JsonResult LiveSearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Json(new { results = new object[0] }, JsonRequestBehavior.AllowGet);

            // Tạo UrlHelper từ Request hiện tại
            var urlHelper = new UrlHelper(Request.RequestContext);

            var results = db.Stories
                .Where(s => s.Title.Contains(keyword) || s.Author.Contains(keyword))
                .OrderByDescending(s => s.Views)
                .Take(4)
                .ToList()
                .Select(s => new
                {
                    s.StoryID,
                    s.Title,
                    s.Author,
                    CoverImage = urlHelper.Content(s.CoverImage) // dùng urlHelper thay vì Url
                });

            return Json(new { results }, JsonRequestBehavior.AllowGet);
        }

    }
}
