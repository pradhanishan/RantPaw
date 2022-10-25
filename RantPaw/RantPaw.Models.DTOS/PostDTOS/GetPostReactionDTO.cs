using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.DTOS.PostDTOS
{
    public sealed class GetPostReactionDTO
    {
        [Required]
        public int ReactorId { get; set; }

        [Required]
        public string ReactorName { get; set; } = string.Empty;

        [Required]
        public string Reaction { get; set; } = string.Empty;
    }
}
