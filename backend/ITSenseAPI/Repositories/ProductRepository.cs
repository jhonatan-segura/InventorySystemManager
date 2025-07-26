using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;
using ITSenseAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITSenseAPI.Repositories
{
   public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
   {
      private new readonly ApplicationDbContext _context = context;

      public async Task<IEnumerable<GetProductDto>> GetAllActiveAsync()
   {
      return await _context.Products
          .Where(p => p.IsActive == 1)
          .Include(p => p.TypeOfManufacturing)
          .Include(p => p.ProductStatus)
          .Select(p => new GetProductDto
          {
             Id = p.Id,
             Name = p.Name,
             StockQuantity = p.StockQuantity,
             TypeOfManufacturingId = p.TypeOfManufacturingId,
             TypeOfManufacturingName = p.TypeOfManufacturing.Name,
             ProductStatusId = p.ProductStatusId,
             ProductStatusName = p.ProductStatus!.Name
          })
          .ToListAsync();
   }

   public async Task<GetProductDto?> GetByIdAsync(int id)
   {
      var product = await _context.Products
          .Where(p => p.IsActive == 1 && p.Id == id)
          .Include(p => p.TypeOfManufacturing)
          .Include(p => p.ProductStatus)
          .Select(p => new GetProductDto
          {
             Id = p.Id,
             Name = p.Name,
             StockQuantity = p.StockQuantity,
             TypeOfManufacturingId = p.TypeOfManufacturingId,
             TypeOfManufacturingName = p.TypeOfManufacturing.Name,
             ProductStatusId = p.ProductStatusId,
             ProductStatusName = p.ProductStatus!.Name
          })
          .FirstOrDefaultAsync();

      return product;
   }
   public async Task<GetProductDto> CreateAsync(ProductDto dto)
   {
      var product = new Product
      {
         Name = dto.Name,
         StockQuantity = dto.StockQuantity,
         TypeOfManufacturingId = dto.TypeOfManufacturingId,
         ProductStatusId = dto.ProductStatusId,
         IsActive = 1,
         RegisteredDate = DateTime.UtcNow
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return new GetProductDto
      {
         Id = product.Id,
         Name = product.Name,
         StockQuantity = product.StockQuantity,
         TypeOfManufacturingId = product.TypeOfManufacturingId,
         ProductStatusId = product.ProductStatusId,
      };
   }

   public async Task<GetProductDto?> UpdateAsync(int id, ProductDto dto)
   {
      var product = await _context.Products.FindAsync(id);
      if (product == null || product.IsActive == 0)
         return null;

      product.Name = dto.Name;
      product.StockQuantity = dto.StockQuantity;
      product.TypeOfManufacturingId = dto.TypeOfManufacturingId;
      product.ProductStatusId = dto.ProductStatusId;

      await _context.SaveChangesAsync();
      return new GetProductDto
      {
         Id = product.Id,
         Name = product.Name,
         StockQuantity = product.StockQuantity,
         TypeOfManufacturingId = product.TypeOfManufacturingId,
         ProductStatusId = product.ProductStatusId,
      };
   }

   public async Task<bool> SetAsFaultyAsync(int id)
   {
      var vehicle = await _context.Products.FindAsync(id);

      if (vehicle == null)
         return false;

      var faultyStatus = await _context.Set<ProductStatus>()
            .FirstOrDefaultAsync(s => s.Name.ToLower() == "defectuoso");

      if (faultyStatus == null)
         return false;

      vehicle.ProductStatusId = faultyStatus.Id;

      await _context.SaveChangesAsync();

      return true;
   }
   public async Task<(bool Success, string Message)> MoveToOutputAsync(MoveProductDto dto)
   {
      var product = await _context.Products
          .FirstOrDefaultAsync(p => p.Id == dto.ProductId && p.IsActive == 1);

      if (product == null)
         return (false, "Producto no encontrado");

      if (dto.StockQuantity <= 0)
         return (false, "La cantidad debe ser mayor que cero");

      if (product.StockQuantity < dto.StockQuantity)
         return (false, "Cantidad en stock insuficiente");

      product.StockQuantity -= dto.StockQuantity;

      var output = new InventoryOutput
      {
         ProductId = dto.ProductId,
         StockQuantity = dto.StockQuantity,
         Reason = dto.Reason,
         RegisteredDate = DateTime.UtcNow
      };

      _context.InventoryOutputs.Add(output);
      await _context.SaveChangesAsync();

      return (true, "Producto movido a salida de inventario correctamente.");
   }

   // public new async Task<bool> SoftDeleteAsync(int id)
   // {
   //    var product = await _context.Products.FindAsync(id);
   //    if (product == null || product.IsActive == 0)
   //       return false;

   //    product.IsActive = 0;
   //    await _context.SaveChangesAsync();
   //    return true;
   // }

}
}