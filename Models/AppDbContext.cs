using System.Data.Entity;

namespace Nettruyen.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext") { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryGenre> StoryGenres { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterImage> ChapterImages { get; set; }
        public DbSet<ChapterText> ChapterTexts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavoriteStory> FavoriteStories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<StoryGenre>()
                .HasKey(sg => new { sg.StoryID, sg.GenreID });

            modelBuilder.Entity<FavoriteStory>()
                .HasKey(fs => new { fs.StoryID, fs.UserID });

            
            modelBuilder.Entity<Chapter>()
                .HasRequired(c => c.Story)
                .WithMany(s => s.Chapters)
                .HasForeignKey(c => c.StoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChapterText>()
                .HasRequired(ct => ct.Chapter)
                .WithMany(c => c.ChapterTexts)
                .HasForeignKey(ct => ct.ChapterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChapterImage>()
                .HasRequired(ci => ci.Chapter)
                .WithMany(c => c.ChapterImages)
                .HasForeignKey(ci => ci.ChapterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Story)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FavoriteStory>()
                .HasRequired(fs => fs.Story)
                .WithMany(s => s.FavoriteStories)
                .HasForeignKey(fs => fs.StoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoryGenre>()
                .HasRequired(sg => sg.Story)
                .WithMany(s => s.StoryGenres)
                .HasForeignKey(sg => sg.StoryID)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
