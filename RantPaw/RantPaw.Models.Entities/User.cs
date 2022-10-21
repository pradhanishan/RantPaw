using System.ComponentModel.DataAnnotations;

namespace RantPaw.Models.Entities
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(40)]
        public string Username { get; set; } = String.Empty;

        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
    }
}