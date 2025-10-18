using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class ChapterImage
    {
        [Key]
        public int ImageID { get; set; }

        [ForeignKey("Chapter")]
        public int ChapterID { get; set; }

        [StringLength(500)]
        public string ImageURL { get; set; }

        public virtual Chapter Chapter { get; set; }
    }
}
