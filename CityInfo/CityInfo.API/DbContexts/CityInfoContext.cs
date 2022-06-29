using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext:DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Zagreb")
                {
                    Id = 1,
                    Description = "Beli grad"
                },
                new City("Slavonski Brod")
                {
                    Id = 2,
                    Description = "Brod na Savi"
                },
                new City("Zadar")
                {
                    Id = 3,
                    Description = "Grad na moru"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Špansko trg") { Id = 1, CityId = 1, Description = "Mjesto gdje ima svacega" },
                new PointOfInterest("Jarun") { Id = 2, CityId = 1, Description = "Mjesto za rolanje i opustanje" },              
                new PointOfInterest("Torovi") { Id = 3, CityId = 2, Description = "Mjesto za odmor" },
                new PointOfInterest("Lepinja") { Id = 4, CityId = 2, Description = "Jako finooo" },
                new PointOfInterest("Vinogorje") { Id = 5, CityId = 3, Description = "Mjesto gdje ima zelenila" },
                new PointOfInterest("Korzo") { Id = 6, CityId = 3, Description = "Mjesto za rolanje i opustanje" }
                );
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
        

    }
}
