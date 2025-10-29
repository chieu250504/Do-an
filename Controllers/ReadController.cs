using System.Linq;
using System.Web.Mvc;
using Nettruyen.Models;
using System.Data.Entity;

namespace Nettruyen.Controllers
{
    public class ReadController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        
        public ActionResult Chapter(int id)
        {
            
            var chapter = db.Chapters
                .Include(c => c.Story)
                .Include(c => c.ChapterImages)
                .FirstOrDefault(c => c.ChapterID == id);

            if (chapter == null)
                return HttpNotFound();

            return View(chapter);
        }
    }
}
