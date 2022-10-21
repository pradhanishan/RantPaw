using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Models.DTOS.UserDTOS
{
    public class LoginUserDTO
    {
        [Required]
        [MinLength(3), MaxLength(40)]
        public string Username { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [MinLength(6), MaxLength(40)]
        public string Password { get; set; } = string.Empty;
    }
}
