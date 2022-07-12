using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Data
{
    public class HotelsConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel()
                {
                    Id = 1,
                    Name = "Helvetia Hotel",
                    Address = "Marata St., 11, St. Petersburg",
                    Rating = 9.6,
                    CountryId = 1
                },
                new Hotel()
                {
                    Id = 2,
                    Name = "Hilton Kyiv",
                    Address = "30 Tarasa Shevchenka Blvd",
                    Rating = 8.5,
                    CountryId = 2,
                },
                new Hotel()
                {
                    Id = 3,
                    Name = "BonHotel",
                    Address = "2 Pritytskogo St",
                    Rating = 8.8,
                    CountryId = 3
                }
            );
        }
    }
}
