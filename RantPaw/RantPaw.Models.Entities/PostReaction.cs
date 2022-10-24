using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.Entities
{
    public sealed class PostReaction
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int? PostId { get; set; }

        [Required]
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public int? ReactionId { get; set; }

        [Required]
        [ForeignKey("ReactionId")]

        public Reaction? Reaction { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        [Required]

        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;


    }
}
