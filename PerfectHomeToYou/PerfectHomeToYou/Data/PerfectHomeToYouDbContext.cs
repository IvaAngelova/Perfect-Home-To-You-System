using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Data
{
    public class PerfectHomeToYouDbContext : IdentityDbContext<User>
    {
        public PerfectHomeToYouDbContext(DbContextOptions<PerfectHomeToYouDbContext> options)
            : base(options)
        {

        }

        public DbSet<Apartment> Apartments { get; init; }
        public DbSet<Client> Clients { get; init; }
        public DbSet<City> Cities { get; init; }
        public DbSet<Neighborhood> Neighborhoods { get; init; }
        public DbSet<Question> Questions { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Apartment>()
                .HasOne(c => c.City)
                .WithMany(c => c.Apartments)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Apartment>()
                .HasOne(c => c.Neighborhood)
                .WithMany(c => c.Apartments)
                .HasForeignKey(c => c.NeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Apartment>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Apartments)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Neighborhood>()
                .HasOne(c => c.City)
                .WithMany(c => c.Neighborhoods)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Question>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Question>()
                .HasOne(c => c.Apartment)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Client>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Client>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
