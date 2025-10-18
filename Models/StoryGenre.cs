namespace Nettruyen.Models
{
    public class StoryGenre
    {
        public int StoryID { get; set; }
        public int GenreID { get; set; }

        public virtual Story Story { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
