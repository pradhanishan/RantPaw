using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.Entities
{
    public sealed class Post
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(4), MaxLength(255)]
        public bool IsAnonymous { get; set; } = true;

        public int AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public User User { get; set; }

        [Required]

        public DateTimeOffset CreatedDate = DateTimeOffset.UtcNow;

        [Required]

        public DateTimeOffset UpdateDate = DateTimeOffset.UtcNow;

    }
}
