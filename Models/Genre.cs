using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nettruyen.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual ICollection<StoryGenre> StoryGenres { get; set; }
    }
}
