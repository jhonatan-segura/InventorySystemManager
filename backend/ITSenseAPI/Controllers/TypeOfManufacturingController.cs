using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ITSenseAPI.Context;
using ITSenseAPI.DTOs;
using ITSenseAPI.Entities;

namespace ITSenseAPI.Controllers
{

   [ApiController]
   [Route("api/type-of-manufacturing")]
   [Authorize]
   public class TypeOfManufacturingController(ApplicationDbContext context) : ControllerBase
   {
      private readonly ApplicationDbContext _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<GetTypeOfManufacturingDto>>> GetAll()
      {
         var typesOfManufacturing = await _context.TypesOfManufacturing
             .Where(p => p.IsActive == 1)
             .Select(p => new GetTypeOfManufacturingDto
             {
                Id = p.Id,
                Name = p.Name,
             })
             .ToListAsync();

         return Ok(typesOfManufacturing);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<GetTypeOfManufacturingDto>> GetById(int id)
      {
         var typeOfManufacturing = await _context.TypesOfManufacturing
             .Where(p => p.IsActive == 1 && p.Id == id)
             .Select(p => new GetTypeOfManufacturingDto
             {
                Id = p.Id,
                Name = p.Name
             })
             .FirstOrDefaultAsync();

         if (typeOfManufacturing == null)
            return NotFound();

         return Ok(typeOfManufacturing);
      }

      [HttpPost]
      public async Task<IActionResult> Create(TypeOfManufacturingDto dto)
      {
         var typeOfManufacturing = new TypeOfManufacturing
         {
            Name = dto.Name,
            IsActive = 1
         };

         _context.TypesOfManufacturing.Add(typeOfManufacturing);
         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetById), new { id = typeOfManufacturing.Id }, typeOfManufacturing);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, TypeOfManufacturingDto dto)
      {
         var product = await _context.TypesOfManufacturing.FindAsync(id);
         if (product == null || product.IsActive == 0)
            return NotFound();

         product.Name = dto.Name;

         await _context.SaveChangesAsync();
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         var typesOfManufacturing = await _context.TypesOfManufacturing.FindAsync(id);
         if (typesOfManufacturing == null || typesOfManufacturing.IsActive == 0)
            return NotFound();

         typesOfManufacturing.IsActive = 0;
         await _context.SaveChangesAsync();

         return NoContent();
      }

   }
}