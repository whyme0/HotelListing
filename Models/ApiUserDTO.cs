using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class LoginApiUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

    public class ApiUserDTO
    {
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Password length cannot be less than 8 characters or more than 16 characters!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
