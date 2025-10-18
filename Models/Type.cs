using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nettruyen.Models
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }

        [StringLength(255)]
        public string TypeName { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}
