using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }

    public class CreateCountryDTO
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Country name is too long")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Country short name is too long")]
        public string? ShortName { get; set; }
    }
}
