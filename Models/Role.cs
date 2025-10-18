using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nettruyen.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
