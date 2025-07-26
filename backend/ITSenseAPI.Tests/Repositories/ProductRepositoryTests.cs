using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITSenseAPI.Repositories;

public class ProductRepositoryTests
{
   private readonly ProductRepository _repository;
   private readonly ApplicationDbContext _context;

   public ProductRepositoryTests()
   {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase("TestDb")
          .Options;

      _context = new ApplicationDbContext(options);
      _repository = new ProductRepository(_context);
   }

   [Fact]
   public async Task GetByIdAsync_ReturnsProduct()
   {
      _context.ProductStatuses.Add(new ProductStatus { Id = 1, Name = "Disponible" });
      _context.TypesOfManufacturing.Add(new TypeOfManufacturing { Id = 1, Name = "Manual" });

      _context.Products.Add(new Product
      {
         Id = 1,
         Name = "Taladro",
         StockQuantity = 10,
         ProductStatusId = 1,
         TypeOfManufacturingId = 1,
         IsActive = 1
      });
      await _context.SaveChangesAsync();

      var result = await _repository.GetByIdAsync(1);

      Assert.NotNull(result);
      Assert.Equal("Taladro", result!.Name);
   }

   [Fact]
   public async Task CreateAsync_ShouldAddProduct()
   {
      var dto = new ProductDto
      {
         Name = "Taladro",
         StockQuantity = 5,
         TypeOfManufacturingId = 1,
         ProductStatusId = 1
      };

      var result = await _repository.CreateAsync(dto);

      Assert.NotNull(result);
      Assert.Equal("Taladro", result.Name);
   }

   [Fact]
   public async Task MoveToOutputAsync_ShouldCreateNewInventoryOutput_AndDecreaseStock()
   {
      var product = new Product
      {
         Id = 2,
         Name = "Taladro",
         StockQuantity = 10,
         ProductStatusId = 1,
         TypeOfManufacturingId = 1,
         IsActive = 1
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      var dto = new MoveProductDto
      {
         ProductId = 1,
         StockQuantity = 8,
         Reason = "Salida por uso"
      };

      var result = await _repository.MoveToOutputAsync(dto);

      Assert.True(result.Success);

      var output = await _context.InventoryOutputs
         .FirstOrDefaultAsync(o => o.ProductId == dto.ProductId && o.StockQuantity == dto.StockQuantity && o.Reason == dto.Reason);

      Assert.NotNull(output);
      Assert.Equal(8, output!.StockQuantity);

      var updatedProduct = await _context.Products.FindAsync(1);
      Assert.Equal(2, updatedProduct!.StockQuantity);
   }


   [Fact]
   public async Task SoftDeleteAsync_ShouldMarkAsInactive()
   {
      var product = new Product
      {
         Name = "Producto Prueba",
         StockQuantity = 10,
         TypeOfManufacturingId = 1,
         ProductStatusId = 1,
         IsActive = 1
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      var result = await _repository.SoftDeleteAsync(product.Id);

      Assert.True(result);
      var dbProduct = await _context.Products.FindAsync(product.Id);
      Assert.Equal(0, dbProduct!.IsActive);
   }
}
