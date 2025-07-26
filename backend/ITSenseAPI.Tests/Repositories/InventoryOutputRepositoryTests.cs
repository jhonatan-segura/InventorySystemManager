using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;
using ITSenseAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class InventoryOutputRepositoryTests
{
   private readonly InventoryOutputRepository _repository;
   private readonly ApplicationDbContext _context;

   public InventoryOutputRepositoryTests()
   {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase("InventoryOutputTestDb")
          .Options;

      _context = new ApplicationDbContext(options);
      _repository = new InventoryOutputRepository(_context);
   }

   [Fact]
   public async Task CreateAsync_ShouldAddInventoryOutput()
   {
      var product = new Product
      {
         Name = "Martillo",
         StockQuantity = 15,
         TypeOfManufacturingId = 1,
         ProductStatusId = 1,
         IsActive = 1
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      var dto = new InventoryOutputDto
      {
         ProductId = product.Id,
         StockQuantity = 3,
         Reason = "Venta"
      };

      var resultId = await _repository.CreateAsync(dto);

      var output = await _context.InventoryOutputs.FindAsync(resultId);
      Assert.NotNull(output);
      Assert.Equal("Venta", output!.Reason);
   }
}
