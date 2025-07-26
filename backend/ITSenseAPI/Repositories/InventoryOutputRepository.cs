using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;
using ITSenseAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITSenseAPI.Repositories
{
   public class InventoryOutputRepository(ApplicationDbContext context) : BaseRepository<InventoryOutput>(context), IInventoryOutputRepository
   {
      private new readonly ApplicationDbContext _context = context;

      public async Task<IEnumerable<GetInventoryOutputDto>> GetAllActiveAsync()
      {
         return await _context.InventoryOutputs
             .Where(p => p.IsActive == 1)
             .Select(p => new GetInventoryOutputDto
             {
                Id = p.Id,
                Product = new GetProductIdNameDto
                {
                   Id = p.ProductId,
                   Name = p.Product.Name,
                },
                StockQuantity = p.StockQuantity,
                Reason = p.Reason,
             })
             .ToListAsync();
      }

      public async Task<GetInventoryOutputDto?> GetByIdAsync(int id)
      {
         var product = await _context.InventoryOutputs
             .Where(p => p.IsActive == 1 && p.Id == id)
             .Select(p => new GetInventoryOutputDto
             {
                Id = p.Id,
                Product = new GetProductIdNameDto
                {
                   Id = p.ProductId,
                   Name = p.Product.Name,
                },
                StockQuantity = p.StockQuantity,
                Reason = p.Reason,
             })
             .FirstOrDefaultAsync();

         return product;
      }
      public async Task<GetInventoryOutputDto> CreateAsync(InventoryOutputDto dto)
      {
         var inventoryOutput = new InventoryOutput
         {
            ProductId = dto.ProductId,
            StockQuantity = dto.StockQuantity,
            Reason = dto.Reason,
            IsActive = 1
         };

         _context.InventoryOutputs.Add(inventoryOutput);
         await _context.SaveChangesAsync();

         return new GetInventoryOutputDto
         {
            Id = inventoryOutput.Id,
            Product = new GetProductIdNameDto
            {
               Id = inventoryOutput.ProductId,
               Name = inventoryOutput.Product.Name,
            },
            StockQuantity = inventoryOutput.StockQuantity,
            Reason = inventoryOutput.Reason,
         };
      }

      public async Task<GetInventoryOutputDto?> UpdateAsync(int id, InventoryOutputDto dto)
      {
         var inventoryOutput = await _context.InventoryOutputs.FindAsync(id);
         if (inventoryOutput == null || inventoryOutput.IsActive == 0)
            return null;

         inventoryOutput.ProductId = dto.ProductId;
         inventoryOutput.StockQuantity = dto.StockQuantity;
         inventoryOutput.Reason = dto.Reason;

         await _context.SaveChangesAsync();
         return new GetInventoryOutputDto
         {
            Id = inventoryOutput.Id,
            Product = new GetProductIdNameDto
            {
               Id = inventoryOutput.ProductId,
               Name = inventoryOutput.Product.Name,
            },
            StockQuantity = inventoryOutput.StockQuantity,
            Reason = inventoryOutput.Reason,
         };
      }

      // public async Task<bool> SoftDeleteAsync(int id)
      // {
      //    var inventoryOutputs = await _context.InventoryOutputs.FindAsync(id);
      //    if (inventoryOutputs == null || inventoryOutputs.IsActive == 0)
      //       return false;

      //    inventoryOutputs.IsActive = 0;
      //    await _context.SaveChangesAsync();
      //    return true;
      // }

   }
}