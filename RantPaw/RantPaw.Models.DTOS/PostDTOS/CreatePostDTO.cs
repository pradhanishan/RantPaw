using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.DTOS.PostDTOS
{
    public sealed class CreatePostDTO
    {

        [Required]
        [MinLength(4), MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAnonymous { get; set; } = true;

        public int AuthorID { get; set; }

    }
}
