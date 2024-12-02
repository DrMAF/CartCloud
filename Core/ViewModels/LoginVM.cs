using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public record LoginVM
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
