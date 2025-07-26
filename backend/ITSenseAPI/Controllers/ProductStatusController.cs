using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;

namespace ITSenseAPI.Controllers
{
   [ApiController]
   [Route("api/product-status")]
   [Authorize]

   public class ProductStatusController(ApplicationDbContext context) : ControllerBase
   {
      private readonly ApplicationDbContext _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<GetProductStatusDto>>> GetAll()
      {
         var productStatuses = await _context.ProductStatuses
             .Where(p => p.IsActive == 1)
             .Select(p => new GetProductStatusDto
             {
                Id = p.Id,
                Name = p.Name,
             })
             .ToListAsync();

         return Ok(productStatuses);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<GetProductStatusDto>> GetById(int id)
      {
         var productStatus = await _context.ProductStatuses
             .Where(p => p.IsActive == 1 && p.Id == id)
             .Select(p => new GetProductStatusDto
             {
                Id = p.Id,
                Name = p.Name
             })
             .FirstOrDefaultAsync();

         if (productStatus == null)
            return NotFound();

         return Ok(productStatus);
      }

      [HttpPost]
      public async Task<IActionResult> Create(ProductStatusDto dto)
      {
         var productStatus = new ProductStatus
         {
            Name = dto.Name,
            IsActive = 1
         };

         _context.ProductStatuses.Add(productStatus);
         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetById), new { id = productStatus.Id }, productStatus);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, ProductStatusDto dto)
      {
         var productStatus = await _context.ProductStatuses.FindAsync(id);
         if (productStatus == null || productStatus.IsActive == 0)
            return NotFound();

         productStatus.Name = dto.Name;

         await _context.SaveChangesAsync();
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         var productStatus = await _context.ProductStatuses.FindAsync(id);
         if (productStatus == null || productStatus.IsActive == 0)
            return NotFound();

         productStatus.IsActive = 0;
         await _context.SaveChangesAsync();

         return NoContent();
      }

   }
}