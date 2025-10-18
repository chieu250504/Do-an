namespace Nettruyen.Models
{
    public class FavoriteStory
    {
        public int StoryID { get; set; }
        public int UserID { get; set; }

        public virtual Story Story { get; set; }
        public virtual User User { get; set; }
    }
}
