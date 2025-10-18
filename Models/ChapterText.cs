using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class ChapterText
    {
        [Key]
        public int TextID { get; set; }

        public int ChapterID { get; set; }
        public string ContentText { get; set; }

        [ForeignKey("ChapterID")]
        public virtual Chapter Chapter { get; set; }
    }

}
