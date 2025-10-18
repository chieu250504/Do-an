using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Story")]
        public int StoryID { get; set; }

        [ForeignKey("Chapter")]
        public int ChapterID { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Story Story { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}
