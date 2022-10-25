using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.Entities
{
    public sealed class PostWithPostReaction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]

        public int AuthorId { get; set; }

      


        [Required]
        [MinLength(4), MaxLength(255)]
        public bool IsAnonymous { get; set; } = true;

        [Required]

        public List<PostReaction> Reactions { get; set; } = new();


        [Required]

        public DateTimeOffset CreatedDate = DateTimeOffset.UtcNow;

        [Required]

        public DateTimeOffset UpdateDate = DateTimeOffset.UtcNow;
    }
}
