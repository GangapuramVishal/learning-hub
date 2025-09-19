using EFCoreLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLoading
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaAmenity> VillaAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Price = 200,

                },
                new Villa
                {
                    Id = 2,
                    Name = "Grand Villa",
                    Price = 300,

                },
                new Villa
                {
                    Id = 3,
                    Name = "Pool Villa",
                    Price = 500,

                });

            modelBuilder.Entity<VillaAmenity>().HasData(
                new VillaAmenity
                {
                    Id = 1,
                    VillaId = 1,
                    Name = "Private Pool"
                },
                new VillaAmenity
                {
                    Id = 2,
                    VillaId = 1,
                    Name = "Microwave"
                },
                new VillaAmenity
                {
                    Id = 3,
                    VillaId = 1,
                    Name = "Private Balcony"
                },
                new VillaAmenity
                {
                    Id = 4,
                    VillaId = 1,
                    Name = "1 king Bed"
                },
                new VillaAmenity
                {
                    Id = 5,
                    VillaId = 2,
                    Name = "Private Plunge Pool"
                },
                new VillaAmenity
                {
                    Id = 6,
                    VillaId = 2,
                    Name = "Microwave and mini Refrigerator"
                },
                new VillaAmenity
                {
                    Id = 9,
                    VillaId = 3,
                    Name = "Private Pool"
                },
                new VillaAmenity
                {
                    Id = 10,
                    VillaId = 3,
                    Name = "Jacuzzi"
                },
                new VillaAmenity
                {
                    Id = 11,
                    VillaId = 3,
                    Name = "Private Balcony"
                });

        }
    }
}
