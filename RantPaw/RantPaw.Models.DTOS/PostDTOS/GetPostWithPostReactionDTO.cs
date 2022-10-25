using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.DTOS.PostDTOS
{
    public sealed class GetPostWithPostReactionDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [MinLength(4), MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]

        public int AuthorId { get; set; }

        [Required]
        [MinLength(4), MaxLength(255)]
        public bool IsAnonymous { get; set; } = true;

        [Required]
        public string AuthorName { get; set; } = string.Empty;

        [Required]

        public List<GetPostReactionDTO> Reactions { get; set; } = new();


        [Required]

        public DateTimeOffset CreatedDate = DateTimeOffset.UtcNow;

        [Required]

        public DateTimeOffset UpdateDate = DateTimeOffset.UtcNow;
    }
}
