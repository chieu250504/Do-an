using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettruyen.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool PaidSubscription { get; set; }

        [ForeignKey("RoleEntity")]
        public int IdRole { get; set; }

        public virtual Role RoleEntity { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteStory> FavoriteStories { get; set; }
    }
}
