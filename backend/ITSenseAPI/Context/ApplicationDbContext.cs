using ITSenseAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITSenseAPI.Context
{
   public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
   {
      public DbSet<Product> Products { get; set; }
      public DbSet<InventoryOutput> InventoryOutputs { get; set; }
      public DbSet<TypeOfManufacturing> TypesOfManufacturing { get; set; }
      public DbSet<ProductStatus> ProductStatuses { get; set; }
      public DbSet<User> Users { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<InventoryOutput>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Outputs)
            .HasForeignKey(o => o.ProductId);


         modelBuilder.Entity<ProductStatus>().HasData(
                 new ProductStatus { Id = 1, Name = "Disponible" },
                 new ProductStatus { Id = 2, Name = "No Disponible" },
                 new ProductStatus { Id = 3, Name = "Defectuoso" }
             );

         modelBuilder.Entity<TypeOfManufacturing>().HasData(
               new TypeOfManufacturing { Id = 1, Name = "Manual" },
               new TypeOfManufacturing { Id = 2, Name = "Manual y a máquina" }
            );

         modelBuilder.Entity<Product>().HasData(
             new Product
             {
                Id = 1,
                Name = "Taladro Bosch",
                StockQuantity = 10,
                TypeOfManufacturingId = 1,
                ProductStatusId = 1,
                RegisteredDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsActive = 1
             },
             new Product
             {
                Id = 2,
                Name = "Martillo Stanley",
                StockQuantity = 20,
                TypeOfManufacturingId = 2,
                ProductStatusId = 1,
                RegisteredDate = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                IsActive = 1
             },
             new Product
             {
                Id = 3,
                Name = "Cinta Métrica",
                StockQuantity = 30,
                TypeOfManufacturingId = 2,
                ProductStatusId = 2,
                RegisteredDate = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                IsActive = 1
             }
         );

      }
   }
}
