using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO? Country { get; set; }
    }

    public class CreateHotelDTO
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Hotel name is too long")]
        public string? Name { get; set; }
        
        [Required]
        [MaxLength(250, ErrorMessage = "Address name is too long")]
        public string? Address { get; set; }
        
        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }
        
        [Required]
        public int CountryId { get; set; }
    }
}
