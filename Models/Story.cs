using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class Story
    {
        [Key]
        public int StoryID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        public string Description { get; set; }

        [StringLength(500)]
        public string CoverImage { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public int Views { get; set; }

        public float Ranking { get; set; }

        [ForeignKey("Type")]
        public int TypeID { get; set; }
        public virtual Type Type { get; set; }

        public virtual ICollection<StoryGenre> StoryGenres { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteStory> FavoriteStories { get; set; }
    }
}
