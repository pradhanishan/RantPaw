using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.DTOS.PostDTOS
{
    public sealed class GetPostRangeParamDTO
    {
        [Required]
        public int StartingRow { get; set; }

        [Required]
        public int NumberOfRows { get; set; }
    }
}
