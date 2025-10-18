using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterID { get; set; }

        [ForeignKey("Story")]
        public int StoryID { get; set; }

        public int ChapterNumber { get; set; }

        [StringLength(255)]
        public string ChapterTitle { get; set; }

        public DateTime PublishedAt { get; set; }

        public virtual Story Story { get; set; }

        public virtual ICollection<ChapterImage> ChapterImages { get; set; }
        public virtual ChapterText ChapterText { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ChapterText> ChapterTexts { get; set; }
    }
}
