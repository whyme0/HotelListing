using Microsoft.AspNetCore.Identity;

namespace HotelListing.Data
{
    public class ApiUser : IdentityUser
    {
        public DateTime RegistrationDate { get; init; }
        
        public ApiUser() : base()
        {
            RegistrationDate = DateTime.Now;
        }
    }
}
