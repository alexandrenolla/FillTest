using app.Models;
using Microsoft.EntityFrameworkCore;
using app.Utilites.GeoCoding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace app.Services
{
    public class EstateAgencyDbContext : IdentityDbContext<IdentityUser>
    {   
        private readonly GoogleMapsGeoCoding _geoCodingService;

        // Constructor with GoogleMapsGeoCoding service injection
        public EstateAgencyDbContext(DbContextOptions<EstateAgencyDbContext> options, GoogleMapsGeoCoding geoCodingService) : base(options) 
        {
            _geoCodingService = geoCodingService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            var connectionString = "Server=localhost;User ID=root;Password=root;Database=EstateAgencyDb;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString, serverVersion, mySqlOptions => mySqlOptions.EnableRetryOnFailure());
        }

        // DB Tables 
        public DbSet<User> Users { get; set; }
        public DbSet<RealState> Properties { get; set; }
        public DbSet<RealStateImage> Photos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship between RealState and RealStateImage
            modelBuilder.Entity<RealStateImage>()
                .HasOne(x => x.RealState)
                .WithMany(y => y.RealStateImages)
                .HasForeignKey(fk => fk.RealStateId)
                .OnDelete(DeleteBehavior.Cascade);


            // Initial data injection
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Alexandre Nolla",
                    Email = "alexandrenolla@gmail.com",
                    Telephone = "48988050165",
                    Password = "fullstack123"
                }
            );
            
            // Initial data injection with coordinates retrieval
            modelBuilder.Entity<RealState>().HasData(
                new RealState
                {
                    Id = 1,
                    Title = "Mansão Branca",
                    Price = 50000,
                    Neighborhood = "Jurerê Internacional",
                    BedroomQuantity = 6,
                    BusinessType = "Aluguel",
                    Address = "Rodovia Jornalista Maurício Sirotsky Sobrinho, 5145, Jurerê, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rodovia Jornalista Maurício Sirotsky Sobrinho, 5145, Jurerê, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rodovia Jornalista Maurício Sirotsky Sobrinho, 5145, Jurerê, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                },
                new RealState
                {
                    Id = 2,
                    Title = "Apartamento",
                    Price = 650000,
                    Neighborhood = "Canasvieiras",
                    BedroomQuantity = 3,
                    BusinessType = "Venda",
                    Address = "Rodovia Tertuliano Brito Xavier, 3100, Canasvieiras, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rodovia Tertuliano Brito Xavier, 3100, Canasvieiras, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rodovia Tertuliano Brito Xavier, 3100, Canasvieiras, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                },
                new RealState
                {
                    Id = 3,
                    Title = "Casa Azul",
                    Price = 3000000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 5,
                    BusinessType = "Venda",
                    Address = "Rua dos Tambaquis, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rua dos Tambaquis, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rua dos Tambaquis, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                },
                new RealState
                {
                    Id = 4,
                    Title = "Mansão Amarela",
                    Price = 5000000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 5,
                    BusinessType = "Venda",
                    Address = "Rua das Tibiras, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rua das Tibiras, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rua das Tibiras, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                },
                new RealState
                {
                    Id = 5,
                    Title = "Casa",
                    Price = 30000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 3,
                    BusinessType = "Aluguel",
                    Address = "Rua das Algas, 488, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rua das Algas, 488, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rua das Algas, 488, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                },
                new RealState
                {
                    Id = 6,
                    Title = "Casa na praia",
                    Price = 1000000,
                    Neighborhood = "Canasvieiras",
                    BedroomQuantity = 3,
                    BusinessType = "Venda",
                    Address = "Rodovia Tertuliano Brito Xavier, 3000, Canasvieiras, Florianópolis, Santa Catarina, Brasil",
                    Latitude = _geoCodingService.GetCoordinates("Rodovia Tertuliano Brito Xavier, 3000, Canasvieiras, Florianópolis, Santa Catarina, Brasil").Result.Latitude,
                    Longitude = _geoCodingService.GetCoordinates("Rodovia Tertuliano Brito Xavier, 3000, Canasvieiras, Florianópolis, Santa Catarina, Brasil").Result.Longitude
                }
            );

            modelBuilder.Entity<RealStateImage>().HasData(
                new RealStateImage
                {
                    Id = 1,
                    ImageFileName = "casa.jpeg",
                    RealStateId = 1
                },
                new RealStateImage
                {
                    Id = 2,
                    ImageFileName = "casa2.jpeg",
                    RealStateId = 1
                },
                new RealStateImage
                {
                    Id = 3,
                    ImageFileName = "casa3.jpeg",
                    RealStateId = 1
                },
                new RealStateImage
                {
                    Id = 4,
                    ImageFileName = "casa4.jpeg",
                    RealStateId = 2
                },
                new RealStateImage
                {
                    Id = 5,
                    ImageFileName = "casa5.jpeg",
                    RealStateId = 2
                },
                new RealStateImage
                {
                    Id = 6,
                    ImageFileName = "casa6.jpeg",
                    RealStateId = 2
                },
                new RealStateImage
                {
                    Id = 7,
                    ImageFileName = "casa7.jpeg",
                    RealStateId = 3
                },
                new RealStateImage
                {
                    Id = 8,
                    ImageFileName = "casa8.jpeg",
                    RealStateId = 3
                },
                new RealStateImage
                {
                    Id = 9,
                    ImageFileName = "casa9.jpeg",
                    RealStateId = 4
                },
                new RealStateImage
                {
                    Id = 10,
                    ImageFileName = "casa10.jpeg",
                    RealStateId = 4
                },
                new RealStateImage
                {
                    Id = 11,
                    ImageFileName = "casa11.jpeg",
                    RealStateId = 5
                },
                new RealStateImage
                {
                    Id = 12,
                    ImageFileName = "casa12.jpeg",
                    RealStateId = 5
                },
                new RealStateImage
                {
                    Id = 13,
                    ImageFileName = "casa13.jpeg",
                    RealStateId = 6
                },
                new RealStateImage
                {
                    Id = 14,
                    ImageFileName = "casa14.jpeg",
                    RealStateId = 6
                },
                new RealStateImage
                {
                    Id = 15,
                    ImageFileName = "casa15.jpeg",
                    RealStateId = 6
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}