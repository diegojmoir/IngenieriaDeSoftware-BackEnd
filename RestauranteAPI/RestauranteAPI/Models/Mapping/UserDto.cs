using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models.Dto
{
    public class UserDto:BaseModel<string>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\S*$")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
