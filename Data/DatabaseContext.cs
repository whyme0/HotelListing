using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Country>? Countries { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // rename tables
            builder.Entity<ApiUser>(e => e.ToTable("ApiUsers"));
            builder.Entity<IdentityRole>(e => e.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));

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
