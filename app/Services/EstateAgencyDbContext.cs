using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class EstateAgencyDbContext : DbContext
    {
        // Construtor
        public EstateAgencyDbContext(DbContextOptions options) : base(options) { }

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

            modelBuilder.Entity<RealState>().HasData(
                new RealState
                {
                    Id = 1,
                    Title = "Mansão à beira mar de Jurerê Internacional",
                    Price = 30000,
                    Neighborhood = "Jurerê Internacional",
                    BedroomQuantity = 6,
                    BusinessType = "Aluguel",
                    Address = "Rua dos Tambaquis, 100, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                },
                new RealState
                {
                    Id = 2,
                    Title = "Apartamento na Beira Mar de Florianópolis",
                    Price = 150000,
                    Neighborhood = "Agronômica",
                    BedroomQuantity = 3,
                    BusinessType = "Venda",
                    Address = "Avenida Governador Irineu Bornhausen, 3600, Agronômica, Florianópolis, Santa Catarina, Brasil",
                },
                new RealState
                {
                    Id = 3,
                    Title = "Casa em Jurerê",
                    Price = 300000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 5,
                    BusinessType = "Venda",
                    Address = "Rua dos Tambaquis, 100, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                },
                new RealState
                {
                    Id = 4,
                    Title = "Mansão",
                    Price = 3000000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 5,
                    BusinessType = "Venda",
                    Address = "Avenida dos Buzios, 55, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
                },
                new RealState
                {
                    Id = 5,
                    Title = "Casa",
                    Price = 300000,
                    Neighborhood = "Jurerê",
                    BedroomQuantity = 3,
                    BusinessType = "Aluguel",
                    Address = "Rua das Algas, 488, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil",
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
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}