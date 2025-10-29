using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nettruyen.Models;

namespace Nettruyen.Controllers
{
    public class StoryController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: /Story/Detail/5
        public ActionResult Detail(int id)
        {
           
            var story = db.Stories
                .Include(s => s.Chapters)
                .Include(s => s.Type)
                .Include(s => s.StoryGenres.Select(g => g.Genre))
                .FirstOrDefault(s => s.StoryID == id);

            if (story == null)
                return HttpNotFound();

            
            var chapters = story.Chapters
                .OrderByDescending(c => c.ChapterNumber)
                .ToList();

            ViewBag.Chapters = chapters;

            return View(story);
        }
    }
}
