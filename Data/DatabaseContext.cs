using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Country>? Countries { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seeds
            builder.Entity<Country>().HasData(
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

            builder.Entity<Hotel>().HasData(
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
