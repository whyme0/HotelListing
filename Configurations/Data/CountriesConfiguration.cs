using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Data
{
    public class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country()
                {
                    Id = 1,
                    Name = "Russia",
                    ShortName = "RUS"
                },
                new Country()
                {
                    Id = 2,
                    Name = "Ukraine",
                    ShortName = "UKR",
                },
                new Country()
                {
                    Id = 3,
                    Name = "Belarus",
                    ShortName = "BLR"
                }
            );
        }
    }
}
